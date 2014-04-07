using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Prototype
{       
    public class Kinect
    {
        private KinectSensor kinect;
        public Skeleton[] skeletonData;
        private byte[] colorPixels;
        int x, y;

        Bitmap kinectSkel;
        Bitmap kinectVideo;
        Bitmap finalImage;
        IntPtr colorPtr;
        Graphics graphics;       

        public Kinect()
        {                    
            // detect Kinect Sensor
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    kinect = potentialSensor;                
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
                this.kinect.ColorFrameReady += this.kinectSensorColorFrameReady;//this.kinectSensorColorFrameReady;

                // start the kinect              
                this.kinect.Start();
                
            }
            x = TaichiFitness.KinectVideoInstance.Width;
            y = TaichiFitness.KinectVideoInstance.Height;
            this.finalImage =  new Bitmap(x, y);
            //TaichiFitness.KinectVideoInstance.Image = this.finalImage;
        }
        
        public void updateKinect()
        {
            // update kinect bitmap   
            using (Graphics g = Graphics.FromImage(finalImage))
            {
                g.Clear(System.Drawing.Color.Black);
                //overlay
                if (kinectVideo != null)
                {
                    g.DrawImage(kinectVideo, new Rectangle(0, 0, x, y));                    
                }
                if (kinectSkel != null)
                {
                    g.DrawImage(kinectSkel, new Rectangle(0, 0, x, y));
                }
                TaichiFitness.KinectVideoInstance.Image = finalImage;
            }
        }
        private void kinectSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
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
                            
                            kinectSkel = new Bitmap(                               
                                x,
                                y
                                );
                            graphics = Graphics.FromImage(kinectSkel);
                            
                            // head
                            DrawBone(JointType.Head, JointType.ShoulderCenter, s, graphics,Pens.Red);

                            // shoulders 
                            DrawBone(JointType.ShoulderLeft, JointType.ShoulderCenter, s, graphics, Pens.Red);
                            DrawBone(JointType.ShoulderRight, JointType.ShoulderCenter, s, graphics, Pens.Red);

                            // bicep/tricep
                            DrawBone(JointType.ElbowRight, JointType.ShoulderRight, s, graphics, Pens.Red);
                            DrawBone(JointType.ElbowLeft, JointType.ShoulderLeft, s, graphics, Pens.Red);

                            // forearm
                            DrawBone(JointType.ElbowRight, JointType.WristRight, s, graphics, Pens.Red);
                            DrawBone(JointType.ElbowLeft, JointType.WristLeft, s, graphics, Pens.Red);

                            // torso
                            DrawBone(JointType.ShoulderCenter, JointType.Spine, s, graphics, Pens.Red);

                            // hips
                            DrawBone(JointType.HipRight, JointType.Spine, s, graphics, Pens.Red);
                            DrawBone(JointType.HipLeft, JointType.Spine, s, graphics, Pens.Red);

                            //// quads
                            DrawBone(JointType.HipRight, JointType.KneeRight, s, graphics, Pens.Red);
                            DrawBone(JointType.HipLeft, JointType.KneeLeft, s, graphics, Pens.Red);

                            //// shin
                            DrawBone(JointType.AnkleLeft, JointType.KneeLeft, s, graphics, Pens.Red);
                            DrawBone(JointType.AnkleRight, JointType.KneeRight, s, graphics, Pens.Red);

                            //TaichiFitness.KinectVideoInstance.Image = kinectSkel;
                        }

                    }
                }


            }
        }       
        public void closeKinect()
        {
            this.kinect.Stop();
        }
        private void kinectSensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
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
                        x,
                        y,
                        colorFrame.Width * colorFrame.BytesPerPixel,
                        System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                        colorPtr);

                    //TaichiFitness.KinectVideoInstance.Image = kinectVideo;
                }
            }
        }
        private Point getPoint(Skeleton s, JointType j)
        {
            SkeletonPoint skelpoint = s.Joints[j].Position;
            // different from guide
            ColorImagePoint colorpoint = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(skelpoint, ColorImageFormat.RgbResolution640x480Fps30);
            return new System.Drawing.Point(colorpoint.X, colorpoint.Y);
        }
        private void DrawBone(JointType j1, JointType j2, Skeleton s, Graphics g, Pen color)
        {
            Point p1 = getPoint(s, j1);
            Point p2 = getPoint(s, j2);

            if (g != null)
            {
                try
                {
                    g.DrawLine(color, p1, p2);
                }
                catch (Exception e)
                {                    
                    Console.WriteLine(e.ToString());
                }
            }

        }

    }
}

