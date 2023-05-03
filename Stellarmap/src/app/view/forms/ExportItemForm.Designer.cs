namespace Stellarmap
	{
	partial class ExportItemForm
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
            this.radioObj = new System.Windows.Forms.RadioButton();
            this.radioWeap = new System.Windows.Forms.RadioButton();
            this.radioArmor = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioNpc = new System.Windows.Forms.RadioButton();
            this.radioDoor = new System.Windows.Forms.RadioButton();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioMeal = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioObj
            // 
            this.radioObj.AutoSize = true;
            this.radioObj.Checked = true;
            this.radioObj.Location = new System.Drawing.Point(18, 110);
            this.radioObj.Name = "radioObj";
            this.radioObj.Size = new System.Drawing.Size(41, 17);
            this.radioObj.TabIndex = 3;
            this.radioObj.TabStop = true;
            this.radioObj.Text = "Obj";
            this.radioObj.UseVisualStyleBackColor = true;
            // 
            // radioWeap
            // 
            this.radioWeap.AutoSize = true;
            this.radioWeap.Location = new System.Drawing.Point(18, 133);
            this.radioWeap.Name = "radioWeap";
            this.radioWeap.Size = new System.Drawing.Size(54, 17);
            this.radioWeap.TabIndex = 4;
            this.radioWeap.Text = "Weap";
            this.radioWeap.UseVisualStyleBackColor = true;
            // 
            // radioArmor
            // 
            this.radioArmor.AutoSize = true;
            this.radioArmor.Location = new System.Drawing.Point(18, 19);
            this.radioArmor.Name = "radioArmor";
            this.radioArmor.Size = new System.Drawing.Size(52, 17);
            this.radioArmor.TabIndex = 5;
            this.radioArmor.TabStop = true;
            this.radioArmor.Text = "Armor";
            this.radioArmor.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioMeal);
            this.groupBox1.Controls.Add(this.radioNpc);
            this.groupBox1.Controls.Add(this.radioDoor);
            this.groupBox1.Controls.Add(this.radioObj);
            this.groupBox1.Controls.Add(this.radioArmor);
            this.groupBox1.Controls.Add(this.radioWeap);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(88, 160);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save Path";
            // 
            // radioNpc
            // 
            this.radioNpc.AutoSize = true;
            this.radioNpc.Location = new System.Drawing.Point(18, 87);
            this.radioNpc.Name = "radioNpc";
            this.radioNpc.Size = new System.Drawing.Size(45, 17);
            this.radioNpc.TabIndex = 7;
            this.radioNpc.TabStop = true;
            this.radioNpc.Text = "Npc";
            this.radioNpc.UseVisualStyleBackColor = true;
            // 
            // radioDoor
            // 
            this.radioDoor.AutoSize = true;
            this.radioDoor.Location = new System.Drawing.Point(18, 42);
            this.radioDoor.Name = "radioDoor";
            this.radioDoor.Size = new System.Drawing.Size(48, 17);
            this.radioDoor.TabIndex = 6;
            this.radioDoor.TabStop = true;
            this.radioDoor.Text = "Door";
            this.radioDoor.UseVisualStyleBackColor = true;
            // 
            // textFileName
            // 
            this.textFileName.Location = new System.Drawing.Point(106, 41);
            this.textFileName.Name = "textFileName";
            this.textFileName.Size = new System.Drawing.Size(160, 20);
            this.textFileName.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(106, 90);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(191, 90);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "File Name:";
            // 
            // radioMeal
            // 
            this.radioMeal.AutoSize = true;
            this.radioMeal.Location = new System.Drawing.Point(18, 64);
            this.radioMeal.Name = "radioMeal";
            this.radioMeal.Size = new System.Drawing.Size(48, 17);
            this.radioMeal.TabIndex = 8;
            this.radioMeal.TabStop = true;
            this.radioMeal.Text = "Meal";
            this.radioMeal.UseVisualStyleBackColor = true;
            // 
            // ExportItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 185);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textFileName);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportItemForm";
            this.ShowIcon = false;
            this.Text = "Export Object";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.RadioButton radioObj;
		private System.Windows.Forms.RadioButton radioWeap;
		private System.Windows.Forms.RadioButton radioArmor;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textFileName;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioNpc;
		private System.Windows.Forms.RadioButton radioDoor;
        private System.Windows.Forms.RadioButton radioMeal;
		}
	}