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
       // private WriteableBitmap colorBitmap;
        Bitmap kinectSkel;
        Bitmap kinectVideo;
        Bitmap finalImage;

        IntPtr colorPtr;
        Graphics g;
        Point kinectVideoDimensions;
       

        public Kinect(Point kinectVideoDimensions)
        {
            this.kinectVideoDimensions = kinectVideoDimensions;

            this.finalImage = new Bitmap(kinectVideoDimensions.X,kinectVideoDimensions.Y);

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
        }
        public Bitmap updateKinect()
        {
            // update kinect bitmap   
            using (g = Graphics.FromImage(finalImage))
            {
                g.Clear(System.Drawing.Color.Black);
                // overlay
                if (kinectVideo != null && kinectSkel != null)
                {
                    g.DrawImage(kinectVideo, new Rectangle(0, 0, this.kinectVideoDimensions.X, this.kinectVideoDimensions.Y));
                    g.DrawImage(kinectSkel, new Rectangle(0, 0, this.kinectVideoDimensions.X, this.kinectVideoDimensions.Y));
                }
                

               return finalImage;
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
                                this.kinectVideoDimensions.X,
                                this.kinectVideoDimensions.Y
                                );
                            //g = Graphics.FromImage(kinectSkel);

                            DrawBone(JointType.Head, JointType.ShoulderCenter, s, g);
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
                        colorFrame.Width,
                        colorFrame.Height,
                        colorFrame.Width * colorFrame.BytesPerPixel,
                        System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                        colorPtr);

                    TaichiFitness.KinectVideoInstance.Image = kinectVideo;
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
        private void DrawBone(JointType j1, JointType j2, Skeleton s, Graphics g)
        {
            Point p1 = getPoint(s, j1);
            Point p2 = getPoint(s, j2);
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

    }
}

