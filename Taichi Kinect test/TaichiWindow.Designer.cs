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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_start = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_videostream)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_connection_status
            // 
            this.label_connection_status.AutoSize = true;
            this.label_connection_status.Location = new System.Drawing.Point(3, 0);
            this.label_connection_status.Name = "label_connection_status";
            this.label_connection_status.Size = new System.Drawing.Size(17, 13);
            this.label_connection_status.TabIndex = 0;
            this.label_connection_status.Text = "lol";
            // 
            // button_up
            // 
            this.button_up.Location = new System.Drawing.Point(3, 3);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(58, 23);
            this.button_up.TabIndex = 1;
            this.button_up.Text = "up";
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // button_down
            // 
            this.button_down.Location = new System.Drawing.Point(3, 76);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(58, 23);
            this.button_down.TabIndex = 2;
            this.button_down.Text = "down";
            this.button_down.UseVisualStyleBackColor = true;
            this.button_down.Click += new System.EventHandler(this.button_down_Click);
            // 
            // timer_info_update
            // 
            this.timer_info_update.Interval = 50;
            this.timer_info_update.Tick += new System.EventHandler(this.timerElapsedEvent);
            // 
            // pictureBox_videostream
            // 
            this.pictureBox_videostream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_videostream.Location = new System.Drawing.Point(65, 3);
            this.pictureBox_videostream.Name = "pictureBox_videostream";
            this.pictureBox_videostream.Size = new System.Drawing.Size(537, 395);
            this.pictureBox_videostream.TabIndex = 3;
            this.pictureBox_videostream.TabStop = false;
            this.pictureBox_videostream.Click += new System.EventHandler(this.pictureBox_videostream_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 543F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label_connection_status, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_videostream, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(675, 401);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button_up, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_down, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_start, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(608, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(64, 192);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(3, 129);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(58, 28);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // TaichiWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 401);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TaichiWindow";
            this.Text = "Taichi Window";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaichiWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_videostream)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Label label_connection_status;
        private Button button_up;
        private Button button_down;
        private Timer timer_info_update;
        private PictureBox pictureBox_videostream;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btn_start;

    }
}

