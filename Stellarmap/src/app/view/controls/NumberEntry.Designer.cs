namespace Stellarmap
	{
	partial class NumberEntry
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
            this.Label = new System.Windows.Forms.Label();
            this.Entry = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.Entry)).BeginInit();
            this.SuspendLayout();
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(4, 6);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(74, 13);
            this.Label.TabIndex = 0;
            this.Label.Text = "Number Entry:";
            // 
            // Entry
            // 
            this.Entry.Location = new System.Drawing.Point(84, 3);
            this.Entry.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Entry.Name = "Entry";
            this.Entry.Size = new System.Drawing.Size(100, 20);
            this.Entry.TabIndex = 1;
            this.Entry.ThousandsSeparator = true;
            // 
            // NumberEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Entry);
            this.Controls.Add(this.Label);
            this.Name = "NumberEntry";
            this.Size = new System.Drawing.Size(204, 26);
            ((System.ComponentModel.ISupportInitialize)(this.Entry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label Label;
		private System.Windows.Forms.NumericUpDown Entry;
		}
	}
