namespace Stellarmap
	{
	partial class BasicTextEditor
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
			if(disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
            this.components = new System.ComponentModel.Container();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.contextRecommendSpelling = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolstripRecommendSpelling = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolstrinpCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstripCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstripPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllCtrAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCharacterSizesChart = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new Stellarmap.FlickerFreeRichEditTextBox();
            this.contextRecommendSpelling.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(508, 549);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.TabStop = false;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(601, 549);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // contextRecommendSpelling
            // 
            this.contextRecommendSpelling.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripRecommendSpelling,
            this.toolStripSeparator1,
            this.toolstrinpCut,
            this.toolstripCopy,
            this.toolstripPaste,
            this.selectAllCtrAToolStripMenuItem});
            this.contextRecommendSpelling.Name = "contextRecommendSpelling";
            this.contextRecommendSpelling.Size = new System.Drawing.Size(201, 120);
            this.contextRecommendSpelling.Opening += new System.ComponentModel.CancelEventHandler(this.contextRecommendSpelling_Opening);
            // 
            // toolstripRecommendSpelling
            // 
            this.toolstripRecommendSpelling.Name = "toolstripRecommendSpelling";
            this.toolstripRecommendSpelling.Size = new System.Drawing.Size(200, 22);
            this.toolstripRecommendSpelling.Text = "Recommended Spelling";
            this.toolstripRecommendSpelling.Click += new System.EventHandler(this.titleSpelling_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // toolstrinpCut
            // 
            this.toolstrinpCut.Name = "toolstrinpCut";
            this.toolstrinpCut.Size = new System.Drawing.Size(200, 22);
            this.toolstrinpCut.Text = "Cut         Ctr+X";
            this.toolstrinpCut.Click += new System.EventHandler(this.toolstrinpCut_Click);
            // 
            // toolstripCopy
            // 
            this.toolstripCopy.Name = "toolstripCopy";
            this.toolstripCopy.Size = new System.Drawing.Size(200, 22);
            this.toolstripCopy.Text = "Copy      Ctr+C";
            this.toolstripCopy.Click += new System.EventHandler(this.toolstripCopy_Click);
            // 
            // toolstripPaste
            // 
            this.toolstripPaste.Name = "toolstripPaste";
            this.toolstripPaste.Size = new System.Drawing.Size(200, 22);
            this.toolstripPaste.Text = "Paste      Ctr+V";
            this.toolstripPaste.Click += new System.EventHandler(this.toolstripPaste_Click);
            // 
            // selectAllCtrAToolStripMenuItem
            // 
            this.selectAllCtrAToolStripMenuItem.Name = "selectAllCtrAToolStripMenuItem";
            this.selectAllCtrAToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.selectAllCtrAToolStripMenuItem.Text = "Select All    Ctr+A";
            this.selectAllCtrAToolStripMenuItem.Click += new System.EventHandler(this.selectAllCtrAToolStripMenuItem_Click);
            // 
            // labelCharacterSizesChart
            // 
            this.labelCharacterSizesChart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelCharacterSizesChart.Enabled = false;
            this.labelCharacterSizesChart.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCharacterSizesChart.Location = new System.Drawing.Point(12, 12);
            this.labelCharacterSizesChart.Name = "labelCharacterSizesChart";
            this.labelCharacterSizesChart.ReadOnly = true;
            this.labelCharacterSizesChart.Size = new System.Drawing.Size(728, 34);
            this.labelCharacterSizesChart.TabIndex = 5;
            this.labelCharacterSizesChart.Text = "1       10        20        30        40        50        60        70        80";
            // 
            // richTextBox1
            // 
            this.richTextBox1.ContextMenuStrip = this.contextRecommendSpelling;
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 52);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(728, 474);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "Testing text.";
            // 
            // BasicTextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 584);
            this.ControlBox = false;
            this.Controls.Add(this.labelCharacterSizesChart);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.richTextBox1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicTextEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Text";
            this.contextRecommendSpelling.ResumeLayout(false);
            this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private FlickerFreeRichEditTextBox richTextBox1;
		private System.Windows.Forms.ContextMenuStrip contextRecommendSpelling;
		private System.Windows.Forms.ToolStripMenuItem toolstripRecommendSpelling;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolstripCopy;
		private System.Windows.Forms.ToolStripMenuItem toolstripPaste;
		private System.Windows.Forms.ToolStripMenuItem toolstrinpCut;
		private System.Windows.Forms.ToolStripMenuItem selectAllCtrAToolStripMenuItem;
		private System.Windows.Forms.RichTextBox labelCharacterSizesChart;
		}
	}