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
       // Cursor myCursor;

        public TaichiFitness()
        {
            InitializeComponent();
             //myCursor = new Cursor("myCursor.cur");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string misc = "./misc";
            if (!System.IO.Directory.Exists(misc))
            {
                System.IO.Directory.CreateDirectory(misc);
            }
            System.Media.SoundPlayer player = new System.Media.SoundPlayer("./misc/nature_sounds.wav");
            player.PlayLooping();
            //player.Play();
            //Cursor myCursor = new Cursor("myCursor.cur");
            //myControl.Cursor = myCursor;
            //this.Cursor = Cursor.

            /*
             * TODO play intro audio
             */
            this.moveListPanel.Hide();
            this.mainMenuPanel.Show();
            this.highscorePanel.Hide();
            this.gamePanel.Hide();          
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
            // Play GoodBye
            //this.mainMenuTableLayout.Hide();
            Application.Exit();
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
            if (labelGameMode.Text == "Form 1")
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Hide();
                this.highscorePanel.Hide();
                this.gamePanel.Show();
                this.labelGameMode.Text = "Form 2";
                this.videoTutorial.URL = "./misc/basic_move.wmv";
            }
            else if (labelGameMode.Text == "Form 2")
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Hide();
                this.highscorePanel.Hide();
                this.gamePanel.Show();
                this.labelGameMode.Text = "Form 3";
                this.videoTutorial.URL = "./misc/basic_move.wmv";
            }
            else if (labelGameMode.Text == "Form 3")
            {
                this.moveListPanel.Hide();
                this.mainMenuPanel.Hide();
                this.highscorePanel.Hide();
                this.gamePanel.Show();
                this.labelGameMode.Text = "Form 1";
                this.videoTutorial.URL = "./misc/basic_move.wmv";
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
            this.videoTutorial.URL = "./misc/tutorial.wmv";
        }
        private void startForm1_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Show();
            this.labelGameMode.Text = "Form 1";
            this.videoTutorial.URL = "./misc/basic_move.wmv";
        }
        private void startForm2_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Show();
            this.labelGameMode.Text = "Form 2";
            this.videoTutorial.URL = "./misc/basic_move.wmv";
        }
        private void startForm3_Click(object sender, EventArgs e)
        {
            this.moveListPanel.Hide();
            this.mainMenuPanel.Hide();
            this.highscorePanel.Hide();
            this.gamePanel.Show();
            this.labelGameMode.Text = "Form 3";
            this.videoTutorial.URL = "./misc/basic_move.wmv";
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

    }
}
