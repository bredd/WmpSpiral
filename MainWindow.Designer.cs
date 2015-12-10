namespace WmpSpiral
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.button_Play = new System.Windows.Forms.Button();
            this.button_Pause = new System.Windows.Forms.Button();
            this.button_Spiral = new System.Windows.Forms.Button();
            this.SpiralShuffle = new System.Windows.Forms.Button();
            this.FullShuffle = new System.Windows.Forms.Button();
            this.AlbumTrack = new System.Windows.Forms.Button();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // button_Play
            // 
            this.button_Play.Location = new System.Drawing.Point(172, 12);
            this.button_Play.Name = "button_Play";
            this.button_Play.Size = new System.Drawing.Size(75, 23);
            this.button_Play.TabIndex = 0;
            this.button_Play.Text = "Play";
            this.button_Play.UseVisualStyleBackColor = true;
            this.button_Play.Click += new System.EventHandler(this.button_Play_Click);
            // 
            // button_Pause
            // 
            this.button_Pause.Location = new System.Drawing.Point(172, 41);
            this.button_Pause.Name = "button_Pause";
            this.button_Pause.Size = new System.Drawing.Size(75, 23);
            this.button_Pause.TabIndex = 1;
            this.button_Pause.Text = "Pause";
            this.button_Pause.UseVisualStyleBackColor = true;
            this.button_Pause.Click += new System.EventHandler(this.button_Pause_Click);
            // 
            // button_Spiral
            // 
            this.button_Spiral.Location = new System.Drawing.Point(12, 12);
            this.button_Spiral.Name = "button_Spiral";
            this.button_Spiral.Size = new System.Drawing.Size(103, 23);
            this.button_Spiral.TabIndex = 2;
            this.button_Spiral.Text = "Spiral";
            this.ToolTips.SetToolTip(this.button_Spiral, "Play the first track of each album, then the second track of each, etc.");
            this.button_Spiral.UseVisualStyleBackColor = true;
            this.button_Spiral.Click += new System.EventHandler(this.button_Spiral_Click);
            // 
            // SpiralShuffle
            // 
            this.SpiralShuffle.Location = new System.Drawing.Point(12, 41);
            this.SpiralShuffle.Name = "SpiralShuffle";
            this.SpiralShuffle.Size = new System.Drawing.Size(103, 23);
            this.SpiralShuffle.TabIndex = 3;
            this.SpiralShuffle.Text = "Spiral Shuffle";
            this.ToolTips.SetToolTip(this.SpiralShuffle, "Play a random track from the first album, then a random track from the second and" +
        " so forth; playing one track from each album before returning to play another ra" +
        "ndom track frome each album.");
            this.SpiralShuffle.UseVisualStyleBackColor = true;
            this.SpiralShuffle.Click += new System.EventHandler(this.SpiralShuffle_Click);
            // 
            // FullShuffle
            // 
            this.FullShuffle.Location = new System.Drawing.Point(13, 71);
            this.FullShuffle.Name = "FullShuffle";
            this.FullShuffle.Size = new System.Drawing.Size(102, 23);
            this.FullShuffle.TabIndex = 4;
            this.FullShuffle.Text = "Full Shuffle";
            this.ToolTips.SetToolTip(this.FullShuffle, "Play all tracks in random order.");
            this.FullShuffle.UseVisualStyleBackColor = true;
            this.FullShuffle.Click += new System.EventHandler(this.FullShuffle_Click);
            // 
            // AlbumTrack
            // 
            this.AlbumTrack.Location = new System.Drawing.Point(13, 101);
            this.AlbumTrack.Name = "AlbumTrack";
            this.AlbumTrack.Size = new System.Drawing.Size(102, 23);
            this.AlbumTrack.TabIndex = 5;
            this.AlbumTrack.Text = "Traditional";
            this.ToolTips.SetToolTip(this.AlbumTrack, "Traditional ordering - play each full album in the original track order. Sort alb" +
        "ums alphabetically.");
            this.AlbumTrack.UseVisualStyleBackColor = true;
            this.AlbumTrack.Click += new System.EventHandler(this.AlbumTrack_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 134);
            this.Controls.Add(this.AlbumTrack);
            this.Controls.Add(this.FullShuffle);
            this.Controls.Add(this.SpiralShuffle);
            this.Controls.Add(this.button_Spiral);
            this.Controls.Add(this.button_Pause);
            this.Controls.Add(this.button_Play);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "WmpSpiral";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Play;
        private System.Windows.Forms.Button button_Pause;
        private System.Windows.Forms.Button button_Spiral;
        private System.Windows.Forms.Button SpiralShuffle;
        private System.Windows.Forms.Button FullShuffle;
        private System.Windows.Forms.Button AlbumTrack;
        private System.Windows.Forms.ToolTip ToolTips;
    }
}

