namespace Stellarmap
	{
	partial class CheckList
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
			this.CheckedList = new System.Windows.Forms.CheckedListBox();
			this.Label.SuspendLayout();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.BackColor = System.Drawing.Color.Transparent;
			this.Label.Controls.Add(this.CheckedList);
			this.Label.Location = new System.Drawing.Point(0,1);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(248,172);
			this.Label.TabIndex = 0;
			this.Label.TabStop = false;
			this.Label.Text = "Check List:";
			// 
			// CheckedList
			// 
			this.CheckedList.FormattingEnabled = true;
			this.CheckedList.Location = new System.Drawing.Point(7,20);
			this.CheckedList.Name = "CheckedList";
			this.CheckedList.Size = new System.Drawing.Size(235,139);
			this.CheckedList.TabIndex = 0;
			this.CheckedList.SelectedIndexChanged += new System.EventHandler(this.CheckedList_SelectedIndexChanged);
			this.CheckedList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedList_ItemChecked);
			// 
			// CheckList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Label);
			this.Name = "CheckList";
			this.Size = new System.Drawing.Size(250,176);
			this.Label.ResumeLayout(false);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.GroupBox Label;
		private System.Windows.Forms.CheckedListBox CheckedList;
		}
	}
