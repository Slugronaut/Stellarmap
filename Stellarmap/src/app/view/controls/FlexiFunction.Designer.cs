namespace Stellarmap
	{
	partial class FlexiFunction
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			this.Label = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Label.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label.Location = new System.Drawing.Point(15,13);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(230,207);
			this.Label.TabIndex = 0;
			this.Label.TabStop = false;
			this.Label.Text = "groupBox1";
			// 
			// FlexiFunction
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Controls.Add(this.Label);
			this.Name = "FlexiFunction";
			this.Size = new System.Drawing.Size(262,235);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.GroupBox Label;

		}
	}
