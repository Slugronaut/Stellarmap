namespace Stellarmap
	{
	partial class CreateDoor
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
			this.flowSetId = new System.Windows.Forms.FlowLayoutPanel();
			this.flowSetShort = new System.Windows.Forms.FlowLayoutPanel();
			this.flowSetLong = new System.Windows.Forms.FlowLayoutPanel();
			this.flowSetLockable = new System.Windows.Forms.FlowLayoutPanel();
			this.flowSetKeys = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonAddSide = new System.Windows.Forms.Button();
			this.buttonRemoveLastSide = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textNewSideName = new System.Windows.Forms.TextBox();
			this.comboSideList = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkClosed = new Stellarmap.Check();
			this.checkLocked = new Stellarmap.Check();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowSetId
			// 
			this.flowSetId.AutoScroll = true;
			this.flowSetId.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.flowSetId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowSetId.Location = new System.Drawing.Point(6,359);
			this.flowSetId.Name = "flowSetId";
			this.flowSetId.Size = new System.Drawing.Size(283,301);
			this.flowSetId.TabIndex = 7;
			// 
			// flowSetShort
			// 
			this.flowSetShort.AutoScroll = true;
			this.flowSetShort.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.flowSetShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowSetShort.Location = new System.Drawing.Point(295,19);
			this.flowSetShort.Name = "flowSetShort";
			this.flowSetShort.Size = new System.Drawing.Size(283,301);
			this.flowSetShort.TabIndex = 8;
			// 
			// flowSetLong
			// 
			this.flowSetLong.AutoScroll = true;
			this.flowSetLong.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.flowSetLong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowSetLong.Location = new System.Drawing.Point(584,19);
			this.flowSetLong.Name = "flowSetLong";
			this.flowSetLong.Size = new System.Drawing.Size(283,301);
			this.flowSetLong.TabIndex = 10;
			// 
			// flowSetLockable
			// 
			this.flowSetLockable.AutoScroll = true;
			this.flowSetLockable.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.flowSetLockable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowSetLockable.Location = new System.Drawing.Point(584,359);
			this.flowSetLockable.Name = "flowSetLockable";
			this.flowSetLockable.Size = new System.Drawing.Size(283,301);
			this.flowSetLockable.TabIndex = 11;
			// 
			// flowSetKeys
			// 
			this.flowSetKeys.AutoScroll = true;
			this.flowSetKeys.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.flowSetKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowSetKeys.Location = new System.Drawing.Point(295,359);
			this.flowSetKeys.Name = "flowSetKeys";
			this.flowSetKeys.Size = new System.Drawing.Size(283,301);
			this.flowSetKeys.TabIndex = 12;
			// 
			// buttonAddSide
			// 
			this.buttonAddSide.Location = new System.Drawing.Point(18,72);
			this.buttonAddSide.Name = "buttonAddSide";
			this.buttonAddSide.Size = new System.Drawing.Size(117,23);
			this.buttonAddSide.TabIndex = 13;
			this.buttonAddSide.Text = "Add Direction";
			this.buttonAddSide.UseVisualStyleBackColor = true;
			this.buttonAddSide.Click += new System.EventHandler(this.buttonAddSide_Click);
			// 
			// buttonRemoveLastSide
			// 
			this.buttonRemoveLastSide.Location = new System.Drawing.Point(18,156);
			this.buttonRemoveLastSide.Name = "buttonRemoveLastSide";
			this.buttonRemoveLastSide.Size = new System.Drawing.Size(117,23);
			this.buttonRemoveLastSide.TabIndex = 14;
			this.buttonRemoveLastSide.Text = "Remove Direction";
			this.buttonRemoveLastSide.UseVisualStyleBackColor = true;
			this.buttonRemoveLastSide.Click += new System.EventHandler(this.buttonRemoveLastSide_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15,26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83,13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Direction Name:";
			// 
			// textNewSideName
			// 
			this.textNewSideName.Location = new System.Drawing.Point(18,46);
			this.textNewSideName.Name = "textNewSideName";
			this.textNewSideName.Size = new System.Drawing.Size(117,20);
			this.textNewSideName.TabIndex = 16;
			// 
			// comboSideList
			// 
			this.comboSideList.FormattingEnabled = true;
			this.comboSideList.Location = new System.Drawing.Point(18,129);
			this.comboSideList.Name = "comboSideList";
			this.comboSideList.Size = new System.Drawing.Size(117,21);
			this.comboSideList.TabIndex = 17;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.flowSetId);
			this.groupBox1.Controls.Add(this.flowSetShort);
			this.groupBox1.Controls.Add(this.flowSetLong);
			this.groupBox1.Controls.Add(this.flowSetKeys);
			this.groupBox1.Controls.Add(this.flowSetLockable);
			this.groupBox1.Location = new System.Drawing.Point(12,52);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(876,673);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Side Based Function Calls";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Location = new System.Drawing.Point(2,19);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(287,301);
			this.panel1.TabIndex = 20;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textNewSideName);
			this.groupBox3.Controls.Add(this.buttonAddSide);
			this.groupBox3.Controls.Add(this.comboSideList);
			this.groupBox3.Controls.Add(this.buttonRemoveLastSide);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Location = new System.Drawing.Point(3,3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(284,191);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Door Direction";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkClosed);
			this.groupBox2.Controls.Add(this.checkLocked);
			this.groupBox2.Location = new System.Drawing.Point(134,200);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(153,83);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Non-side Based Settings";
			// 
			// checkClosed
			// 
			this.checkClosed.BackColor = System.Drawing.Color.Transparent;
			this.checkClosed.Checked = true;
			this.checkClosed.FunctionName = "SetClosed";
			this.checkClosed.KeyType = Stellarmap.EntryType.Mixed;
			this.checkClosed.LabelText = "Closed";
			this.checkClosed.Location = new System.Drawing.Point(8,31);
			this.checkClosed.Name = "checkClosed";
			this.checkClosed.ParameterType = Stellarmap.FuncParamType.Property;
			this.checkClosed.Size = new System.Drawing.Size(66,22);
			this.checkClosed.TabIndex = 13;
			this.checkClosed.ValueType = Stellarmap.EntryType.Mixed;
			// 
			// checkLocked
			// 
			this.checkLocked.BackColor = System.Drawing.Color.Transparent;
			this.checkLocked.Checked = false;
			this.checkLocked.FunctionName = "SetLocked";
			this.checkLocked.KeyType = Stellarmap.EntryType.Mixed;
			this.checkLocked.LabelText = "Locked";
			this.checkLocked.Location = new System.Drawing.Point(80,31);
			this.checkLocked.Name = "checkLocked";
			this.checkLocked.ParameterType = Stellarmap.FuncParamType.Property;
			this.checkLocked.Size = new System.Drawing.Size(70,22);
			this.checkLocked.TabIndex = 14;
			this.checkLocked.ValueType = Stellarmap.EntryType.Mixed;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0,0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(902,24);
			this.menuStrip1.TabIndex = 19;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37,20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// importToolStripMenuItem
			// 
			this.importToolStripMenuItem.Enabled = false;
			this.importToolStripMenuItem.Name = "importToolStripMenuItem";
			this.importToolStripMenuItem.Size = new System.Drawing.Size(110,22);
			this.importToolStripMenuItem.Text = "&Import";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(107,6);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(110,22);
			this.exportToolStripMenuItem.Text = "&Export";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// CreateDoor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(902,737);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "CreateDoor";
			this.Text = "Create Door";
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowSetId;
		private System.Windows.Forms.FlowLayoutPanel flowSetShort;
		private System.Windows.Forms.FlowLayoutPanel flowSetLong;
		private System.Windows.Forms.FlowLayoutPanel flowSetLockable;
		private System.Windows.Forms.FlowLayoutPanel flowSetKeys;
		private System.Windows.Forms.Button buttonAddSide;
		private System.Windows.Forms.Button buttonRemoveLastSide;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textNewSideName;
		private System.Windows.Forms.ComboBox comboSideList;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private Check checkClosed;
		private Check checkLocked;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;

		}
	}