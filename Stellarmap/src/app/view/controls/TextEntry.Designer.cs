namespace Stellarmap
	{
	partial class TextEntry
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
			this.Entry = new System.Windows.Forms.TextBox();
			this.buttonEditWindow = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.AutoSize = true;
			this.Label.Location = new System.Drawing.Point(4,6);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(58,13);
			this.Label.TabIndex = 0;
			this.Label.Text = "Text Entry:";
			// 
			// Entry
			// 
			this.Entry.Location = new System.Drawing.Point(72,3);
			this.Entry.Name = "Entry";
			this.Entry.Size = new System.Drawing.Size(100,20);
			this.Entry.TabIndex = 1;
			// 
			// buttonEditWindow
			// 
			this.buttonEditWindow.Location = new System.Drawing.Point(172,3);
			this.buttonEditWindow.Name = "buttonEditWindow";
			this.buttonEditWindow.TabIndex = 2;
			this.buttonEditWindow.Text = ":";
			this.buttonEditWindow.UseVisualStyleBackColor = true;
			this.buttonEditWindow.Click += new System.EventHandler(this.buttonEditWindow_Click);
			// 
			// TextEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.buttonEditWindow);
			this.Controls.Add(this.Entry);
			this.Controls.Add(this.Label);
			this.Name = "TextEntry";
			this.Size = new System.Drawing.Size(208,26);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label Label;
		private System.Windows.Forms.TextBox Entry;
		private System.Windows.Forms.Button buttonEditWindow;
		}
	}
