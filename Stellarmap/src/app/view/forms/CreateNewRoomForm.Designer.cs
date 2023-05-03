namespace Stellarmap
	{
	partial class CreateNewRoomForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textRoomName = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioRoom = new System.Windows.Forms.RadioButton();
            this.radioShop = new System.Windows.Forms.RadioButton();
            this.radioInstance = new System.Windows.Forms.RadioButton();
            this.radioPolice = new System.Windows.Forms.RadioButton();
            this.radioJailCell = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Room Name:";
            // 
            // textRoomName
            // 
            this.textRoomName.Location = new System.Drawing.Point(87, 20);
            this.textRoomName.Name = "textRoomName";
            this.textRoomName.Size = new System.Drawing.Size(148, 20);
            this.textRoomName.TabIndex = 1;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(108, 157);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(189, 157);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radioRoom
            // 
            this.radioRoom.AutoSize = true;
            this.radioRoom.Checked = true;
            this.radioRoom.Location = new System.Drawing.Point(15, 64);
            this.radioRoom.Name = "radioRoom";
            this.radioRoom.Size = new System.Drawing.Size(89, 17);
            this.radioRoom.TabIndex = 4;
            this.radioRoom.TabStop = true;
            this.radioRoom.Text = "Normal Room";
            this.radioRoom.UseVisualStyleBackColor = true;
            // 
            // radioShop
            // 
            this.radioShop.AutoSize = true;
            this.radioShop.Location = new System.Drawing.Point(15, 87);
            this.radioShop.Name = "radioShop";
            this.radioShop.Size = new System.Drawing.Size(50, 17);
            this.radioShop.TabIndex = 5;
            this.radioShop.Text = "Shop";
            this.radioShop.UseVisualStyleBackColor = true;
            // 
            // radioInstance
            // 
            this.radioInstance.AutoSize = true;
            this.radioInstance.Location = new System.Drawing.Point(138, 64);
            this.radioInstance.Name = "radioInstance";
            this.radioInstance.Size = new System.Drawing.Size(97, 17);
            this.radioInstance.TabIndex = 6;
            this.radioInstance.Text = "Instance Room";
            this.radioInstance.UseVisualStyleBackColor = true;
            // 
            // radioPolice
            // 
            this.radioPolice.AutoSize = true;
            this.radioPolice.Location = new System.Drawing.Point(138, 87);
            this.radioPolice.Name = "radioPolice";
            this.radioPolice.Size = new System.Drawing.Size(90, 17);
            this.radioPolice.TabIndex = 7;
            this.radioPolice.Text = "Police Station";
            this.radioPolice.UseVisualStyleBackColor = true;
            // 
            // radioJailCell
            // 
            this.radioJailCell.AutoSize = true;
            this.radioJailCell.Location = new System.Drawing.Point(138, 110);
            this.radioJailCell.Name = "radioJailCell";
            this.radioJailCell.Size = new System.Drawing.Size(60, 17);
            this.radioJailCell.TabIndex = 8;
            this.radioJailCell.TabStop = true;
            this.radioJailCell.Text = "Jail Cell";
            this.radioJailCell.UseVisualStyleBackColor = true;
            // 
            // CreateNewRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 192);
            this.Controls.Add(this.radioJailCell);
            this.Controls.Add(this.radioPolice);
            this.Controls.Add(this.radioInstance);
            this.Controls.Add(this.radioShop);
            this.Controls.Add(this.radioRoom);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textRoomName);
            this.Controls.Add(this.label1);
            this.Name = "CreateNewRoomForm";
            this.Text = "CreateNewRoomForm";
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textRoomName;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioRoom;
        private System.Windows.Forms.RadioButton radioShop;
        private System.Windows.Forms.RadioButton radioInstance;
        private System.Windows.Forms.RadioButton radioPolice;
        private System.Windows.Forms.RadioButton radioJailCell;
		}
	}