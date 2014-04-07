using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;



namespace TaichiKinect
{
    public partial class TaichiWindow : Form
    {
        public KinectSensor kinect;
        public Skeleton[] skeletonData;
        private byte[] colorPixels;
        Bitmap kinectSkel;
        Bitmap kinectVideo;
        Bitmap finalImage;

        IntPtr colorPtr;
        Graphics g;
        
        public TaichiWindow()
        {
            InitializeComponent();
            this.label_connection_status.Text = "Connecting..";
            finalImage = new Bitmap(this.pictureBox_videostream.Width, this.pictureBox_videostream.Height);
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    kinect = potentialSensor;
                    this.label_connection_status.Text = "Connected!";
                    break;
                }
            }

            if (this.kinect != null)
            {
                // enable stream of information
                this.kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                this.kinect.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                this.kinect.SkeletonStream.Enable();


                //set up skeleton object            
                skeletonData = new Skeleton[kinect.SkeletonStream.FrameSkeletonArrayLength]; // Allocate ST data
                this.kinect.SkeletonFrameReady += this.kinectSkeletonFrameReady; 

                // set up color stream
                this.colorPixels = new byte[this.kinect.ColorStream.FramePixelDataLength];
                //this.colorBitmap = new WriteableBitmap(this.kinect.ColorStream.FrameWidth, this.kinect.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                this.kinect.ColorFrameReady += this.kinectSensorColorFrameReady;//this.kinectSensorColorFrameReady;
               
                // start the kinect              
               this.kinect.Start();
               this.label_connection_status.Text = "Kinect Started!";
            }
        }

        private void kinectSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            this.label_connection_status.Text = "Skeleton Frame Ready!";
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame()) // Open the Skeleton frame
            {
                if (skeletonFrame != null && this.skeletonData != null) // check that a frame is available
                {
                    skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletonData); // get the skeletal information in this frame                   
                    foreach (Skeleton s in skeletonData)
                    {
                        if (s.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            // head
                            Point headPoint = getPoint(s,JointType.Head);
                            //shoulder center
                            Point neckPoint = getPoint(s,JointType.ShoulderCenter);
                            kinectSkel = new Bitmap(
                                this.pictureBox_videostream.Width,
                                this.pictureBox_videostream.Height
                                );
                            g = Graphics.FromImage(kinectSkel);

                            DrawBone(JointType.Head,JointType.ShoulderCenter,s,g);
                            // shoulders 
                            DrawBone(JointType.ShoulderLeft, JointType.ShoulderCenter, s, g);
                            DrawBone(JointType.ShoulderRight, JointType.ShoulderCenter, s, g);
                            // bicep/tricep
                            DrawBone(JointType.ElbowRight, JointType.ShoulderRight, s, g);
                            DrawBone(JointType.ElbowLeft, JointType.ShoulderLeft, s, g);
                            // forearm
                            DrawBone(JointType.ElbowRight, JointType.WristRight, s, g);  
                            DrawBone(JointType.ElbowLeft, JointType.WristLeft, s, g);


                            // torso
                            DrawBone(JointType.ShoulderCenter, JointType.Spine, s, g);

                            // hips
                            //DrawBone(JointType.HipRight, JointType.Spine, s, g);
                            //DrawBone(JointType.HipLeft, JointType.Spine, s, g);

                            //// quads
                            //DrawBone(JointType.HipRight, JointType.KneeRight, s, g);
                            //DrawBone(JointType.HipLeft, JointType.KneeLeft, s, g);

                            //// shin
                            //DrawBone(JointType.AnkleLeft, JointType.KneeLeft, s, g);
                            //DrawBone(JointType.AnkleRight, JointType.KneeRight, s, g);

                            int x = headPoint.X;
                            int y= headPoint.Y;
                            if (x != 0 || y != 0)
                            {
                                this.label_connection_status.Text = "x: " + x + ", y: " + y;
                            }
                            
                        }   
                    }
                }
            }
        }

        private Point getPoint(Skeleton s,JointType j)
        {
            SkeletonPoint skelpoint = s.Joints[j].Position;
            // different from guide
            ColorImagePoint colorpoint = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(skelpoint, ColorImageFormat.RgbResolution640x480Fps30);
            return new System.Drawing.Point(colorpoint.X,colorpoint.Y);
        }

        private void DrawBone(JointType j1, JointType j2, Skeleton s, Graphics g)
        {
            Point p1 = getPoint(s,j1);
            Point p2 = getPoint(s,j2);
            if (g != null)
            {
                try
                {
                    g.DrawLine(Pens.Red, p1, p2);
                }
                catch (Exception e)
                {

                }
            }
            
        }


        private void kinectSensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            this.label_connection_status.Text = "Color Frame Ready!";
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {           
                if (colorPixels == null) colorPixels = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(this.colorPixels);
                colorFrame.CopyPixelDataTo(colorPixels);

                Marshal.FreeHGlobal(colorPtr);
                colorPtr = Marshal.AllocHGlobal(colorPixels.Length);
                Marshal.Copy(colorPixels, 0, colorPtr, colorPixels.Length);

                kinectVideo = new Bitmap(
                    //colorFrame.Width,
                    //colorFrame.Height,
                    this.pictureBox_videostream.Width,
                    this.pictureBox_videostream.Height,
                    colorFrame.Width * colorFrame.BytesPerPixel,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                    colorPtr);                                             
                }
            }           
        
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (this.kinect.ElevationAngle < this.kinect.MaxElevationAngle)
            {
                this.kinect.ElevationAngle += 5;
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (this.kinect.ElevationAngle > this.kinect.MinElevationAngle)
            {
                this.kinect.ElevationAngle -= 5;
            }
        }

        private void TaichiWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Stopping Kinect","Message Box");
            kinect.Stop();
        }

        private void timerElapsedEvent(object sender, EventArgs e)
        {
            // update kinect bitmap   
            using (g = Graphics.FromImage(finalImage))
            {
                g.Clear(System.Drawing.Color.Black);
                // overlay
                if (kinectVideo != null && kinectSkel != null)
                {
                    g.DrawImage(kinectVideo, new Rectangle(0, 0, this.pictureBox_videostream.Width, this.pictureBox_videostream.Height));
                    g.DrawImage(kinectSkel, new Rectangle(0, 0, this.pictureBox_videostream.Width, this.pictureBox_videostream.Height));
                }
                
                this.pictureBox_videostream.Image = finalImage;
            }
            
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            this.timer_info_update.Enabled = true;
        }

        private void pictureBox_videostream_Click(object sender, EventArgs e)
        {

        }
    }
}
