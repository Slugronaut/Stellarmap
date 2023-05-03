namespace Stellarmap
	{
	partial class ComboSelectionTypeable
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
			this.Entry = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.AutoSize = true;
			this.Label.Location = new System.Drawing.Point(3, 6);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(90, 13);
			this.Label.TabIndex = 2;
			this.Label.Text = "Combo Selection:";
			// 
			// Entry
			// 
			this.Entry.FormattingEnabled = true;
			this.Entry.Location = new System.Drawing.Point(99, 3);
			this.Entry.Name = "Entry";
			this.Entry.Size = new System.Drawing.Size(136, 21);
			this.Entry.TabIndex = 3;
			this.Entry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			// 
			// ComboSelection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Entry);
			this.Controls.Add(this.Label);
			this.Name = "ComboSelection";
			this.Size = new System.Drawing.Size(240, 26);			
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label Label;
		private System.Windows.Forms.ComboBox Entry;
		}
	}
