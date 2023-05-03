namespace Stellarmap
	{
	partial class RadioList
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
            this.Label = new System.Windows.Forms.GroupBox();
            this.CheckedList = new System.Windows.Forms.RadioListBox();
            this.Label.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label
            // 
            this.Label.BackColor = System.Drawing.Color.Transparent;
            this.Label.Controls.Add(this.CheckedList);
            this.Label.Location = new System.Drawing.Point(0, 1);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(248, 172);
            this.Label.TabIndex = 0;
            this.Label.TabStop = false;
            this.Label.Text = "Radio List:";
            // 
            // CheckedList
            // 
            this.CheckedList.BackColor = System.Drawing.SystemColors.Window;
            this.CheckedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CheckedList.FormattingEnabled = true;
            this.CheckedList.Location = new System.Drawing.Point(6, 19);
            this.CheckedList.Name = "CheckedList";
            this.CheckedList.Size = new System.Drawing.Size(236, 147);
            this.CheckedList.TabIndex = 1;
            // 
            // RadioList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Label);
            this.Name = "RadioList";
            this.Size = new System.Drawing.Size(250, 176);
            this.Label.ResumeLayout(false);
            this.ResumeLayout(false);

			}

		#endregion

        private System.Windows.Forms.GroupBox Label;
        private System.Windows.Forms.RadioListBox CheckedList;
		}
	}
