namespace Stellarmap
	{
	partial class DomainOptionsWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.RoomImageWidth = new System.Windows.Forms.NumericUpDown();
			this.RoomImageHeight = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ExitNodeWidth = new System.Windows.Forms.NumericUpDown();
			this.ExitNodeHeight = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ExitLineWidth = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.Ok = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBaseRoomName = new System.Windows.Forms.TextBox();
			this.textStartRoomName = new System.Windows.Forms.TextBox();
			this.comboEncoding = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.comboLineEndings = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.RoomImageWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RoomImageHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ExitNodeWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ExitNodeHeight)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ExitLineWidth)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6,28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101,13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Room Image Width:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6,54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104,13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Room Image Height:";
			// 
			// RoomImageWidth
			// 
			this.RoomImageWidth.Location = new System.Drawing.Point(113,26);
			this.RoomImageWidth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.RoomImageWidth.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.RoomImageWidth.Name = "RoomImageWidth";
			this.RoomImageWidth.Size = new System.Drawing.Size(75,20);
			this.RoomImageWidth.TabIndex = 4;
			this.RoomImageWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			// 
			// RoomImageHeight
			// 
			this.RoomImageHeight.Location = new System.Drawing.Point(113,52);
			this.RoomImageHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.RoomImageHeight.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.RoomImageHeight.Name = "RoomImageHeight";
			this.RoomImageHeight.Size = new System.Drawing.Size(75,20);
			this.RoomImageHeight.TabIndex = 5;
			this.RoomImageHeight.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6,126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90,13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Exit Node Height:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6,100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87,13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Exit Node Width:";
			// 
			// ExitNodeWidth
			// 
			this.ExitNodeWidth.Location = new System.Drawing.Point(113,98);
			this.ExitNodeWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.ExitNodeWidth.Name = "ExitNodeWidth";
			this.ExitNodeWidth.Size = new System.Drawing.Size(75,20);
			this.ExitNodeWidth.TabIndex = 8;
			this.ExitNodeWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// ExitNodeHeight
			// 
			this.ExitNodeHeight.Location = new System.Drawing.Point(113,124);
			this.ExitNodeHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.ExitNodeHeight.Name = "ExitNodeHeight";
			this.ExitNodeHeight.Size = new System.Drawing.Size(75,20);
			this.ExitNodeHeight.TabIndex = 9;
			this.ExitNodeHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ExitLineWidth);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.ExitNodeHeight);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.ExitNodeWidth);
			this.groupBox1.Controls.Add(this.RoomImageWidth);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.RoomImageHeight);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(207,199);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Metrics";
			// 
			// ExitLineWidth
			// 
			this.ExitLineWidth.Location = new System.Drawing.Point(113,165);
			this.ExitLineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.ExitLineWidth.Name = "ExitLineWidth";
			this.ExitLineWidth.Size = new System.Drawing.Size(75,20);
			this.ExitLineWidth.TabIndex = 11;
			this.ExitLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6,167);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(81,13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Exit Line Width:";
			// 
			// Ok
			// 
			this.Ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Ok.Location = new System.Drawing.Point(276,217);
			this.Ok.Name = "Ok";
			this.Ok.Size = new System.Drawing.Size(75,23);
			this.Ok.TabIndex = 11;
			this.Ok.Text = "Ok";
			this.Ok.UseVisualStyleBackColor = true;
			this.Ok.Click += new System.EventHandler(this.Ok_Click);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(357,217);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75,23);
			this.Cancel.TabIndex = 12;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textBaseRoomName);
			this.groupBox2.Controls.Add(this.textStartRoomName);
			this.groupBox2.Location = new System.Drawing.Point(225,12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(207,90);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Default Names";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8,54);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96,13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Base Room Name:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8,28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(63,13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Start Room:";
			// 
			// textBaseRoomName
			// 
			this.textBaseRoomName.Location = new System.Drawing.Point(101,51);
			this.textBaseRoomName.Name = "textBaseRoomName";
			this.textBaseRoomName.Size = new System.Drawing.Size(100,20);
			this.textBaseRoomName.TabIndex = 1;
			// 
			// textStartRoomName
			// 
			this.textStartRoomName.Location = new System.Drawing.Point(77,25);
			this.textStartRoomName.Name = "textStartRoomName";
			this.textStartRoomName.Size = new System.Drawing.Size(124,20);
			this.textStartRoomName.TabIndex = 0;
			// 
			// comboEncoding
			// 
			this.comboEncoding.DropDownWidth = 300;
			this.comboEncoding.FormattingEnabled = true;
			this.comboEncoding.Location = new System.Drawing.Point(42,23);
			this.comboEncoding.Name = "comboEncoding";
			this.comboEncoding.Size = new System.Drawing.Size(155,21);
			this.comboEncoding.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6,26);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(30,13);
			this.label8.TabIndex = 15;
			this.label8.Text = "LPC:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboLineEndings);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.comboEncoding);
			this.groupBox3.Location = new System.Drawing.Point(228,112);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(203,99);
			this.groupBox3.TabIndex = 16;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Encoding Info:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6,53);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(71,13);
			this.label9.TabIndex = 16;
			this.label9.Text = "Line Endings:";
			// 
			// comboLineEndings
			// 
			this.comboLineEndings.FormattingEnabled = true;
			this.comboLineEndings.Items.AddRange(new object[] {
            "Unix (LF)",
            "Mac (CR)",
            "Windows (CR+LF)"});
			this.comboLineEndings.Location = new System.Drawing.Point(83,50);
			this.comboLineEndings.Name = "comboLineEndings";
			this.comboLineEndings.Size = new System.Drawing.Size(114,21);
			this.comboLineEndings.TabIndex = 17;
			// 
			// DomainOptionsWindow
			// 
			this.AcceptButton = this.Ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(443,248);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Ok);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DomainOptionsWindow";
			this.ShowIcon = false;
			this.Text = "DomainOptionsWindow";
			((System.ComponentModel.ISupportInitialize)(this.RoomImageWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RoomImageHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ExitNodeWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ExitNodeHeight)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ExitLineWidth)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown RoomImageWidth;
		private System.Windows.Forms.NumericUpDown RoomImageHeight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown ExitNodeWidth;
		private System.Windows.Forms.NumericUpDown ExitNodeHeight;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Ok;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBaseRoomName;
		private System.Windows.Forms.TextBox textStartRoomName;
		private System.Windows.Forms.NumericUpDown ExitLineWidth;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboEncoding;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox comboLineEndings;
		private System.Windows.Forms.Label label9;
		}
	}