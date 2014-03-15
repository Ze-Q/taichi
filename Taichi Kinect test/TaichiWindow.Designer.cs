using System.Windows.Forms;
namespace TaichiKinect
{
    partial class TaichiWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void setText(string text)
        {
            this.label_connection_status.Text = text;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_connection_status = new System.Windows.Forms.Label();
            this.button_up = new System.Windows.Forms.Button();
            this.button_down = new System.Windows.Forms.Button();
            this.timer_info_update = new System.Windows.Forms.Timer(this.components);
            this.pictureBox_videostream = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_videostream)).BeginInit();
            this.SuspendLayout();
            // 
            // label_connection_status
            // 
            this.label_connection_status.AutoSize = true;
            this.label_connection_status.Location = new System.Drawing.Point(12, 77);
            this.label_connection_status.Name = "label_connection_status";
            this.label_connection_status.Size = new System.Drawing.Size(17, 13);
            this.label_connection_status.TabIndex = 0;
            this.label_connection_status.Text = "lol";
            // 
            // button_up
            // 
            this.button_up.Location = new System.Drawing.Point(576, 25);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(75, 23);
            this.button_up.TabIndex = 1;
            this.button_up.Text = "up";
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // button_down
            // 
            this.button_down.Location = new System.Drawing.Point(576, 71);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(75, 23);
            this.button_down.TabIndex = 2;
            this.button_down.Text = "down";
            this.button_down.UseVisualStyleBackColor = true;
            this.button_down.Click += new System.EventHandler(this.button_down_Click);
            // 
            // timer_info_update
            // 
            this.timer_info_update.Enabled = true;
            this.timer_info_update.Interval = 50;
            this.timer_info_update.Tick += new System.EventHandler(this.timerElapsedEvent);
            // 
            // pictureBox_videostream
            // 
            this.pictureBox_videostream.Location = new System.Drawing.Point(174, 98);
            this.pictureBox_videostream.Name = "pictureBox_videostream";
            this.pictureBox_videostream.Size = new System.Drawing.Size(287, 245);
            this.pictureBox_videostream.TabIndex = 3;
            this.pictureBox_videostream.TabStop = false;
            // 
            // TaichiWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 401);
            this.Controls.Add(this.pictureBox_videostream);
            this.Controls.Add(this.button_down);
            this.Controls.Add(this.button_up);
            this.Controls.Add(this.label_connection_status);
            this.Name = "TaichiWindow";
            this.Text = "Taichi Window";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaichiWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_videostream)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Label label_connection_status;
        private Button button_up;
        private Button button_down;
        private Timer timer_info_update;
        private PictureBox pictureBox_videostream;

    }
}

