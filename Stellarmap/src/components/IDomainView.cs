using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public interface IDomainView
		{
		//input
		bool SetSubscriber(IDomainModelAdapter controller);
		bool RemoveSubscriber(IDomainModelAdapter controller);
		void DisplayModel(string roomFileName,RoomUpdateInfo info);
		RoomUpdateInfo PropogateViewToModel(string roomName);
		string GetCurrentModelViewed();
		void TriggerCurrentViewControlsUpdate();
		}
	}
