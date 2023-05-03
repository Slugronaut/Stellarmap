namespace Stellarmap
	{
	partial class ComboListBuilder
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
			this.Entry = new System.Windows.Forms.ComboBox();
			this.Label = new System.Windows.Forms.GroupBox();
			this.EntryList = new System.Windows.Forms.ListBox();
			this.RemoveEntry = new System.Windows.Forms.Button();
			this.AddEntry = new System.Windows.Forms.Button();
			this.Label.SuspendLayout();
			this.SuspendLayout();
			// 
			// Entry
			// 
			this.Entry.FormattingEnabled = true;
			this.Entry.Location = new System.Drawing.Point(7, 19);
			this.Entry.Name = "Entry";
			this.Entry.Size = new System.Drawing.Size(202, 21);
			this.Entry.TabIndex = 1;	
			this.Entry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;		
			// 
			// Label
			// 
			this.Label.Controls.Add(this.Entry);
			this.Label.Controls.Add(this.EntryList);
			this.Label.Controls.Add(this.RemoveEntry);
			this.Label.Controls.Add(this.AddEntry);
			this.Label.Location = new System.Drawing.Point(3, 3);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(216, 198);			
			this.Label.TabStop = false;
			this.Label.Text = "List Builder";
			// 
			// EntryList
			// 
			this.EntryList.FormattingEnabled = true;
			this.EntryList.Location = new System.Drawing.Point(7, 75);
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(202, 108);
			this.EntryList.TabIndex = 4;			
			this.EntryList.HorizontalScrollbar = true;
			// 
			// RemoveEntry
			// 
			this.RemoveEntry.Location = new System.Drawing.Point(134, 45);
			this.RemoveEntry.Name = "RemoveEntry";
			this.RemoveEntry.Size = new System.Drawing.Size(75, 23);
			this.RemoveEntry.TabIndex = 3;
			this.RemoveEntry.Text = "Remove";
			this.RemoveEntry.UseVisualStyleBackColor = true;
			this.RemoveEntry.Click += new System.EventHandler(this.RemoveEntry_Click);
			// 
			// AddEntry
			// 
			this.AddEntry.Location = new System.Drawing.Point(7, 45);
			this.AddEntry.Name = "AddEntry";
			this.AddEntry.Size = new System.Drawing.Size(75, 23);
			this.AddEntry.TabIndex = 2;
			this.AddEntry.Text = "Add";
			this.AddEntry.UseVisualStyleBackColor = true;
			this.AddEntry.Click += new System.EventHandler(this.AddEntry_Click);
			// 
			// ComboListBuilder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Label);
			this.Name = "ComboListBuilder";
			this.Size = new System.Drawing.Size(225, 208);			
			this.Label.ResumeLayout(false);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ComboBox Entry;
		private System.Windows.Forms.GroupBox Label;
		private System.Windows.Forms.ListBox EntryList;
		private System.Windows.Forms.Button RemoveEntry;
		private System.Windows.Forms.Button AddEntry;
		}
	}
