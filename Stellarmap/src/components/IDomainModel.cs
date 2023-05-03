using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public interface IDomainModel
		{
		bool Initialized
			{
			get;
			}
		
		//input
		bool AddSubscriber(IDomainViewAdapter controller);
		bool RemoveSubscriber(IDomainViewAdapter controller);
		void RefreshDirectory();		
		
		void DisableModel();
		void ResetModel(string path);
		
		RoomModel GetRoom(string roomName);
		bool ImportRoom(string roomName);
		bool AddRoom(string roomName,RoomType type);
		void AddExitToRoom(string room,string exit,string direction);
		void AddEnterToRoom(string room,string enter,string direction);
				
		void RemoveRoom(string roomName);
		void RemoveExitFromRoom(string room,string exit);
		void RemoveEnterFromRoom(string room,string enter);
		
		//inventory
		void AddDoor(string door,ItemSaveType type);
		void AddDoorToRoom(string room,string exit,string door);
		bool AddInventory(string inventory,ItemSaveType type);
		void AddInventoryToRoom(string room,string inventory);
		
		void RemoveDoor(string door);
		void RemoveDoorFromRoom(string room,string door);
		void RemoveDoorFromExit(string room,string exit);
		void RemoveInventory(string inventory);
		
		void AddDomainReference(string domainName);
		
		bool SaveModelToDisk(VirtualDomain vd);
		bool LoadModelFromDisk(VirtualDomain vd);
		
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
		
		List<RoomModel> GetRooms
			{
			get;
			}
		List<string>	RoomNames
			{
			get;
			}	
		List<string>	Doors
			{
			get;
			}
		List<string>	Inventory
			{
			get;
			}
		List<string> ReferencedDomains
			{
			get;
			}
			
		
		void StoreView(string roomName,RoomUpdateInfo info);
		RoomUpdateInfo PropogateModelToView(string roomName);
		}
	}
