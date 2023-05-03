
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Parser = Stellarmass.LPC.Parser;
using Lexer = Stellarmass.LPC.Lexer;


namespace Stellarmap
	{
    public enum RoomType
        {
        Normal,
        Shop,
        Instance,
        PoliceStation,
        JailCell,
        }

	public class RoomModel :  ICloneable
		{
		#region members
		//editor states (yeah, I'm getting lazy with my bindings now).
		//Used to save control states for non-function controls
		public RoomEditorState EditorState = new RoomEditorState();
        public RoomType Type = RoomType.Normal;
		
		//general states
		public FunctionCallsCollection FunctionCalls = new FunctionCallsCollection();
		public string FileName;
		public string RawFileName;
		public DecomposedCode Code = new DecomposedCode();
		#endregion
		
		public RoomModel(FunctionCallsCollection functionCalls,string fileName,DomainModel model,RoomType type)
			{
			FunctionCalls = functionCalls;
			FileName = fileName;
			RawFileName = RoomModel.ConvertFullPathToRoomName(fileName);
			
			//try loading pre-exiting file first. If you can't,
			//then create one from the default text file asset
			string roomPath = model.DomainRootDir + System.IO.Path.DirectorySeparatorChar.ToString() + model.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Room);
			string fullFilePath = roomPath + System.IO.Path.DirectorySeparatorChar.ToString() + RawFileName;
			
			if(System.IO.File.Exists(fullFilePath))
				{
				Code.LoadLPC(fullFilePath);
				}
			else{
                switch(type)
                    {
                    case RoomType.Normal:           {Code.CreateLPCFromScratch(DeadSoulsObjectType.Room); break;}
                    case RoomType.Shop:             {Code.CreateLPCFromScratch(DeadSoulsObjectType.Shop); break;}
                    case RoomType.Instance:         {Code.CreateLPCFromScratch(DeadSoulsObjectType.InstanceRoom); break;}
                    case RoomType.PoliceStation:    {Code.CreateLPCFromScratch(DeadSoulsObjectType.PoliceOffice); break;}
                    case RoomType.JailCell:    {Code.CreateLPCFromScratch(DeadSoulsObjectType.JailCell); break;}
                    default:                        {Code.CreateLPCFromScratch(DeadSoulsObjectType.Room); break;}
                    }
				//Code.CreateLPCFromScratch(DeadSoulsObjectType.Room);
				}
			
			//PushDataToCode();
			}
		
		public RoomModel(string roomName,DomainModel model)
			{
			FunctionCalls = new FunctionCallsCollection();//functionCalls;
			FileName = RoomModel.ConvertRoomNameToFullPath(roomName);
			RawFileName = roomName;
			
			
			//try loading pre-exiting file first. If you can't,
			//then create one from the default text file asset
			string roomPath = model.DomainRootDir + System.IO.Path.DirectorySeparatorChar.ToString() + model.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Room);
			string fullFilePath = roomPath + System.IO.Path.DirectorySeparatorChar.ToString() + RawFileName;
			
			if(System.IO.File.Exists(fullFilePath))
				{
				Code.LoadLPC(fullFilePath);
				}
			else
				{
				//failed to load
				throw new DomainModelException("Could not find file for room: " + roomName);
				}
			
			this.EditorState.Reset();
			PullDataFromCode();
			}
		
		public bool SaveModelToDisk(string dir)
			{
			PushDataToCode(); //update parse map

			StringBuilder str = new StringBuilder();
			foreach(Parser.Token token in this.Code.Tokens)
				{
				str.Append(token.GetCode(Globals.WorkspaceSave.LineEndingMap[Globals.WorkspaceSave.LineEndings]));
				}
			
			using(System.IO.StreamWriter stream = new System.IO.StreamWriter(dir + System.IO.Path.DirectorySeparatorChar.ToString() + this.RawFileName,false,Globals.WorkspaceSave.LPCEncoding))
				{
				stream.Write(str);
				stream.Flush();
				}
			
			return true;
			}
		
		public object Clone()
			{
			return this.InnerClone();
			}
		
		protected RoomModel InnerClone()
			{
			RoomModel s = this.MemberwiseClone() as RoomModel;
			return s;
			}
		
		/// <summary>
		/// Obtains values from parsed LPC code and applies them to
		/// the room model's data.
		/// </summary>
		/// <param name="?"></param>
		private void PullDataFromCode()
			{
			//first, find main function
			Parser.Token body = this.Code.GetFunctionBody("create");
			if(body == null)
				{
				MessageBox.Show("DEBUG ERROR: Room model's code failed to load properly. The 'create' function body was not found.");
				return;
				}
			
			//don't assume we have children in the function body
			if(body.Children.Count < 2)
				{
				MessageBox.Show("DEBUG ERROR: Missing function body for 'create'. The Parser has mismatched token children.");
				return;
				}
			
			//build a list of all function calls found in the LPC object
			Dictionary<string,Parser.Token> list = this.Code.GetAllFunctionCalls(body);
			
			//update all function call controls if they have matching info from the aforementioned list
			foreach(string dataCall in list.Keys)
				{
				const int FUNCTION_PARAMS = 0;
				if(list.ContainsKey(dataCall))
					{
					StringBuilder str = new StringBuilder("");
					
					//can't use foreach because some children may be null
					for(int index = 0; index < list[dataCall].Children.Count; index++)
						{
						if(list[dataCall].Children != null && list[dataCall].Children[FUNCTION_PARAMS] != null)
							{
							str.Append(list[dataCall].Children[FUNCTION_PARAMS][index].Code);
							}
						}
					this.FunctionCalls.DefineCall(dataCall,str.ToString());
					}
				}
			
			//update exists and enters list
			if(this.FunctionCalls.CallList.ContainsKey("SetExits"))
				{
				this.EditorState.ExitsList = ParserTools.StringIntoMap(this.FunctionCalls.CallList["SetExits"],"SetExits");
				}
			if(this.FunctionCalls.CallList.ContainsKey("SetEnters"))
				{
				this.EditorState.EntersList = ParserTools.StringIntoMap(this.FunctionCalls.CallList["SetEnters"],"SetEnters");
				}

            //check for evidence that the room supports day/night settings for light or long descriptions
            if(this.FunctionCalls.CallList.ContainsKey("SetDayLong") ||
               this.FunctionCalls.CallList.ContainsKey("SetNightLong") ||
               this.FunctionCalls.CallList.ContainsKey("SetDayLight") ||
               this.FunctionCalls.CallList.ContainsKey("SetNightLight") )
				{
                this.EditorState.UseDayNight = true;
                }
            
			}
		
		/// <summary>
		/// Converts the room model's data back into parsed LPC.
		/// </summary>
		private void PushDataToCode()
			{
			//first, find main function
			Parser.Token body = this.Code.GetFunctionBody("create");
			if(body == null)
				{
				MessageBox.Show("DEBUG ERROR: Room model's code failed to update properly. The 'create' function body was not found.");
				return;
				}
			
			//assume we have children in the function body
			if(body.Children.Count < 2)
				{
				MessageBox.Show("DEBUG ERROR: Missing function body for 'create'. Parser mismatched token children.");
				return;
				}
			
			//build a list of all function calls that already exist and just need to be changed
			Dictionary<string,Parser.Token> list = this.Code.GetAllFunctionCalls(body);
			List<string> remaining = new List<string>(this.FunctionCalls.CallList.Keys);
			
			//build list of relevant function calls within body
			foreach(string codeCall in list.Keys)
				{
				foreach(string dataCall in this.FunctionCalls.CallList.Keys)
					{
					if(dataCall == null || dataCall.Length < 1)
						{
						MessageBox.Show("Warning! A function with no name was produced for '"+ this.RawFileName + "'. The flawed data has not been written but you you should verify the contents of the file.");
						}
					else{
						if(dataCall == codeCall.Trim())
							{
							//edit currently existsing data within the parsed LPC object
							const int FUNCTION_CALL_PARAMS = 0;
							
							string parameters = this.FunctionCalls.CallList[dataCall];
							if(parameters.Length < 1 || parameters == "({})" || parameters == "([])" || parameters == "\"\"")
								{
								//don't remove if it's the create function
								if(dataCall == "create")
									{
									list[dataCall].SetTextAsTokens(parameters,FUNCTION_CALL_PARAMS);
									remaining.Remove(dataCall);
									}
								else{
									RemoveFunctionCallFromCode(dataCall,body);
									}
								}
							else{
								string encodedText = parameters;
								list[dataCall].SetTextAsTokens(encodedText,FUNCTION_CALL_PARAMS);
								remaining.Remove(dataCall);
								}
							
							}
						}
					}
				}
			
			//now deal with all the remaining function calls from
			//the model that aren't in the LPC parse map yet.
			List<string> removedFunctions = new List<string>();
			foreach(string dataCall in remaining)
				{
				const int FUNCTION_BODY = 1;
				string parameters = this.FunctionCalls.CallList[dataCall];
				//add functiuons that don't exist within the parsed LPC
				//but do exist within the model.  But only add them it the
				//the parameters aren't empty
				if(parameters.Length > 0 && parameters != null && parameters != "({})" && parameters != "([])" && parameters != "\"\"")
					{	
					body.InsertTextAsTokensEnd("\t" + dataCall +"();\n",FUNCTION_BODY);
					}
				else{removedFunctions.Add(dataCall);}
				}
			
			//loop through again to fill in the parameters for everything we just created
			//if the parameters are empty remove that function call from the code
			list = this.Code.GetAllFunctionCalls(body);
			foreach(string dataCall in remaining)
				{
				if(dataCall == null || dataCall.Length < 1)
					{
					MessageBox.Show("Warning! A function with no name was produced for '"+ this.RawFileName + "'. The flawed data has not been written but you you should verify the contents of the file.");
					}
				else{
					if(!removedFunctions.Contains(dataCall))
						{
						const int FUNCTION_CALL_PARAMS = 0;
						string parameters = this.FunctionCalls.CallList[dataCall];
						list[dataCall].SetTextAsTokens(parameters,FUNCTION_CALL_PARAMS);
						}
					}
				}

			foreach(string file in FunctionCalls.AdditionalIncludes)
				{
				this.Code.AddInclude(file);
				}
			return;
			}
		
		/// <summary>
		/// This function 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="root"></param>
		private void RemoveFunctionCallFromCode(string name,Parser.Token root)
			{
			const int FUNC_BODY_TOKENS = 1;
			
			
			//foreach(Parser.Token token in root.Children[FUNC_BODY_TOKENS])
			for(int count = 0; count < root.Children[FUNC_BODY_TOKENS].Count; count++)
				{
				Parser.Token token = root.Children[FUNC_BODY_TOKENS][count];
				
				if(token.Type == Parser.TokenType.FunctionCall)
					{
					string funcName = "";
					//gotta find out the form of the function call with respect to
					//the scope resolution operator
					for(int index = 0; index < token.Lexmes.Count; index++)
						{
						Lexer.Lexme lexme = token.Lexmes[index];
						
						if(lexme.Data.ToString() == "::")
							{
							funcName = token.Lexmes[index+1].Data.ToString();
							break;
							}
						if(lexme.Type == Lexer.LexmeType.Identifier)
							{
							//look ahead
							if(token.Lexmes[index+1].Data.ToString() == "::")
								{
								funcName = token.Lexmes[index+2].Data.ToString();
								break;
								}
							else{
								//found the name of the currently investigated grammar token.
								//is it the one we are looking for?
								funcName = lexme.Data.ToString();
								if(name == funcName)
									{
									Parser.Token funccall = root.Children[FUNC_BODY_TOKENS][count];
									Parser.Token pretab = null,newline = null;
									
									if(count > 0)
										{pretab = root.Children[FUNC_BODY_TOKENS][count-1];}
									if(root.Children[FUNC_BODY_TOKENS].Count-1 <= count+1)
										{newline = root.Children[FUNC_BODY_TOKENS][count+1];}
									
									
									//WARNING - hackish!
									//This parameter is used to remove the tab token and newline token that were
									//placed after the function back in 'PushDataToCode'. It should only be set
									//if you knoew for sure there is a tab before this function and a newline after.
									//It is used to remove all that exra space generated when a new function is created and then never used.
									
									//yup
									root.Children[FUNC_BODY_TOKENS].Remove(funccall);
									//if(pretab != null)	{root.Children[FUNC_BODY_TOKENS].Remove(pretab);}
									//if(newline != null)	{root.Children[FUNC_BODY_TOKENS].Remove(newline);}
									
									
									
									}
								break;
								}
							}
						}
					}
				}
			return;
			}


		#region static methods
		private static string ConvertRoomNameToFullPath(string roomName)
			{
			return "DOMAIN_ROOM \"/" + roomName + "\"";
			}

		private static string ConvertFullPathToRoomName(string fileName)
			{
			fileName = fileName.Replace("DOMAIN_ROOM \"/","");
			fileName = fileName.Replace("\"","");
			return fileName;
			}
		#endregion
		}
	}
