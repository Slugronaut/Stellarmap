namespace Stellarmap
	{
	partial class DoubleComboMap
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
			this.Key = new System.Windows.Forms.ComboBox();
			this.EntryList = new System.Windows.Forms.ListBox();
			this.RemoveEntry = new System.Windows.Forms.Button();
			this.AddEntry = new System.Windows.Forms.Button();
			this.Value = new System.Windows.Forms.ComboBox();
			this.Label.SuspendLayout();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.Controls.Add(this.Value);
			this.Label.Controls.Add(this.Key);
			this.Label.Controls.Add(this.EntryList);
			this.Label.Controls.Add(this.RemoveEntry);
			this.Label.Controls.Add(this.AddEntry);
			this.Label.Location = new System.Drawing.Point(3, 3);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(216, 198);
			this.Label.TabIndex = 0;
			this.Label.TabStop = false;
			this.Label.Text = "Map Builder";
			// 
			// Key
			// 
			this.Key.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Key.FormattingEnabled = true;
			this.Key.Location = new System.Drawing.Point(7, 19);
			this.Key.Name = "Key";
			this.Key.Size = new System.Drawing.Size(106, 21);
			this.Key.TabIndex = 1;
			this.Key.DropDownWidth = 350;
			// 
			// EntryList
			// 
			this.EntryList.FormattingEnabled = true;
			this.EntryList.HorizontalScrollbar = true;
			this.EntryList.Location = new System.Drawing.Point(7, 75);
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(202, 108);
			this.EntryList.TabIndex = 5;
			// 
			// RemoveEntry
			// 
			this.RemoveEntry.Location = new System.Drawing.Point(115, 45);
			this.RemoveEntry.Name = "RemoveEntry";
			this.RemoveEntry.Size = new System.Drawing.Size(75, 23);
			this.RemoveEntry.TabIndex = 4;
			this.RemoveEntry.Text = "Remove";
			this.RemoveEntry.UseVisualStyleBackColor = true;
			this.RemoveEntry.Click += new System.EventHandler(RemoveEntry_Click);
			// 
			// AddEntry
			// 
			this.AddEntry.Location = new System.Drawing.Point(7, 45);
			this.AddEntry.Name = "AddEntry";
			this.AddEntry.Size = new System.Drawing.Size(75, 23);
			this.AddEntry.TabIndex = 3;
			this.AddEntry.Text = "Add";
			this.AddEntry.UseVisualStyleBackColor = true;
			this.AddEntry.Click += new System.EventHandler(AddEntry_Click);
			// 
			// Value
			// 
			this.Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Value.FormattingEnabled = true;
			this.Value.Location = new System.Drawing.Point(119, 19);
			this.Value.Name = "Value";
			this.Value.Size = new System.Drawing.Size(91, 21);
			this.Value.TabIndex = 2;
			this.Value.DropDownWidth = 350;
			// 
			// DoubleComboMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Label);
			this.Name = "DoubleComboMap";
			this.Size = new System.Drawing.Size(229, 211);
			this.Label.ResumeLayout(false);
			this.ResumeLayout(false);

			}
		#endregion

		private System.Windows.Forms.GroupBox Label;
		private System.Windows.Forms.ListBox EntryList;
		private System.Windows.Forms.Button RemoveEntry;
		private System.Windows.Forms.Button AddEntry;
		private System.Windows.Forms.ComboBox Key;
		private System.Windows.Forms.ComboBox Value;
		}
	}
