using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Prototype
{
    public partial class TaichiFitness : Form
    {
        private static PictureBox kinectVideo;
        public int elapsedTime;
        public int pausedTime;
        //public const int 
        // formcode[form number, form segment time info]
        private int[,] formcode = new int[,] { { 1, 1, 1, 1, 1 }, 
                                               { 2, 2, 2, 2, 2 }, 
                                               { 3, 3, 3, 3, 3 } }; 
        //Singleton VideoFeed for Kinect
        public static PictureBox KinectVideoInstance
        {
            get
            {
               return kinectVideo;
            }
        }
       
       // Cursor myCursor;
        Kinect kinect;
        public TaichiFitness()
        {
            InitializeComponent();
            //FileIO fileIO = new FileIO();
            elapsedTime = 0;
            pausedTime = 0;
            kinectVideo = this.pictureBoxKinectVideoStream;
            kinectVideoTimer.Enabled = false ;
            kinect = new Kinect();
      
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.mainMenuPanel.Show();
            this.moveListPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Hide();
        }

        public void updateKinectVideoStream(Bitmap Videoframe)
        {
            ///this.pictureBoxKinectVideoStream.Image = this.kinect.kinectVideoBitmap;
        }
        private void play_Click(object sender, EventArgs e)
        {
            this.mainMenuPanel.Hide();
            this.moveListPanel.Show();
            this.highscorePanel.Hide();
            this.gamePanel.Hide();

        }
        private void exit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure that you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                kinect.closeKinect();
                Application.ExitThread();
            }

        }

        private void highscorePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (labelGameMode.Text == this.buttonForm1.Text)
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Hide();
                this.highscorePanel.Hide();
                this.gamePanel.Show();
                this.labelGameMode.Text = this.buttonForm2.Text;
                this.videoTutorial.URL = "./misc/White Crane-windows.wmv";
            }
            else if (labelGameMode.Text == this.buttonForm2.Text)
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Hide();
                this.highscorePanel.Hide();
                this.gamePanel.Show();
                this.labelGameMode.Text = this.buttonForm3.Text;
                this.videoTutorial.URL = "./misc/Rollout-windows.wmv";
            }
            else if (labelGameMode.Text == this.buttonForm3.Text)
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Hide();
                this.highscorePanel.Hide();
                this.gamePanel.Show();
                this.labelGameMode.Text = this.buttonForm1.Text;
                this.videoTutorial.URL = "./misc/preparation.wmv";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void startTutorial_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Show();
            this.labelGameMode.Text = "Tutorial";
            //this.videoTutorial.settings.autoStart = false;
            this.videoTutorial.URL = "./misc/Tutorial-windows.wmv";
            this.kinectVideoTimer.Enabled = true;
        }
        private void startForm1_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Show();
            this.labelGameMode.Text = this.buttonForm1.Text;
            this.videoTutorial.URL = "./misc/preparation.wmv";
            this.elapsedTime = 0;
            this.timerMoves.Enabled = true;
            this.kinectVideoTimer.Enabled = true;
        }
        private void startForm2_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Show();
            this.labelGameMode.Text = this.buttonForm2.Text;
            this.videoTutorial.URL = "./misc/White Crane-windows.wmv";
            this.elapsedTime = 0;
            this.timerMoves.Enabled = true;
            this.kinectVideoTimer.Enabled = true;
        }
        private void startForm3_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Show();
            this.labelGameMode.Text = this.buttonForm3.Text;
            this.videoTutorial.URL = "./misc/Rollout-windows.wmv";
            this.elapsedTime = 0;
            this.timerMoves.Enabled = true;
            this.kinectVideoTimer.Enabled = true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Show();
            this.highscorePanel.Hide();
            this.gamePanel.Hide();
        }

        private void shuffle_Click(object sender, EventArgs e)
        {
            string temp = buttonForm1.Text;
            buttonForm1.Text = buttonForm2.Text;
            buttonForm2.Text = buttonForm3.Text;
            buttonForm3.Text = temp;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void videoTutorial_EndOfStream(object sender, AxWMPLib._WMPOCXEvents_EndOfStreamEvent e)
        {
            this.kinectVideoTimer.Enabled = false;
            if (this.labelGameMode.Text == "Tutorial")
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Show();
                this.highscorePanel.Hide();
                this.gamePanel.Hide();
            }
            else
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Hide();
                this.highscorePanel.Show();
                this.gamePanel.Hide();
            }

        }

        private void videoTutorial_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 1)
            {
                if (this.labelGameMode.Text == "Tutorial")
                {
                    this.moveListPanel.Hide();
                    this.mainMenuPanel.Show();
                    this.highscorePanel.Hide();
                    this.gamePanel.Hide();
                }
                else
                {
                    this.moveListPanel.Hide();
                    this.mainMenuPanel.Hide();
                    this.highscorePanel.Show();
                    this.gamePanel.Hide();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void mainMenuTableLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void titleLabel_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TaichiFitness_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dialog = MessageBox.Show("Are you sure that you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.kinect.closeKinect();
                Application.ExitThread();﻿
            }

            else if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void labelGameMode_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Show();
            this.highscorePanel.Hide();
            this.gamePanel.Hide();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void kinectVideoTimer_Tick(object sender, EventArgs e)
        {
            kinect.updateKinect();

        }

        private void timerMoves_Tick(object sender, EventArgs e)
        {
            if (this.labelGameMode.Text == this.buttonForm1.Text )
            {

            }
            else if (this.labelGameMode.Text == this.buttonForm2.Text)
            {

            }
            else if (this.labelGameMode.Text == this.buttonForm3.Text)
            {
                
            }

            if (elapsedTime == 10)
            {
                if (pausedTime < 31)
                {
                    this.videoTutorial.Ctlcontrols.pause();
                    pausedTime++;
                }
                else if (pausedTime >= 31)
                {
                    // restart pause
                    this.videoTutorial.Ctlcontrols.play();
                    pausedTime = 0; 
                    elapsedTime++; 
                }

            }
            if (elapsedTime == 20)
            {
                if (pausedTime < 31)
                {
                    this.videoTutorial.Ctlcontrols.pause();
                    pausedTime++;
                }
                else if (pausedTime >= 31)
                {
                    // restart pause
                    this.videoTutorial.Ctlcontrols.play();
                    pausedTime = 0;
                    elapsedTime++;
                }

            }
            if (pausedTime == 0)
            {
                elapsedTime++;
            }
            
        }

    }
}
