namespace Stellarmap
	{
	partial class TextDump
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
			this.buttonEditWindow = new System.Windows.Forms.Button();
			this.TextEntry = new System.Windows.Forms.TextBox();
			this.Label.SuspendLayout();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.Controls.Add(this.buttonEditWindow);
			this.Label.Controls.Add(this.TextEntry);
			this.Label.Location = new System.Drawing.Point(4,4);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(247,84);
			this.Label.TabIndex = 0;
			this.Label.TabStop = false;
			this.Label.Text = "Text Dump";
			// 
			// buttonEditWindow
			// 
			this.buttonEditWindow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonEditWindow.Location = new System.Drawing.Point(223,6);
			this.buttonEditWindow.Name = "buttonEditWindow";
			this.buttonEditWindow.Size = new System.Drawing.Size(18,14);
			this.buttonEditWindow.TabIndex = 1;
			this.buttonEditWindow.Text = "...";
			this.buttonEditWindow.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonEditWindow.UseVisualStyleBackColor = true;
			this.buttonEditWindow.Click += new System.EventHandler(this.buttonEditWindow_Click);
			// 
			// TextEntry
			// 
			this.TextEntry.Location = new System.Drawing.Point(7,20);
			this.TextEntry.Multiline = true;
			this.TextEntry.Name = "TextEntry";
			this.TextEntry.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TextEntry.Size = new System.Drawing.Size(234,52);
			this.TextEntry.TabIndex = 0;
			// 
			// TextDump
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Label);
			this.Name = "TextDump";
			this.Size = new System.Drawing.Size(254,101);
			this.Label.ResumeLayout(false);
			this.Label.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.GroupBox Label;
		private System.Windows.Forms.TextBox TextEntry;
		private System.Windows.Forms.Button buttonEditWindow;
		}
	}
