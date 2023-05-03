namespace Stellarmap
	{
	partial class ListBuilder
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
			this.EntryList = new System.Windows.Forms.ListBox();
			this.Entry = new System.Windows.Forms.TextBox();
			this.RemoveEntry = new System.Windows.Forms.Button();
			this.AddEntry = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.Label.SuspendLayout();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.Controls.Add(this.button1);
			this.Label.Controls.Add(this.EntryList);
			this.Label.Controls.Add(this.Entry);
			this.Label.Controls.Add(this.RemoveEntry);
			this.Label.Controls.Add(this.AddEntry);
			this.Label.Location = new System.Drawing.Point(1,1);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(198,198);
			this.Label.TabIndex = 0;
			this.Label.TabStop = false;
			this.Label.Text = "List Builder:";
			// 
			// EntryList
			// 
			this.EntryList.FormattingEnabled = true;
			this.EntryList.Location = new System.Drawing.Point(7,75);
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(183,108);
			this.EntryList.TabIndex = 4;
			// 
			// Entry
			// 
			this.Entry.Location = new System.Drawing.Point(6,19);
			this.Entry.Name = "Entry";
			this.Entry.Size = new System.Drawing.Size(184,20);
			this.Entry.TabIndex = 1;
			// 
			// RemoveEntry
			// 
			this.RemoveEntry.Location = new System.Drawing.Point(115,45);
			this.RemoveEntry.Name = "RemoveEntry";
			this.RemoveEntry.Size = new System.Drawing.Size(75,23);
			this.RemoveEntry.TabIndex = 3;
			this.RemoveEntry.Text = "Remove";
			this.RemoveEntry.UseVisualStyleBackColor = true;
			this.RemoveEntry.Click += new System.EventHandler(this.RemoveEntry_Click);
			// 
			// AddEntry
			// 
			this.AddEntry.Location = new System.Drawing.Point(7,45);
			this.AddEntry.Name = "AddEntry";
			this.AddEntry.Size = new System.Drawing.Size(75,23);
			this.AddEntry.TabIndex = 2;
			this.AddEntry.Text = "Add";
			this.AddEntry.UseVisualStyleBackColor = true;
			this.AddEntry.Click += new System.EventHandler(this.AddEntry_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(175,22);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(12,15);
			this.button1.TabIndex = 5;
			this.button1.Text = ":";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ListBuilder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Label);
			this.Name = "ListBuilder";
			this.Size = new System.Drawing.Size(200,200);
			this.Label.ResumeLayout(false);
			this.Label.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.GroupBox Label;
		private System.Windows.Forms.Button AddEntry;
		private System.Windows.Forms.TextBox Entry;
		private System.Windows.Forms.Button RemoveEntry;
		private System.Windows.Forms.ListBox EntryList;
		private System.Windows.Forms.Button button1;
		}
	}
