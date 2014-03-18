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
        Bitmap kinectVideo;
        IntPtr colorPtr;

        public Kinect()
        {
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
                //this.kinect.SkeletonFrameReady += this.kinectSkeletonFrameReady;

                // set up color stream
                this.colorPixels = new byte[this.kinect.ColorStream.FramePixelDataLength];
                //this.colorBitmap = new WriteableBitmap(this.kinect.ColorStream.FrameWidth, this.kinect.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                this.kinect.ColorFrameReady += this.kinectSensorColorFrameReady;//this.kinectSensorColorFrameReady;

                // start the kinect              
                this.kinect.Start();
            }
        }

        public Bitmap kinectVideoBitmap
        {
            get { return this.kinectVideo; }
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
    }
}
