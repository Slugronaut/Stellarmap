using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public class DomainControllerException : Exception
		{
		public DomainControllerException() : base() {}
		public DomainControllerException(string message) : base(message)	{}
		public DomainControllerException(string message,Exception inner) : base(message,inner) {}
		}
	
	/// <summary>
	/// Interface through which a model and its views may indirectly
	/// update and query each other.
	/// 
	/// TODO: Needs a system of ensuring that a circular update does not happen
	/// when the model or view triggers an update event!
	/// </summary>
	public class DomainController : IDomainViewAdapter,IDomainModelAdapter
		{
		IDomainModel Model;
		IDomainView View;
		
		
		public DomainController(IDomainModel model,IDomainView view)
			{
			System.Diagnostics.Debug.Assert(model != null && view != null);
			
			//assign refs
			Model = model;
			View = view;
			
			//subscribe to refs (publishers)
			if(!Model.AddSubscriber(this))
				{
				throw new DomainControllerException("Could not add view to model.");
				}
			if(!View.SetSubscriber(this))
				{
				throw new DomainControllerException("Could not attach model to view.");
				}
			}
		
		
		#region for model
		public void UpdateView(string roomName)
			{
			View.DisplayModel(roomName,Model.PropogateModelToView(roomName));
			}
		
		public RoomUpdateInfo QueryView(string roomFileName)
			{
			return View.PropogateViewToModel(roomFileName);
			}
		
		#endregion
		
		
		#region for view
		public List<RoomModel> Rooms
			{
			get {return Model.GetRooms;}
			}
		
		public List<string> Inventory
			{
			get{return Model.Inventory;}
			}
		
		public List<string> Doors
			{
			get{return Model.Doors;}
			}	
		
		public RoomModel GetRoom(string roomName)
			{
			return Model.GetRoom(roomName);
			}	
		
		public bool SaveModelToDisk(VirtualDomain vd)
			{
			return Model.SaveModelToDisk(vd);
			}

		public bool LoadModelFromDisk(VirtualDomain vd)
			{
			return Model.LoadModelFromDisk(vd);
			}
		
		public void DisableModel()
			{
			Model.DisableModel();
			}
		
		public void TriggerViewUpdate(string roomName)
			{
			View.DisplayModel(roomName,Model.PropogateModelToView(roomName));
			}
		
		public void TriggerCurrentViewControlsUpdate()
			{
			View.TriggerCurrentViewControlsUpdate();
			}
		
		public void UpdateModel(string roomFileName)
			{
			Model.StoreView(roomFileName,View.PropogateViewToModel(roomFileName));
			}
		
		public RoomUpdateInfo QueryModel(string roomFileName)
			{
			return Model.PropogateModelToView(roomFileName);
			}
		
		public void ResetModel(string path)
			{
			Model.ResetModel(path);
			}
		
		public bool Initialized
			{
			get {return Model.Initialized;}
			}
		
		public bool AddRoom(string roomName,RoomType type)
			{
			return Model.AddRoom(roomName,type);
			}
		
		public bool ImportRoom(string roomName)
			{
			return Model.ImportRoom(roomName);
			}
		
		public void AddExitToRoom(string room,string exit,string direction)
			{
			Model.AddExitToRoom(room,exit,direction);
			}
		
		public void AddEnterToRoom(string room,string enter,string direction)
			{
			Model.AddEnterToRoom(room,enter,direction);
			}
		
		public void RemoveRoom(string roomName)
			{
			Model.RemoveRoom(roomName);
			}	
		
		public void RemoveExitFromRoom(string room,string exit)
			{
			Model.RemoveExitFromRoom(room,exit);
			}
		
		public void RemoveEnterFromRoom(string room,string enter)
			{
			Model.RemoveEnterFromRoom(room,enter);
			}
		
		
		
		
		public void AddDoor(string door,ItemSaveType type)
			{
			Model.AddDoor(door,type);
			}

		public void AddDoorToRoom(string room,string exit,string door)
			{
			Model.AddDoorToRoom(room,exit,door);
			}	
		
		public bool AddInventory(string inventory,ItemSaveType type)
			{
			return Model.AddInventory(inventory,type);
			}	
		
		public void AddInventoryToRoom(string room,string inventory)
			{
			Model.AddInventoryToRoom(room,inventory);
			}	
		
		public void RemoveDoor(string door)
			{
			Model.RemoveDoor(door);
			}
		
		public void RemoveDoorFromRoom(string room,string door)
			{
			Model.RemoveDoorFromRoom(room,door);
			}
		
		public void RemoveDoorFromExit(string room,string exit)
			{
			Model.RemoveDoorFromExit(room,exit);
			}
		
		public void RemoveInventory(string inventory)
			{
			Model.RemoveInventory(inventory);
			}
		
		public void AddDomainReference(string domainName)
			{
			Model.AddDomainReference(domainName);
			}
		
		
		public List<string> ReferencedDomains
			{
			get {return Model.ReferencedDomains;}
			}
				
		public List<string> RoomNames
			{
			get {return Model.RoomNames;}
			}
		
		public string DomainName
			{
			get {return Model.DomainName;}
			}
		
		public string DomainRootDir
			{
			get{return Model.DomainRootDir;}
			}
		
		public IncludeFile IncludeFile
			{
			get {return Model.IncludeFile;}
			}	
		#endregion	
		}
	}
