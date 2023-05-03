using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public interface IDomainViewAdapter
		{
		void UpdateView(string roomFileName);
		RoomUpdateInfo QueryView(string roomFileName);
		}
	}
