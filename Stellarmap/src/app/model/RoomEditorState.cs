using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	/// <summary>
	/// Used to save state of controls that don't represent function calls
	/// but still need to be preserved on a per-room basis
	/// </summary>
	public struct RoomEditorState
		{
		public bool UseDayNight;
		public bool Restaurant;
		public List<string> InteractiveStaticItems; //used for populating the reading/searches/enters/smells/sounds/etc boxes
		public List<string> EnterableItems;
		public List<string> ExitItems;
		public Dictionary<string,string> ExitsList;
		public Dictionary<string,string> EntersList;
		public Dictionary<string,string> DoorsList;
		
		public RoomEditorState(bool useDayNight)
			{
			UseDayNight = useDayNight;
			Restaurant = false;
			InteractiveStaticItems = new List<string>();
			EnterableItems = new List<string>();
			ExitItems = new List<string>();
			ExitsList = new Dictionary<string,string>();
			EntersList = new Dictionary<string,string>();
			DoorsList = new Dictionary<string,string>();
			}
		
		public void Reset()
			{
			InteractiveStaticItems = new List<string>();
			EnterableItems = new List<string>();
			ExitItems = new List<string>();
			ExitsList = new Dictionary<string,string>();
			EntersList = new Dictionary<string,string>();
			}
		
		public List<string> CopyStaticItemsFromControl(MapBuilder itemsControl)
			{
			//list all static items
			string param = itemsControl.PullEntry();
			Dictionary<string,string> entries = ParserTools.StringIntoMap(param,"RoomEditorState.CopyStaticItemsFromControl()");
			List<string> list = new List<string>(20);
			foreach(string item in entries.Keys)
				{
				list.Add(ParserTools.ProcessInputText(item,EntryType.Mixed));
				}

			List<string> StaticInteractions = new List<string>(20);
			List<string> Exits = new List<string>(20);
			List<string> Doors = new List<string>(5);


			//copy the data to the room
			StaticInteractions.Add("default");
			StaticInteractions.AddRange(list);
			this.InteractiveStaticItems.Clear();
			this.InteractiveStaticItems.AddRange(StaticInteractions);

			this.EnterableItems.Clear();
			this.EnterableItems.AddRange(list);

			Exits.AddRange(new string[] { "north","south","east","west","northwest","northeast","southwest","southeast","up","down","out" });
			Exits.AddRange(list);
			this.ExitItems.Clear();
			this.ExitItems.AddRange(Exits);
			//this.ExitItems.AddRange(this.SetExits.KeyCollection);
			
			return list;
			}

		
		}
	}
