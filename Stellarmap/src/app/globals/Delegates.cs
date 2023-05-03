using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Stellarmap
	{
	public delegate void ViewEvent(ControlRenderDevice sender,Control target);
	public delegate void ModelEvent(object sender,object model);
	public delegate void AdapterUpdateReceiveEvent(string roomFileName,RoomUpdateInfo info);
	public delegate void AdapterUpdateSendEvent(string roomFileName);
	public delegate bool ExportItemEvent(ItemSaveType type,string fileName);
	public delegate void RoomAdd(Point cursor,string name,RoomType type);
	}
