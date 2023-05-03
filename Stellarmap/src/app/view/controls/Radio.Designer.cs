namespace Stellarmap
	{
	partial class Radio
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
            this.Label = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(3, 2);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(48, 17);
            this.Label.TabIndex = 1;
            this.Label.TabStop = true;
            this.Label.Text = "radio";
            this.Label.UseVisualStyleBackColor = true;
            // 
            // Radio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Label);
            this.Name = "Radio";
            this.Size = new System.Drawing.Size(88, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

        private System.Windows.Forms.RadioButton Label;

        }
	}
