namespace Stellarmap
	{
	partial class MapBuilder
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
			this.EntryList = new System.Windows.Forms.ListBox();
			this.Key = new System.Windows.Forms.TextBox();
			this.RemoveEntry = new System.Windows.Forms.Button();
			this.AddEntry = new System.Windows.Forms.Button();
			this.Label = new System.Windows.Forms.GroupBox();
			this.Value = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.Label.SuspendLayout();
			this.SuspendLayout();
			// 
			// EntryList
			// 
			this.EntryList.FormattingEnabled = true;
			this.EntryList.HorizontalScrollbar = true;
			this.EntryList.Location = new System.Drawing.Point(7,75);
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(202,108);
			this.EntryList.TabIndex = 5;
			// 
			// Key
			// 
			this.Key.Location = new System.Drawing.Point(6,19);
			this.Key.Name = "Key";
			this.Key.Size = new System.Drawing.Size(137,20);
			this.Key.TabIndex = 1;
			// 
			// RemoveEntry
			// 
			this.RemoveEntry.Location = new System.Drawing.Point(115,45);
			this.RemoveEntry.Name = "RemoveEntry";
			this.RemoveEntry.Size = new System.Drawing.Size(75,23);
			this.RemoveEntry.TabIndex = 4;
			this.RemoveEntry.Text = "Remove";
			this.RemoveEntry.UseVisualStyleBackColor = true;
			this.RemoveEntry.Click += new System.EventHandler(this.RemoveEntry_Click);
			// 
			// AddEntry
			// 
			this.AddEntry.Location = new System.Drawing.Point(7,45);
			this.AddEntry.Name = "AddEntry";
			this.AddEntry.Size = new System.Drawing.Size(75,23);
			this.AddEntry.TabIndex = 3;
			this.AddEntry.Text = "Add";
			this.AddEntry.UseVisualStyleBackColor = true;
			this.AddEntry.Click += new System.EventHandler(this.AddEntry_Click);
			// 
			// Label
			// 
			this.Label.Controls.Add(this.button2);
			this.Label.Controls.Add(this.button1);
			this.Label.Controls.Add(this.Value);
			this.Label.Controls.Add(this.EntryList);
			this.Label.Controls.Add(this.Key);
			this.Label.Controls.Add(this.RemoveEntry);
			this.Label.Controls.Add(this.AddEntry);
			this.Label.Location = new System.Drawing.Point(3,3);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(216,198);
			this.Label.TabIndex = 6;
			this.Label.TabStop = false;
			this.Label.Text = "Map Builder:";
			// 
			// Value
			// 
			this.Value.Location = new System.Drawing.Point(149,19);
			this.Value.Name = "Value";
			this.Value.Size = new System.Drawing.Size(60,20);
			this.Value.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(129,22);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(12,15);
			this.button1.TabIndex = 6;
			this.button1.Text = ":";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(195,22);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(12,15);
			this.button2.TabIndex = 7;
			this.button2.Text = ":";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// MapBuilder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Label);
			this.Name = "MapBuilder";
			this.Size = new System.Drawing.Size(227,209);
			this.Label.ResumeLayout(false);
			this.Label.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ListBox EntryList;
		private System.Windows.Forms.TextBox Key;
		private System.Windows.Forms.Button RemoveEntry;
		private System.Windows.Forms.Button AddEntry;
		private System.Windows.Forms.GroupBox Label;
		private System.Windows.Forms.TextBox Value;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		}
	}
