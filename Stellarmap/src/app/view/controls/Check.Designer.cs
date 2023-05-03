namespace Stellarmap
	{
	partial class Check
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
			this.Label = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.AutoSize = true;
			this.Label.BackColor = System.Drawing.Color.Transparent;
			this.Label.Location = new System.Drawing.Point(4, 4);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(74, 17);
			this.Label.TabIndex = 0;
			this.Label.Text = "Checkbox";
			this.Label.UseVisualStyleBackColor = false;
			this.Label.CheckedChanged += new System.EventHandler(OnCheckedChanged);
			// 
			// CheckBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Label);
			this.Name = "CheckBox";
			this.Size = new System.Drawing.Size(88, 22);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.CheckBox Label;
		}
	}
