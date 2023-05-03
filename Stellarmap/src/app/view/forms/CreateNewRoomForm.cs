using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	public partial class CreateNewRoomForm : Form
		{
		private RoomAdd AddRoom;
		private Point Mouse;
		
		public CreateNewRoomForm(RoomAdd roomAddCallback,Point mouseLocation)
			{
			InitializeComponent();
			this.CenterToParent();
			AddRoom += roomAddCallback;
			Mouse = mouseLocation;
			}

		private void buttonOk_Click(object sender,EventArgs e)
			{
			if(AddRoom != null)
				{
                RoomType type = RoomType.Normal;
                if(this.radioRoom.Checked) {type = RoomType.Normal;}
                else if(this.radioShop.Checked) {type = RoomType.Shop;}
                else if(this.radioInstance.Checked) {type = RoomType.Instance;}
                else if(this.radioPolice.Checked) {type = RoomType.PoliceStation;}
                else if(this.radioJailCell.Checked) {type = RoomType.JailCell;}
				AddRoom(Mouse,this.textRoomName.Text,type);
				}
			this.Close();
			}

		private void buttonCancel_Click(object sender,EventArgs e)
			{
			this.Close();
			}
		}
	}