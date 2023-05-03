using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Stellarmap
	{
	public class DomainModelException : Exception
		{
		public DomainModelException() : base() {}
		public DomainModelException(string message) : base(message) {}
		public DomainModelException(string message,Exception inner) : base(message,inner) {}
		}
	
	
	public class DomainReference
		{
		public string Name;
		public Dictionary<string,FileReferenceModel> Rooms = new Dictionary<string,FileReferenceModel>();
		public Dictionary<string,FileReferenceModel> Inventory = new Dictionary<string,FileReferenceModel>();
		public Dictionary<string,FileReferenceModel> Doors = new Dictionary<string,FileReferenceModel>();
		
		public DomainReference(string name)
			{
			Name = IncludeFile.ConvertToValidDirectoryName(name);
			//TODO: add file loading code
			}

		#region static methods
		public string ConvertRoomNameToFullPath(string roomName)
			{
			return Name.ToUpper() + "_ROOM \"/" + roomName + "\"";
			}

		public string ConvertFullPathToRoomName(string fileName)
			{
			fileName = fileName.Replace(Name.ToUpper() + "_ROOM \"/","");
			fileName = fileName.Replace("\"","");
			return fileName;
			}
		#endregion
		}
	
	public class DomainModel : IDomainModel
		{
		#region members
		private const int THIS_DOMAIN = 0;
		private const string ParserErrorTitle = "Parser Error In Model";
		
		private string Name;
		private string RootDirectory;
		private bool DomainInitialized = false;
		
		//model data
		private LinkedList<RoomModel> Rooms;
		private Dictionary<string,RoomModel> RoomNameMap;
				
		//inter-file reference data
		private IncludeFile Include;
		private	List<DomainReference> DomainFileReferences;
		
		private IDomainViewAdapter refMVCAdapter;
		private event AdapterUpdateSendEvent UpdateViewEvent;
		#endregion
		
		
		public DomainModel()	{}
		
		public bool Initialized
			{
			get {return DomainInitialized;}
			}
		
		private void NewDomain(string rootDir)
			{								
			//obtain the name of the domain from the final directory
			List<string> sub = new List<string>(rootDir.Split('\\'));			
			Name = sub[sub.Count-1];
			RootDirectory = rootDir;
			
			//there is always at least one DomainReference, this domain!
			DomainReference localRef = new DomainReference("domain");
			Include = new IncludeFile(Name);
			localRef.Doors = new Dictionary<string,FileReferenceModel>();
			localRef.Inventory = new Dictionary<string,FileReferenceModel>();

			DomainFileReferences = new List<DomainReference>();
			DomainFileReferences.Add(localRef);
			
			//Directories = new DirectoryStructure(RootDirectory);
			Rooms = new LinkedList<RoomModel>();
			RoomNameMap = new Dictionary<string,RoomModel>();
			
			//load all sub directories and files		
			DomainInitialized = true;
			
			}				
		
		
		
		#region properties
		public List<RoomModel> GetRooms
			{
			get {
				return new List<RoomModel>(this.RoomNameMap.Values);
				}
			}
		
		public List<string> RoomNames
			{
			get {
				List<string> file = new List<string>();
				if(!DomainInitialized) { return file; }
				
				foreach(DomainReference domRef in this.DomainFileReferences)
					{
					foreach(FileReferenceModel refMod in domRef.Rooms.Values)
						{
						file.Add(refMod.FullDSPath);
						}
					}
				return file;
				}
			}
		
		public List<string> Inventory
			{
			get{
				List<string> file = new List<string>();
				if(!DomainInitialized) { return file; }
				
				foreach(DomainReference domRef in this.DomainFileReferences)
					{
					foreach(FileReferenceModel refMod in domRef.Inventory.Values)
						{
						file.Add(refMod.FullDSPath);
						}
					}
				//foreach(FileReferenceModel refMod in DomainFileReferences[THIS_DOMAIN].Inventory.Values)
					//{
					//file.Add(refMod.FullDSPath);
					//}
				
				return file;
				//return this.InventoryFiles.Keys;
				}
			}
		
		public List<string> Doors
			{
			get{
				List<string> file = new List<string>();
				if(!DomainInitialized) { return file; }
				
				foreach(FileReferenceModel refMod in DomainFileReferences[THIS_DOMAIN].Doors.Values)
					{
					file.Add(refMod.FullDSPath);
					}

				return file;
				//return new List<string>(this.DoorFiles.Keys);
				}
			}	
		
		public IncludeFile IncludeFile
			{
			get {return Include;}
			}
		
		public string DomainRootDir
			{
			get {return RootDirectory;}
			}
		
		public string RoomSubDir
			{
			get {
				//hack alert: kinda hackish with direct references here
				IncludeFile include = Include;
				//return include.Defines[include.Room];
				return include.GetPathDefinitionValue(ItemSaveType.Room);
				}
			}

		public string DomainName
			{
			get { return this.Name; }
			}	
		#endregion
		
		
		#region public interface
		public RoomModel GetRoom(string room)
			{
			if(!DomainInitialized)	{return null;}
			if(RoomNameMap.ContainsKey(room))
				{
				return RoomNameMap[room];
				}
			return null;
			}
		
		public void DisableModel()
			{
			this.DomainInitialized = false;
			}
		
		public void ResetModel(string path)
			{
			NewDomain(path);
			}
		
		public bool AddSubscriber(IDomainViewAdapter controller)
			{
			//TODO
			System.Diagnostics.Debug.Assert(controller != null);
			
			if(refMVCAdapter == null)
				{
				refMVCAdapter = controller;
				this.UpdateViewEvent += new AdapterUpdateSendEvent(refMVCAdapter.UpdateView);
				return true;
				}
			
			return false;
			}
		
		public bool RemoveSubscriber(IDomainViewAdapter controller)
			{
			System.Diagnostics.Debug.Assert(controller != null);
			if(refMVCAdapter == controller)
				{
				this.UpdateViewEvent -= new AdapterUpdateSendEvent(refMVCAdapter.UpdateView);
				refMVCAdapter = null;
				return true;
				}
			
			return false;
			}
		
		public void RefreshDirectory()
			{
			//TODO: obtain all object files within directories
			//TODO: add all room objects by calling 'AddRoom' and passing the filename
			}

		public void StoreView(string roomName,RoomUpdateInfo info)
			{
			//System.Diagnostics.Debug.Assert(roomName != null && roomName != "" && info != null);
			if(roomName.Length < 1 || roomName == null) return;

			if(!RoomNameMap.ContainsKey(roomName))
				{
				throw new DomainModelException("Room could not be found: " + roomName);
				}

			RoomNameMap[roomName].FunctionCalls = info.Functions;
			RoomNameMap[roomName].EditorState = info.EditorStates;
			}

		public RoomUpdateInfo PropogateModelToView(string roomName)
			{
			//System.Diagnostics.Debug.Assert(roomName != null && roomName.Length != 0; roomName != "");
			if(roomName == null || roomName.Length < 1) return null;

			if(!RoomNameMap.ContainsKey(roomName))
				{
				throw new DomainModelException("Room could not be found: " + roomName);
				}

			RoomUpdateInfo info = new RoomUpdateInfo();
			info.Functions = RoomNameMap[roomName].FunctionCalls;
			info.EditorStates = RoomNameMap[roomName].EditorState;
			return info;
			}

		public bool ImportRoom(string roomName)
			{
			return LoadRoom(roomName);
			}
		
		public bool AddRoom(string roomName,RoomType type)
			{
			if(roomName.Length == 0 || RoomNameMap.ContainsKey(roomName))
				{
				return false;
				}
			
			Rooms.AddLast(new RoomModel(null,roomName,this,type));
			RoomNameMap.Add(roomName,Rooms.Last.Value);
			
			//we have to get the raw roomname from linked list
			//this.DomainFileReferences[THIS_DOMAIN].Rooms.Add("~" + Include.GetPathDefinitionIdentifier(ItemSaveType.Room) + " \"/" + Rooms.Last.Value.RawFileName + "\"",new FileReferenceModel(roomName,Include));
			this.DomainFileReferences[THIS_DOMAIN].Rooms.Add(roomName,new FileReferenceModel(Globals.Generator.FunctionParameterLiteral + roomName,Include));
			
			return true;
			}

		public void AddExitToRoom(string targetRoom,string exitToPath,string direction)
			{
			if(targetRoom == "" || exitToPath == "" || direction == "") return;
			if(targetRoom  == exitToPath)
				{
				System.Windows.Forms.MessageBox.Show("You cannot have " + targetRoom + " exit to itself.");
				return;
				}
			string targetFunction = "SetExits";
			RoomModel room = RoomNameMap[targetRoom];

			//first order of business is to make sure we even have the
			//function call for an exit, otherwise we need to create it
			if(room.FunctionCalls.CallList.ContainsKey(targetFunction))
				{
				//function call exists. Do we already have an entry for this direction?
				try
					{
					Dictionary<string,string> map = ParserTools.StringIntoMap(room.FunctionCalls.CallList[targetFunction],targetFunction);
					if(map.ContainsKey(direction))
						{
						//we have to edit a pre-existing entry
						foreach(string key in map.Keys)
							{
							if(key == direction)
								{
								map[key] = exitToPath;
								room.EditorState.ExitsList = map;
								room.FunctionCalls.CallList[targetFunction] = ParserTools.MapIntoString(map);
								return;
								}
							}
						}
					else
						{
						//entry does not exist, create it now
						map.Add(direction,exitToPath);
						room.EditorState.ExitsList = map;
						room.FunctionCalls.CallList[targetFunction] = ParserTools.MapIntoString(map);
						return;
						}
					}
				catch(ParserException e)
					{
					System.Windows.Forms.MessageBox.Show(e.Message,ParserErrorTitle);
					}
				}
			else
				{
				//we are storing this function call for the first time
				//TODO: Implement this
				//throw new Exception("Function does not exist in this control. TODO: Implement Model.AddExitToRoom()");
				Dictionary<string,string> map = new Dictionary<string,string>();
				map.Add(direction,exitToPath);
				room.EditorState.ExitsList = map;
				room.FunctionCalls.CallList.Add(targetFunction,ParserTools.MapIntoString(map));
				}
			}

		public void AddEnterToRoom(string targetRoom,string exitToPath,string direction)
			{
			if(targetRoom == "" || exitToPath == "" || direction == "") return;
			if(targetRoom == exitToPath)
				{
				System.Windows.Forms.MessageBox.Show("You cannot have " + targetRoom + " enter into itself.");
				return;
				}
			string targetFunction = "SetEnters";
			RoomModel room = RoomNameMap[targetRoom];

			//first order of business is to make sure we even have the
			//function call for an exit, otherwise we need to create it
			if(room.FunctionCalls.CallList.ContainsKey(targetFunction))
				{
				//function call exists. Do we already have an entry for this direction?
				try
					{
					Dictionary<string,string> map = ParserTools.StringIntoMap(room.FunctionCalls.CallList[targetFunction],targetFunction);
					if(map.ContainsKey(direction))
						{
						//we have to edit a pre-existing entry
						foreach(string key in map.Keys)
							{
							if(key == direction)
								{
								map[key] = exitToPath;
								room.EditorState.EntersList = map;
								room.FunctionCalls.CallList[targetFunction] = ParserTools.MapIntoString(map);
								return;
								}
							}
						}
					else
						{
						//entry does not exist, create it now
						map.Add(direction,exitToPath);
						room.EditorState.EntersList = map;
						room.FunctionCalls.CallList[targetFunction] = ParserTools.MapIntoString(map);
						return;
						}
					}
				catch(ParserException e)
					{
					System.Windows.Forms.MessageBox.Show(e.Message,ParserErrorTitle);
					}
				}
			else
				{
				//we are storing this function call for the first time
				//TODO: Implement this
				//throw new Exception("Function doesn not exist in this control. TODO: Implement Model.AddExitToRoom()");
				Dictionary<string,string> map = new Dictionary<string,string>();
				map.Add(direction,exitToPath);
				room.EditorState.EntersList = map;
				room.FunctionCalls.CallList.Add(targetFunction,ParserTools.MapIntoString(map));
				
				}
			}	
		
		public void RemoveRoom(string roomName)
			{
			System.Diagnostics.Debug.Assert(RoomNameMap.ContainsKey(roomName));
			
			Rooms.Remove(RoomNameMap[roomName]);
			RoomNameMap.Remove(roomName);

			//we have to get the raw roomname from linked list
			//this.DomainFileReferences[THIS_DOMAIN].Rooms.Add("~" + Include.GetPathDefinitionIdentifier(ItemSaveType.Room) + " \"/" + Rooms.Last.Value.RawFileName + "\"",new FileReferenceModel(roomName,Include));
			//this.DomainFileReferences[THIS_DOMAIN].Rooms.Add(roomName,new FileReferenceModel(Globals.Generator.FunctionParameterLiteral + roomName,Include));
			this.DomainFileReferences[THIS_DOMAIN].Rooms.Remove(roomName);
			
			//we need to remove every exit/entrance that
			//references the room that was just deleted
			foreach(RoomModel room in Rooms)
				{
				RemoveExitFromRoom(room.FileName,roomName);
				RemoveEnterFromRoom(room.FileName,roomName);
				}
			}

		public void RemoveExitFromRoom(string targetRoom,string exitToPath)
			{
			if(targetRoom == "" || exitToPath == "") return;
			string targetFunction = "SetExits";
			RoomModel room = RoomNameMap[targetRoom];

			//first order of business is to see if we even have any exits defined
			if(room.FunctionCalls.CallList.ContainsKey(targetFunction))
				{
				//function is defined. now, is the exit present?
				string mapping = room.FunctionCalls.CallList[targetFunction];
				if(mapping.Contains(exitToPath))
					{
					//ok, we know the exit is here. Now we just need to find and remove it.
					//Note, there can be more than one.
					try
						{
						Dictionary<string,string> map = ParserTools.StringIntoMap(mapping,targetFunction);
						Dictionary<string,string> rebuild = new Dictionary<string,string>();

						foreach(string key in map.Keys)
							{
							//if(map[key] != exitToPath)
							//    {
							//    //only add entries that aren't to be removed
							//    rebuild.Add(key,map[key]);
							//    }
							//J.C. -2/16/2010 - changed to remove entry based on exit direction rather than
							//                  exit destination. That way we don't remove multiple entries
							//                  when the user indented only one.
							if(key != "\"" + exitToPath + "\"")
								{
								rebuild.Add(key,map[key]);
								}
							}

						room.EditorState.ExitsList = rebuild;
						room.FunctionCalls.CallList[targetFunction] = ParserTools.MapIntoString(rebuild);
						}
					catch(ParserException e)
						{
						System.Windows.Forms.MessageBox.Show(e.Message,ParserErrorTitle);
						return;
						}
					}
				}
			}

		public void RemoveEnterFromRoom(string targetRoom,string exitToPath)
			{
			if(targetRoom == "" || exitToPath == "") return;
			string targetFunction = "SetEnters";
			RoomModel room = RoomNameMap[targetRoom];

			//first order of business is to see if we even have any exits defined
			if(room.FunctionCalls.CallList.ContainsKey(targetFunction))
				{
				//function is defined. now, is the exit present?
				string mapping = room.FunctionCalls.CallList[targetFunction];
				if(mapping.Contains(exitToPath))
					{
					//ok, we know the exit is here. Now we just need to find and remove it
					//Note, there can be more than one.
					try
						{
						Dictionary<string,string> map = ParserTools.StringIntoMap(mapping,targetFunction);
						Dictionary<string,string> rebuild = new Dictionary<string,string>();

						foreach(string key in map.Keys)
							{
							if(map[key] != exitToPath)
								{
								//only add entries that aren't to be removed
								rebuild.Add(key,map[key]);
								}
							}

						room.EditorState.EntersList = rebuild;
						room.FunctionCalls.CallList[targetFunction] = ParserTools.MapIntoString(rebuild);
						}
					catch(ParserException e)
						{
						System.Windows.Forms.MessageBox.Show(e.Message,ParserErrorTitle);
						return;
						}
					}
				}
			}	
		
		
		
		//inventory
		public void AddDoor(string door,ItemSaveType type)
			{
			if(door == null || door.Length < 1)	return;

			if(!DomainFileReferences[THIS_DOMAIN].Doors.ContainsKey(door))
				{
				FileReferenceModel mod = new FileReferenceModel(door,type,Include,this.Name);
				DomainFileReferences[THIS_DOMAIN].Doors.Add(door,mod);
				}
			}

		public void AddDoorToRoom(string targetRoom,string exitName,string doorFile)
			{
			throw new Exception("DomainModel->AddDoorToRoom() not implemented");
			}		
		
		public bool AddInventory(string inventory,ItemSaveType type)
			{
			if(inventory == null || inventory.Length < 1) return false;
			
			//hack alert - gotta infer the item type from the directory.
			//This was added to make the 'import item' functionality work
			if(type == ItemSaveType.Unknown)
				{
				string[] path = inventory.Split('\\');
				type = Include.DetermineSaveType(path[path.Length-2]);
				if(type == ItemSaveType.Unknown)	return false;
				
				string inventoryName = path[path.Length-1].Split('.')[0];
				if(!DomainFileReferences[THIS_DOMAIN].Inventory.ContainsKey(inventoryName))
					{
					FileReferenceModel mod = new FileReferenceModel(inventoryName,type,Include,this.Name);
					DomainFileReferences[THIS_DOMAIN].Inventory.Add(inventoryName,mod);
					}
				}
			else{
				if(!DomainFileReferences[THIS_DOMAIN].Inventory.ContainsKey(inventory))
					{
					FileReferenceModel mod = new FileReferenceModel(inventory,type,Include,this.Name);
					DomainFileReferences[THIS_DOMAIN].Inventory.Add(inventory,mod);
					}
				}
			
			return true;
			}
		
		public void AddInventoryToRoom(string room,string inventory)
			{
			throw new Exception("DomainModel->AddInventoryToRoom() not implemented");
			}	
		
		
		
		public void RemoveDoor(string door)
			{
			throw new Exception("DomainModel->RemoveDoor() not implemented");
			if(door == null || door.Length < 1) return;

			if(!DomainFileReferences[THIS_DOMAIN].Doors.ContainsKey(door))
				{
				DomainFileReferences[THIS_DOMAIN].Doors.Remove(door);
				
				//remove instance from all rooms
				
				}
			}
		
		public void RemoveDoorFromRoom(string targetRoom,string doorName)
			{
			throw new Exception("DomainModel->RemoveDoorFromRoom() not implemented");
			}
		
		public void RemoveDoorFromExit(string targetRoom,string exitName)
			{
			throw new Exception("DomainModel->RemoveDoorFromExit() not implemented");
			}
		
		public void RemoveInventory(string inventory)
			{
			throw new Exception("DomainModel->RemoveInventory() not implemented");
			}
		
		private void RemoveInventoryFromRoom(string room,string inventory)
			{
			throw new Exception("DomainModel->RemoveInventoryFromRoom() not implemented");
			}
		
		
		
		
		public bool SaveModelToDisk(VirtualDomain vd)
			{
			return this.Serialize(vd);
			}

		public bool LoadModelFromDisk(VirtualDomain vd)
			{
			return this.Deserialize(vd);
			}
		
		
		//other domains	
		public List<string> ReferencedDomains
			{
			get {
				List<String> refs = new List<string>();
			
				foreach(DomainReference domRef in this.DomainFileReferences)
					{
					refs.Add(domRef.Name);
					}
				
				return refs;
				}
			}
		
		public void AddDomainReference(string domainFile)
			{
			//make sure the name has no extra directory info, is lowercase and without spaces,
			//and doesn't have an file extension
			domainFile = DomainModel.ConvertDomainFileToDomainName(domainFile);
			
			this.Include.AddDomainReference(domainFile,domainFile);
			//this.DomainFileReferences.Add(new DomainReference(domainFile));
			
			
			//as much as I hate using messageboxes in the model, I really need to display this without
			//making the domain file parser stop it's loop
			try {
				this.DomainFileReferences.Add(this.ParseXmlFileReferences(domainFile));
				}
			catch(DomainModelException exception)
				{
				System.Windows.Forms.MessageBox.Show(exception.Message);
				}
			
			//gotta open up the xml file and see it's guts. Then import all of those items
			//into this domain's lists.
			return;
			}
		
		public bool RemoveDomainReference(string domainFile)
			{
			return false;
			}
		
		public static string ConvertDomainFileToDomainName(string domainFile)
			{
			string[] temp = domainFile.Split('\\');
			domainFile = temp[temp.Length-1];
			temp = domainFile.Split('.');
			domainFile = temp[0];
			domainFile = IncludeFile.ConvertToValidDirectoryName(domainFile);
			
			return domainFile;
			}
		#endregion
		
		
		#region private methods
		private void SetRoomFunctionCalls(string roomName,FunctionCallsCollection functionCalls)
			{
			System.Diagnostics.Debug.Assert(roomName != null && roomName != "" && functionCalls != null);
			
			if(!RoomNameMap.ContainsKey(roomName))
				{
				throw new DomainModelException("Room could not be found: " + roomName);
				}
				
			RoomNameMap[roomName].FunctionCalls = functionCalls;
			}
		
		private void SetRoomEditorStates(string roomName,RoomEditorState state)
			{
			System.Diagnostics.Debug.Assert(roomName != null && roomName != "");
			
			if(!RoomNameMap.ContainsKey(roomName))
				{
				throw new DomainModelException("Room could not be found: " + roomName);
				}
				
			RoomNameMap[roomName].EditorState = state;
			}
		
		private FunctionCallsCollection GetRoomFunctionCalls(string roomName)
			{
			System.Diagnostics.Debug.Assert(roomName != null && roomName != "");
			
			//get the room
			if(!RoomNameMap.ContainsKey(roomName))
				{
				throw new DomainModelException("Room could not be found: " + roomName);
				}
						
			return RoomNameMap[roomName].FunctionCalls;
			}
		
		private RoomEditorState GetRoomEditorStates(string roomName)
			{
			System.Diagnostics.Debug.Assert(roomName != null && roomName != "");
			
			if(!RoomNameMap.ContainsKey(roomName))
				{
				throw new DomainModelException("Room could not be found: " + roomName);
				}
						
			return RoomNameMap[roomName].EditorState;
			}
		
		private bool LoadRoom(string roomName)
			{
			if(roomName.Length == 0 || RoomNameMap.ContainsKey(this.DomainFileReferences[THIS_DOMAIN].ConvertRoomNameToFullPath(roomName)))
				{
				return false;
				}
			
			RoomModel room;
			try{
				room = new RoomModel(roomName,this);
				}
			catch(DomainModelException exc)
				{
				throw new DomainModelException(exc.Message);
				}
			Rooms.AddLast(room);
			RoomNameMap.Add(this.DomainFileReferences[THIS_DOMAIN].ConvertRoomNameToFullPath(roomName),Rooms.Last.Value);
			//we have to get the raw roomname from linked list
			this.DomainFileReferences[THIS_DOMAIN].Rooms.Add(Include.GetPathDefinitionIdentifier(ItemSaveType.Room,this.Name) + " \"/" + Rooms.Last.Value.RawFileName + "\"",new FileReferenceModel(roomName,ItemSaveType.Room,Include,this.Name));
						
			return true;
			}	
		
		private bool Serialize(VirtualDomain vd)
			{
			//generates directory structure, domain include header, and domain master xml file
			Stellarmap.DeadSouls.Globals.DomainDirectories.ValidateDirectoriesExist(this.RootDirectory);
			Include.CreateFile(this.RootDirectory);

			string roomPath = this.RootDirectory + "\\" + Include.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Room);
			DeadSouls.Globals.DomainFiles.GenerateFiles(this.RootDirectory);
			GenerateDomainXMLFile(vd);
			
			//generate lpc room files
			foreach(RoomModel room in this.Rooms)
				{
				room.SaveModelToDisk(roomPath);
				}
			return true;
			}
		
		private bool Deserialize(VirtualDomain vd)
			{
			//the workspace was already reset so the root directory should be good to go
			//find the master XML file and begin disecting the directory layout and file locations
			string masterFile = this.RootDirectory + "\\" + this.Name + ".xml";
			if(!System.IO.File.Exists(masterFile))
				{throw new DomainModelException("No domain master file found. Cannot load domain.");}
			
			Sluggy.Utility.XmlParser xml;
			try{
				bool validFile = false;
				xml = new Sluggy.Utility.XmlParser(masterFile);
				if(xml.Contents.Children[0].Name == "domain")
					{
					validFile = true;
					Sluggy.Utility.Tag domainTag = xml.Contents.Children[0];
					foreach(Sluggy.Utility.Tag tag in domainTag.Children)
						{
						//HACK ALERT - might as well just hardcode a giant ugly-ass switch while I'm at it
						//look for the 'room' directory tag
						#region room tag parsing
						if(tag.Name == "directory" && tag.Attributes["name"] == "room")
							{
							//we now have the room directory, start loading room files
							foreach(Sluggy.Utility.Tag roomTag in tag.Children)
								{
								string roomName,iconName;
								int xPos,yPos;
								
								if(roomTag.Attributes.ContainsKey("name"))
									{roomName = roomTag.Attributes["name"];}
								else{throw new DomainModelException("Missing room name in domain master file. Canceling domain laoding. Please manually edit the master xml file to reflect all room files.");}
								if(roomTag.Attributes.ContainsKey("x"))
									{xPos = System.Convert.ToInt32(roomTag.Attributes["x"]);}
								else{xPos = 0;}
								if(roomTag.Attributes.ContainsKey("y"))
									{yPos = System.Convert.ToInt32(roomTag.Attributes["y"]);}
								else{yPos = 0;}
								if(roomTag.Attributes.ContainsKey("icon"))
									{iconName = roomTag.Attributes["icon"];}
								else{iconName = Globals.ImageBoxProperties.DefaultIconDesc;}
								
								
								//failure to load a room does not necesitate failure to load the domain
								try{this.LoadRoom(roomName);}
								catch(Sluggy.Utility.XMLParserException exception)
									{
									//throw new DomainModelException("Invalid master XML file. Could not load domain.",exception);
									System.Windows.Forms.MessageBox.Show("Failure loading room file '" + roomName + "' due to parsing error.\n\n" + exception.Message);
									}
								catch(DomainModelException exception)
									{
									System.Windows.Forms.MessageBox.Show(exception.Message);
									}
								
								vd.AddImage(new System.Drawing.Point(xPos,yPos),
											Globals.ImageBoxProperties.DefaultImage,
											this.DomainFileReferences[THIS_DOMAIN].ConvertRoomNameToFullPath(roomName),
											iconName);
								//the room object still needs it's iocn bitmap to be set
								//however, the bitmaps loaded from the config file are only
								//available to the DomainViewForm so it will have to deal with this		
								}
							//break;
							}
						#endregion
						
						#region door tag parsing
						if(tag.Name == "directory" && tag.Attributes["name"] == "doors")
							{
							foreach(Sluggy.Utility.Tag doorTag in tag.Children)
								{
								if(doorTag.Attributes.ContainsKey("name") && doorTag.Attributes.ContainsKey("class"))
									{
									string name = doorTag.Attributes["name"];
									string doorClass = doorTag.Attributes["class"];

									if(!DomainFileReferences[THIS_DOMAIN].Inventory.ContainsKey(name))
										{
										FileReferenceModel mod = new FileReferenceModel(name,Include.DetermineSaveType(doorClass),Include,this.Name);
										DomainFileReferences[THIS_DOMAIN].Doors.Add(name,mod);
										}
									}
								}
							}
						#endregion
						
						#region item tag parsing
						if(tag.Name == "directory" && tag.Attributes["name"] == "inventory")
							{
							foreach(Sluggy.Utility.Tag itemTag in tag.Children)
								{
								if(itemTag.Attributes.ContainsKey("name") && itemTag.Attributes.ContainsKey("class"))
									{
									string name = itemTag.Attributes["name"];
									string itemClass = itemTag.Attributes["class"];

									if(!DomainFileReferences[THIS_DOMAIN].Inventory.ContainsKey(name))
										{
										FileReferenceModel mod = new FileReferenceModel(name,Include.DetermineSaveType(itemClass),Include,this.Name);
										DomainFileReferences[THIS_DOMAIN].Inventory.Add(name,mod);
										}
									}
								}
							}
						#endregion
						
						#region domain references
						if(tag.Name == "directory" && tag.Attributes["name"] == "references")
							{
							foreach(Sluggy.Utility.Tag itemTag in tag.Children)
								{
								if(itemTag.Attributes.ContainsKey("name"))
									{
									string name = itemTag.Attributes["name"];
									if(name != this.Name && name != "domain")
										{	
										this.AddDomainReference(name);
										}
									}
								}
							}
						#endregion
						//break;
						}
					}
				if(!validFile)
					{
					throw new DomainModelException("Invalid master XML file. Could not load domain.");
					}
				}
			catch(Sluggy.Utility.XMLParserException exception)
				{
				throw new DomainModelException("Failed to parse domain master XML file.\n\n" + exception.Message);
				}
			catch(System.IndexOutOfRangeException exception)
				{
				throw new DomainModelException("Corrupted master XML file. Could not load domain.",exception);
				}
			catch(System.ArgumentOutOfRangeException exception)
				{
				throw new DomainModelException("Corrupted master XML file. Could not load domain.",exception);
				}
			
			return true;
			}
		
		private DomainReference ParseXmlFileReferences(string domainFileName)
			{
			DomainReference domRef = new DomainReference(domainFileName);

			//the workspace was already reset so the root directory should be good to go
			//find the master XML file and begin disecting the directory layout and file locations
			string masterFile = Globals.Dirs.DomainReferences + "\\" + domainFileName + ".xml";
			if(!System.IO.File.Exists(masterFile))
				{ throw new DomainModelException("External domain reference " + domainFileName + " is missing."); }
			
			Sluggy.Utility.XmlParser xml;
			try{
				bool validFile = false;
				xml = new Sluggy.Utility.XmlParser(masterFile);
				if(xml.Contents.Children[0].Name == "domain")
					{
					validFile = true;
					Sluggy.Utility.Tag domainTag = xml.Contents.Children[0];
					foreach(Sluggy.Utility.Tag tag in domainTag.Children)
						{
						//HACK ALERT - might as well just hardcode a giant ugly-ass switch while I'm at it
						
						//remember, this is for a domain reference file. We aren't loading an files, just getting their
						//names and importing them into the workspace.
						
						#region room tag parsing
						if(tag.Name == "directory" && tag.Attributes["name"] == "room")
						    {
						    foreach(Sluggy.Utility.Tag roomTag in tag.Children)
						        {
						        string roomName;
								
						        if(roomTag.Attributes.ContainsKey("name"))
						            { roomName = roomTag.Attributes["name"]; }
						        else { throw new DomainModelException("Missing room name in external domain " + domainFileName + "."); }
								
						        domRef.Rooms.Add(domRef.ConvertRoomNameToFullPath(roomName),new FileReferenceModel(roomName,ItemSaveType.Room,Include,domainFileName));
								}
						    //break;
						    }
						
						#endregion

						#region item tag parsing
						if(tag.Name == "directory" && tag.Attributes["name"] == "inventory")
							{
							foreach(Sluggy.Utility.Tag itemTag in tag.Children)
								{
								if(itemTag.Attributes.ContainsKey("name") && itemTag.Attributes.ContainsKey("class"))
									{
									string name = itemTag.Attributes["name"];
									string itemClass = itemTag.Attributes["class"];

									domRef.Inventory.Add(domRef.ConvertRoomNameToFullPath(name),new FileReferenceModel(name,Include.DetermineSaveType(itemClass),Include,domainFileName));
									}
								}
							}
						#endregion
						//break;
						}
					}
				if(!validFile)
					{
					throw new DomainModelException("Invalid master XML file. Could not load domain.");
					}
				}
			catch(Sluggy.Utility.XMLParserException exception)
				{
				throw new DomainModelException("Failed to parse domain master XML file.\n\n" + exception.Message);
				}
			catch(System.IndexOutOfRangeException exception)
				{
				throw new DomainModelException("Corrupted master XML file. Could not load domain.",exception);
				}
			catch(System.ArgumentOutOfRangeException exception)
				{
				throw new DomainModelException("Corrupted master XML file. Could not load domain.",exception);
				}
			return domRef;
			}	
		
		private void GenerateDomainXMLFile(VirtualDomain vd)
			{
			StringBuilder str = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
			str.Append("\n<domain name=\"" + this.DomainName + "\" >\n");
			str.Append("\t<lpc name=\"include.h\" type=\"header\" />\n");
			
			
			//save list of room files
			str.Append("\t<directory name=\"room\">\n");
			foreach(RoomModel room in this.Rooms)
				{
				str.Append("\t\t<lpc name=\"" + room.RawFileName + "\" type=\"roomfile\" x=\"" + vd.GetIconPosition(room.FileName).X + "\" y=\"" + vd.GetIconPosition(room.FileName).Y + "\" icon=\"" + vd.GetIconDesc(room.FileName) + "\"/>\n");
				}
			str.Append("\t</directory>\n");
			
			//doors
			str.Append("\t<directory name=\"doors\">\n");
			foreach(FileReferenceModel door in DomainFileReferences[THIS_DOMAIN].Doors.Values)
				{
				str.Append("\t\t<lpc name=\"" + door.RawName + "\" type=\"inventory\" class=\"" + door.Type.ToString().ToLower() + "\" />\n");
				}
			str.Append("\t</directory>\n");
			
			//save list of other assorted inventory files (npc/items/etc)
			str.Append("\t<directory name=\"inventory\">\n");
			foreach(FileReferenceModel item in DomainFileReferences[THIS_DOMAIN].Inventory.Values)
				{
				str.Append("\t\t<lpc name=\"" + item.RawName + "\" type=\"inventory\" class=\"" + item.Type.ToString().ToLower() + "\" />\n");
				}
			str.Append("\t</directory>\n");
			
			//referenced domains
			str.Append("\t<directory name=\"references\">\n");
			foreach(DomainReference domRef in DomainFileReferences)
				{
				if(domRef.Name != this.Name && domRef.Name != "domain")
					{
					str.Append("\t\t<ref name=\"" + domRef.Name + "\" />\n");
					}
				}
			str.Append("\t</directory>\n");
			
			str.Append("</domain>\n");
			System.IO.Directory.SetCurrentDirectory(this.RootDirectory);
			using(System.IO.StreamWriter stream = new StreamWriter(this.DomainName + ".xml"))
				{
				stream.Write(str);
				stream.Flush();
				}
			
			}
		
		
		#endregion
		}
	}
