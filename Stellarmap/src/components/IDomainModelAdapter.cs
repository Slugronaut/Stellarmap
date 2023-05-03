using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public interface IDomainModelAdapter
		{
		void TriggerViewUpdate(string roomName);
		void TriggerCurrentViewControlsUpdate();
		void UpdateModel(string roomName);
		RoomUpdateInfo QueryModel(string roomName);
		
		void DisableModel();
		void ResetModel(string path);
		
		RoomModel GetRoom(string roomName);
		bool AddRoom(string roomName,RoomType type);
		bool ImportRoom(string roomName);
		void AddExitToRoom(string room,string exit,string direction);
		void AddEnterToRoom(string room,string enter,string direction);
		
		void RemoveRoom(string roomName);
		void RemoveExitFromRoom(string room,string exit);
		void RemoveEnterFromRoom(string room,string enter);
		
		
		//inventory
		void AddDoor(string door,ItemSaveType type);
		void AddDoorToRoom(string room,string exit,string door);
		bool AddInventory(string name,ItemSaveType type);
		void AddInventoryToRoom(string room,string inventory);
		
		void RemoveDoor(string door);
		void RemoveDoorFromRoom(string room,string door);
		void RemoveDoorFromExit(string room,string exit);
		void RemoveInventory(string name);
		
		void AddDomainReference(string domainName);
		
		bool SaveModelToDisk(VirtualDomain vd);
		bool LoadModelFromDisk(VirtualDomain vd);
		
		List<RoomModel> Rooms
			{
			get;
			}
		
		List<string> RoomNames
			{
			get;
			}
		
		List<string> Doors
			{
			get;
			}
		
		List<string> Inventory
			{
			get;
			}
		
		List<string> ReferencedDomains
			{
			get;
			}
				
		IncludeFile IncludeFile
			{
			get;
			}
		string DomainName
			{
			get;
			}
		string DomainRootDir
			{
			get;
			}
		
		bool Initialized
			{
			get;
			}	
		}
	}
