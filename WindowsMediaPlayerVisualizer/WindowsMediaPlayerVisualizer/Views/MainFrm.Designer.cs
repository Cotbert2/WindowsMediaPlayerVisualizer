using WindowsMediaPlayerVisualizer.Components;

namespace WindowsMediaPlayerVisualizer.Views
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            this.btnMusic = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblSong = new System.Windows.Forms.Label();
            this.lblArtist = new System.Windows.Forms.Label();
            this.barPlayer = new Guna.UI2.WinForms.Guna2TrackBar();
            this.btnBackward = new FontAwesome.Sharp.IconButton();
            this.btnForward = new FontAwesome.Sharp.IconButton();
            this.btnStop = new FontAwesome.Sharp.IconButton();
            this.btnPlay = new FontAwesome.Sharp.IconButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.canvas = new VisualizerCanvas();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMusic
            // 
            this.btnMusic.BackColor = System.Drawing.Color.Black;
            this.btnMusic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMusic.ForeColor = System.Drawing.Color.Black;
            this.btnMusic.IconChar = FontAwesome.Sharp.IconChar.Music;
            this.btnMusic.IconColor = System.Drawing.Color.White;
            this.btnMusic.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMusic.IconSize = 32;
            this.btnMusic.Location = new System.Drawing.Point(1184, 31);
            this.btnMusic.Name = "btnMusic";
            this.btnMusic.Size = new System.Drawing.Size(40, 40);
            this.btnMusic.TabIndex = 5;
            this.btnMusic.UseVisualStyleBackColor = true;
            this.btnMusic.Click += new System.EventHandler(this.btnMusic_click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Controls.Add(this.lblCountdown);
            this.panel1.Controls.Add(this.lblCounter);
            this.panel1.Controls.Add(this.lblSong);
            this.panel1.Controls.Add(this.lblArtist);
            this.panel1.Controls.Add(this.barPlayer);
            this.panel1.Controls.Add(this.btnBackward);
            this.panel1.Controls.Add(this.btnForward);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnPlay);
            this.panel1.Controls.Add(this.btnMusic);
            this.panel1.Location = new System.Drawing.Point(-31, 666);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1266, 140);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblCountdown
            // 
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCountdown.Location = new System.Drawing.Point(910, 106);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(35, 16);
            this.lblCountdown.TabIndex = 7;
            this.lblCountdown.Text = "-0:00";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCounter.Location = new System.Drawing.Point(441, 110);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(31, 16);
            this.lblCounter.TabIndex = 6;
            this.lblCounter.Text = "0:00";
            // 
            // lblSong
            // 
            this.lblSong.AutoSize = true;
            this.lblSong.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSong.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSong.Location = new System.Drawing.Point(62, 70);
            this.lblSong.Name = "lblSong";
            this.lblSong.Size = new System.Drawing.Size(0, 16);
            this.lblSong.TabIndex = 5;
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtist.ForeColor = System.Drawing.SystemColors.Control;
            this.lblArtist.Location = new System.Drawing.Point(59, 30);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(0, 32);
            this.lblArtist.TabIndex = 1;
            // 
            // barPlayer
            // 
            this.barPlayer.Location = new System.Drawing.Point(444, 74);
            this.barPlayer.Name = "barPlayer";
            this.barPlayer.Size = new System.Drawing.Size(487, 33);
            this.barPlayer.TabIndex = 4;
            this.barPlayer.ThumbColor = System.Drawing.Color.Azure;
            this.barPlayer.Value = 0;
            this.barPlayer.Scroll += new System.Windows.Forms.ScrollEventHandler(this.guna2TrackBar1_Scroll);
            // 
            // btnBackward
            // 
            this.btnBackward.BackColor = System.Drawing.Color.Black;
            this.btnBackward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackward.ForeColor = System.Drawing.Color.Black;
            this.btnBackward.IconChar = FontAwesome.Sharp.IconChar.Backward;
            this.btnBackward.IconColor = System.Drawing.Color.White;
            this.btnBackward.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBackward.IconSize = 32;
            this.btnBackward.Location = new System.Drawing.Point(444, 28);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(40, 40);
            this.btnBackward.TabIndex = 3;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnForward
            // 
            this.btnForward.BackColor = System.Drawing.Color.Black;
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.ForeColor = System.Drawing.Color.Black;
            this.btnForward.IconChar = FontAwesome.Sharp.IconChar.Forward;
            this.btnForward.IconColor = System.Drawing.Color.White;
            this.btnForward.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnForward.IconSize = 32;
            this.btnForward.Location = new System.Drawing.Point(866, 28);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(40, 40);
            this.btnForward.TabIndex = 2;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Black;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.Color.Black;
            this.btnStop.IconChar = FontAwesome.Sharp.IconChar.Stop;
            this.btnStop.IconColor = System.Drawing.Color.White;
            this.btnStop.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStop.IconSize = 32;
            this.btnStop.Location = new System.Drawing.Point(716, 28);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(40, 40);
            this.btnStop.TabIndex = 1;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Black;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.ForeColor = System.Drawing.Color.Black;
            this.btnPlay.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.btnPlay.IconColor = System.Drawing.Color.White;
            this.btnPlay.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPlay.IconSize = 32;
            this.btnPlay.Location = new System.Drawing.Point(652, 28);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(40, 40);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(-1, 1);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1233, 670);
            this.canvas.TabIndex = 1;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 803);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.panel1);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFrm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.onLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnPlay;
        private FontAwesome.Sharp.IconButton btnBackward;
        private FontAwesome.Sharp.IconButton btnForward;
        private Guna.UI2.WinForms.Guna2TrackBar barPlayer;
        private System.Windows.Forms.Timer timer1;
        private FontAwesome.Sharp.IconButton btnStop;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.Label lblSong;
        private FontAwesome.Sharp.IconButton btnMusic;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Panel canvas;
    }
}