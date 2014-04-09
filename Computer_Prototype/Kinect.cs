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
        // dimensions of picturebox for Kinect
        int x, y;
        // angles of limbs for feedback {left elbow,left shoulder,right shoulder,right elbow}
        public int[] angles;

        Bitmap kinectSkel,kinectVideo,finalImage;
        IntPtr colorPtr;

        Graphics graphics;
        // dummy boolean
        public bool moveCompleted;
        public int ERROR_TOLERANCE = 15;

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
            angles = new int[4];

            for (int i = 0; i < 4; i++)
            {
                angles[i] = 0;
            }
            moveCompleted = false;
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
                            // get point
                            int[] skelAngle = new int[4]; 
                            Point shoulderright = getPoint(s, JointType.ShoulderRight);
                            Point elbowright = getPoint(s, JointType.ElbowRight);
                            Point wristright = getPoint(s, JointType.WristRight);

                            Point shouldercenter = getPoint(s, JointType.ShoulderCenter);

                            Point shoulderleft = getPoint(s, JointType.ShoulderLeft);
                            Point elbowleft = getPoint(s, JointType.ElbowLeft);
                            Point wristleft = getPoint(s, JointType.WristLeft);

                            int Angle = (int)((Math.Atan2(shoulderright.Y - elbowright.Y, shoulderright.X - elbowright.X)
                                        - Math.Atan2(wristright.Y - elbowright.Y, wristright.X - elbowright.X)
                                        % (2 * Math.PI)) * 180 / Math.PI);
                            if (Angle < 0)
                            {
                                Angle = Angle + 360;
                            }
                            // left elbow
                            skelAngle[0] = (int)((Math.Atan2(shoulderleft.Y - elbowleft.Y, shoulderleft.X - elbowleft.X)
                                         - Math.Atan2(wristleft.Y - elbowleft.Y, wristleft.X - elbowleft.X)
                                         % (2 * Math.PI)) * 180 / Math.PI);
                            // left shoulder
                            skelAngle[1] = (int)((Math.Atan2(elbowleft.Y - shoulderleft.Y, elbowleft.X - shoulderleft.X)
                                            - Math.Atan2(shouldercenter.Y - shoulderleft.Y, shouldercenter.X - shoulderleft.X)
                                            % (2 * Math.PI)) * 180 / Math.PI);
                            //right elbow
                            skelAngle[2] = (int)((Math.Atan2(shoulderright.Y - elbowright.Y, shoulderright.X - elbowright.X)
                                            - Math.Atan2(wristright.Y - elbowright.Y, wristright.X - elbowright.X)
                                            % (2 * Math.PI)) * 180 / Math.PI);
                            // right shoulder
                            skelAngle[3] = (int)((Math.Atan2(elbowright.Y - shoulderright.Y, elbowright.X - shoulderright.X)
                                            - Math.Atan2(shouldercenter.Y - shoulderright.Y, shouldercenter.X - shoulderright.X)
                                            % (2 * Math.PI)) * 180 / Math.PI);


                            for (int i = 0; i < skelAngle.Length; i++)
                            {
                                if (skelAngle[i] < 0)
                                {
                                    skelAngle[i] = skelAngle[i] + 360;
                                }                               
                            }
                            // debug information
                            TaichiFitness.kinect_info = "LE: " + skelAngle[0] + ",LC: " + skelAngle[1] + ",RC: " + skelAngle[2] + ",RE: " + skelAngle[3];
                            Color color;
                            if (moveCompleted)
                            {
                                color = Color.Green;
                            }
                            else
                            {
                                color = Color.Red;
                            }
                            bool[] cond = new bool[4];

                            cond[0] = (skelAngle[0] < angles[0] + ERROR_TOLERANCE) && (skelAngle[0] > angles[0] - ERROR_TOLERANCE);
                            cond[1] = (skelAngle[1] < angles[1] + ERROR_TOLERANCE) && (skelAngle[1] > angles[1] - ERROR_TOLERANCE);
                            cond[2] = (skelAngle[2] < angles[2] + ERROR_TOLERANCE) && (skelAngle[2] > angles[2] - ERROR_TOLERANCE);
                            cond[3] = (skelAngle[3] < angles[3] + ERROR_TOLERANCE) && (skelAngle[3] > angles[3] - ERROR_TOLERANCE);

                            // TEST TO SEE IF THIS WORKS
                            if (cond[0] && cond[1] && cond[2] && cond[3])
                            {
                                color = Color.Green;
                            }
                            else
                            {
                                color = Color.Red;
                            }

                            DrawBone(JointType.Head, JointType.ShoulderCenter, s, graphics, color);

                            // shoulders 
                            DrawBone(JointType.ShoulderLeft, JointType.ShoulderCenter, s, graphics, color);
                            DrawBone(JointType.ShoulderRight, JointType.ShoulderCenter, s, graphics, color);

                            // bicep/tricep
                            DrawBone(JointType.ElbowRight, JointType.ShoulderRight, s, graphics, color);
                            DrawBone(JointType.ElbowLeft, JointType.ShoulderLeft, s, graphics, color);
                            // forearm

                            DrawBone(JointType.ElbowRight, JointType.WristRight, s, graphics, color);
                            DrawBone(JointType.ElbowLeft, JointType.WristLeft, s, graphics, color);

                            // torso
                            DrawBone(JointType.ShoulderCenter, JointType.Spine, s, graphics, color);

                            // hips
                            DrawBone(JointType.HipRight, JointType.Spine, s, graphics, color);
                            DrawBone(JointType.HipLeft, JointType.Spine, s, graphics, color);

                            //// quads
                            DrawBone(JointType.HipRight, JointType.KneeRight, s, graphics, color);
                            DrawBone(JointType.HipLeft, JointType.KneeLeft, s, graphics, color);

                            //// shin
                            DrawBone(JointType.AnkleLeft, JointType.KneeLeft, s, graphics, color);
                            DrawBone(JointType.AnkleRight, JointType.KneeRight, s, graphics, color);
                           
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
        private void DrawBone(JointType j1, JointType j2, Skeleton s, Graphics g, Color color)
        {
            Point p1 = getPoint(s, j1);
            Point p2 = getPoint(s, j2);
            Pen pen = new Pen(new SolidBrush(color),2);

            if (g != null)
            {
                try
                {
                    g.DrawLine(pen, p1, p2);
                }
                catch (Exception e)
                {                    
                    Console.WriteLine(e.ToString());
                }
            }

        }

    }
}

