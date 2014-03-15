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
using System.Windows.Media.Imaging.WriteableBitmap;


namespace TaichiKinect
{
    public partial class TaichiWindow : Form
    {
        public KinectSensor kinect;
        public Skeleton[] skeletonData;
        private byte[] colorPixels;
        private System.Windows.Media.Imaging.WriteableBitmap colorBitmap;

        
        public TaichiWindow()
        {
            InitializeComponent();
            this.label_connection_status.Text = "Connecting..";
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
                this.kinect.ColorStream.Enable(ColorImageFormat.InfraredResolution640x480Fps30);


                //set up skeleton object            
                skeletonData = new Skeleton[kinect.SkeletonStream.FrameSkeletonArrayLength]; // Allocate ST data
                this.kinect.SkeletonFrameReady += this.kinectSkeletonFrameReady; // new EventHandler<SkeletonFrameReadyEventArgs>(this.kinectSkeletonFrameReady); // Get Ready for Skeleton Ready Events

                // set up color stream
                this.colorPixels = new byte[this.kinect.ColorStream.FramePixelDataLength];
                //this.colorBitmap = new WriteableBitmap(this.sensor.ColorStream.FrameWidth, this.kinect.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                this.kinect.ColorFrameReady += this.kinectSensorColorFrameReady;//this.kinectSensorColorFrameReady;


                // this is a test
               
                // start the kinect              
               this.kinect.Start();           
            }
        }

        private void kinectSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame()) // Open the Skeleton frame
            {
                if (skeletonFrame != null && this.skeletonData != null) // check that a frame is available
                {
                    skeletonFrame.CopySkeletonDataTo(this.skeletonData); // get the skeletal information in this frame
                }           
            }
            this.label_connection_status.Text = "Skeleton Ready!";

        }

          private void kinectSensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
          {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
              if (colorFrame != null)
              {
                colorFrame.CopyPixelDataTo(this.colorPixels);
                
              }
            }  
          }
        private void label_connection_status_Click(object sender, EventArgs e)
        {
    
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

            foreach (Skeleton skel in skeletonData)
            {
                if (skel != null)
                {
                    this.label_connection_status.Text = skel.TrackingId.ToString();
                }
                
            }
        }
    }
}
