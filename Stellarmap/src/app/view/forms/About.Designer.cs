namespace Stellarmap
	{
	partial class About
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
			if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkDeadSouls = new System.Windows.Forms.LinkLabel();
            this.linkStellarmass = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkKeetah = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkHunspell = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.StellarmapVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stellarmap: A visual editor for";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Created by:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Icons and Art by:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "- An upcoming Scifi mud based on Dead Souls";
            // 
            // linkDeadSouls
            // 
            this.linkDeadSouls.AutoSize = true;
            this.linkDeadSouls.LinkArea = new System.Windows.Forms.LinkArea(0, 10);
            this.linkDeadSouls.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkDeadSouls.Location = new System.Drawing.Point(46, 61);
            this.linkDeadSouls.Name = "linkDeadSouls";
            this.linkDeadSouls.Size = new System.Drawing.Size(62, 13);
            this.linkDeadSouls.TabIndex = 5;
            this.linkDeadSouls.TabStop = true;
            this.linkDeadSouls.Text = "Dead Souls";
            this.linkDeadSouls.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDeadSouls_LinkClicked);
            // 
            // linkStellarmass
            // 
            this.linkStellarmass.AutoSize = true;
            this.linkStellarmass.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkStellarmass.Location = new System.Drawing.Point(152, 9);
            this.linkStellarmass.Name = "linkStellarmass";
            this.linkStellarmass.Size = new System.Drawing.Size(60, 13);
            this.linkStellarmass.TabIndex = 6;
            this.linkStellarmass.TabStop = true;
            this.linkStellarmass.Text = "Stellarmass";
            this.linkStellarmass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkStellarmass_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(287, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // linkKeetah
            // 
            this.linkKeetah.AutoSize = true;
            this.linkKeetah.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkKeetah.Location = new System.Drawing.Point(92, 33);
            this.linkKeetah.Name = "linkKeetah";
            this.linkKeetah.Size = new System.Drawing.Size(41, 13);
            this.linkKeetah.TabIndex = 8;
            this.linkKeetah.TabStop = true;
            this.linkKeetah.Text = "Keetah";
            this.linkKeetah.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkKeetah_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkHunspell);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.linkKeetah);
            this.groupBox1.Controls.Add(this.linkDeadSouls);
            this.groupBox1.Location = new System.Drawing.Point(33, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 107);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Thanks";
            // 
            // linkHunspell
            // 
            this.linkHunspell.AutoSize = true;
            this.linkHunspell.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkHunspell.Location = new System.Drawing.Point(80, 85);
            this.linkHunspell.Name = "linkHunspell";
            this.linkHunspell.Size = new System.Drawing.Size(56, 13);
            this.linkHunspell.TabIndex = 11;
            this.linkHunspell.TabStop = true;
            this.linkHunspell.Text = "NHunspell";
            this.linkHunspell.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHunspell_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Spell Checker:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mudlib:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.Location = new System.Drawing.Point(98, 37);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(39, 13);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Sluggy";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(197, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "For Support Email: stellarmap@space-madness.net";
            // 
            // StellarmapVersion
            // 
            this.StellarmapVersion.AutoSize = true;
            this.StellarmapVersion.Location = new System.Drawing.Point(39, 61);
            this.StellarmapVersion.Name = "StellarmapVersion";
            this.StellarmapVersion.Size = new System.Drawing.Size(29, 13);
            this.StellarmapVersion.TabIndex = 14;
            this.StellarmapVersion.Text = "Ver: ";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 206);
            this.Controls.Add(this.StellarmapVersion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkStellarmass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "About";
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkDeadSouls;
		private System.Windows.Forms.LinkLabel linkStellarmass;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.LinkLabel linkKeetah;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.LinkLabel linkHunspell;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label StellarmapVersion;
		}
	}