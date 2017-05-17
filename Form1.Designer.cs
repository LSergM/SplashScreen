namespace media_player_demo
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.videoPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer();
            this.openkey = new System.Windows.Forms.PictureBox();
            this.playKey = new System.Windows.Forms.PictureBox();
            this.pauseKey = new System.Windows.Forms.PictureBox();
            this.stopKey = new System.Windows.Forms.PictureBox();
            this.muteKey = new System.Windows.Forms.PictureBox();
            this.lessVolumeKey = new System.Windows.Forms.PictureBox();
            this.moreVolumeKey = new System.Windows.Forms.PictureBox();
            this.removeKey = new System.Windows.Forms.PictureBox();
            this.statusBar = new System.Windows.Forms.TextBox();
            this.filePath = new System.Windows.Forms.TextBox();
            this.videoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoPanel
            // 
            this.videoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.videoPanel.Controls.Add(this.pictureBox1);
            this.videoPanel.Location = new System.Drawing.Point(18, 13);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(480, 320);
            this.videoPanel.DoubleClick += new System.EventHandler(this.ToggleFullScreen);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(189, 113);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 100);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.ToggleFullScreen);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // openkey
            // 
            this.openkey.Image = ((System.Drawing.Image)(resources.GetObject("openkey.Image")));
            this.openkey.Location = new System.Drawing.Point(18, 369);
            this.openkey.Name = "openkey";
            this.openkey.Size = new System.Drawing.Size(30, 23);
            this.openkey.Click += new System.EventHandler(this.openkey_Click);
            // 
            // playKey
            // 
            this.playKey.Image = ((System.Drawing.Image)(resources.GetObject("playKey.Image")));
            this.playKey.Location = new System.Drawing.Point(56, 369);
            this.playKey.Name = "playKey";
            this.playKey.Size = new System.Drawing.Size(30, 23);
            this.playKey.Click += new System.EventHandler(this.playKey_Click);
            // 
            // pauseKey
            // 
            this.pauseKey.Image = ((System.Drawing.Image)(resources.GetObject("pauseKey.Image")));
            this.pauseKey.Location = new System.Drawing.Point(94, 369);
            this.pauseKey.Name = "pauseKey";
            this.pauseKey.Size = new System.Drawing.Size(30, 23);
            this.pauseKey.Click += new System.EventHandler(this.pauseKey_Click);
            // 
            // stopKey
            // 
            this.stopKey.Image = ((System.Drawing.Image)(resources.GetObject("stopKey.Image")));
            this.stopKey.Location = new System.Drawing.Point(132, 369);
            this.stopKey.Name = "stopKey";
            this.stopKey.Size = new System.Drawing.Size(30, 23);
            this.stopKey.Click += new System.EventHandler(this.stopKey_Click);
            // 
            // muteKey
            // 
            this.muteKey.Image = ((System.Drawing.Image)(resources.GetObject("muteKey.Image")));
            this.muteKey.Location = new System.Drawing.Point(361, 367);
            this.muteKey.Name = "muteKey";
            this.muteKey.Size = new System.Drawing.Size(30, 23);
            this.muteKey.Click += new System.EventHandler(this.muteKey_Click);
            // 
            // lessVolumeKey
            // 
            this.lessVolumeKey.Image = ((System.Drawing.Image)(resources.GetObject("lessVolumeKey.Image")));
            this.lessVolumeKey.Location = new System.Drawing.Point(397, 367);
            this.lessVolumeKey.Name = "lessVolumeKey";
            this.lessVolumeKey.Size = new System.Drawing.Size(30, 23);
            this.lessVolumeKey.Click += new System.EventHandler(this.lessVolumeKey_Click);
            // 
            // moreVolumeKey
            // 
            this.moreVolumeKey.Image = ((System.Drawing.Image)(resources.GetObject("moreVolumeKey.Image")));
            this.moreVolumeKey.Location = new System.Drawing.Point(433, 367);
            this.moreVolumeKey.Name = "moreVolumeKey";
            this.moreVolumeKey.Size = new System.Drawing.Size(30, 23);
            this.moreVolumeKey.Click += new System.EventHandler(this.moreVolumeKey_Click);
            // 
            // removeKey
            // 
            this.removeKey.Image = ((System.Drawing.Image)(resources.GetObject("removeKey.Image")));
            this.removeKey.Location = new System.Drawing.Point(469, 367);
            this.removeKey.Name = "removeKey";
            this.removeKey.Size = new System.Drawing.Size(30, 23);
            this.removeKey.Click += new System.EventHandler(this.removeKey_Click);
            // 
            // statusBar
            // 
            this.statusBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusBar.Enabled = false;
            this.statusBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.statusBar.ForeColor = System.Drawing.Color.Black;
            this.statusBar.Location = new System.Drawing.Point(229, 340);
            this.statusBar.Multiline = true;
            this.statusBar.Name = "statusBar";
            this.statusBar.ReadOnly = true;
            this.statusBar.Size = new System.Drawing.Size(270, 21);
            this.statusBar.TabIndex = 48;
            this.statusBar.Text = "(Status)";
            // 
            // filePath
            // 
            this.filePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.filePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.filePath.ForeColor = System.Drawing.Color.Black;
            this.filePath.Location = new System.Drawing.Point(18, 340);
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(210, 21);
            this.filePath.TabIndex = 58;
            this.filePath.Text = "(No file loaded)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(516, 400);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.removeKey);
            this.Controls.Add(this.moreVolumeKey);
            this.Controls.Add(this.lessVolumeKey);
            this.Controls.Add(this.muteKey);
            this.Controls.Add(this.stopKey);
            this.Controls.Add(this.pauseKey);
            this.Controls.Add(this.playKey);
            this.Controls.Add(this.openkey);
            this.Controls.Add(this.videoPanel);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Video Playback in VC#";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_closing);
            this.videoPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox openkey;
        private System.Windows.Forms.PictureBox playKey;
        private System.Windows.Forms.PictureBox pauseKey;
        private System.Windows.Forms.PictureBox stopKey;
        private System.Windows.Forms.PictureBox muteKey;
        private System.Windows.Forms.PictureBox lessVolumeKey;
        private System.Windows.Forms.PictureBox moreVolumeKey;
        private System.Windows.Forms.PictureBox removeKey;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox statusBar;
        private System.Windows.Forms.TextBox filePath;
    }
}

