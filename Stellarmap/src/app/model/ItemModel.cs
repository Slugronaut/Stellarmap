using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Parser = Stellarmass.LPC.Parser;
using Lexer = Stellarmass.LPC.Lexer;

namespace Stellarmap
	{
	public enum ItemSaveType
		{
		Weapon,
		Armor,
		Object,
		Door,
		Meal,
		NPC,
		Room,
		Unknown,
		}
	
	public enum ItemLoadType
		{
		Item,
		Armor,
		Door,
		Npc,
		Room,
		}
	
	public class ItemModel
		{
		#region members
		
		private const string InheritRemovalValueReplacer = ""; //when removing 'inherit' commands from code, this is what they are replaced with
		IDomainModelAdapter refMVCDomain;
		DecomposedCode Code = new DecomposedCode();
		#endregion
		
		
		#region public interface
		
		public ItemModel(IDomainModelAdapter domain,ItemLoadType loadType)
			{
			this.refMVCDomain = domain;
			
			switch(loadType)
				{
				case ItemLoadType.Door:
					{
					Code.CreateLPCFromScratch(DeadSoulsObjectType.Door);
					break;
					}
				case ItemLoadType.Armor:
					{
					Code.CreateLPCFromScratch(DeadSoulsObjectType.Armor);
					break;
					}
				case ItemLoadType.Npc:
					{
					Code.CreateLPCFromScratch(DeadSoulsObjectType.Npc);
					break;
					}
				default:
					{
					Code.CreateLPCFromScratch(DeadSoulsObjectType.Item);
					break;
					}
				}
			
			
			}
		
		public bool SaveModelToDisk(FunctionCallsCollection functions,FunctionCallsCollection inherits,ItemSaveType saveType,string fileName)
			{
			//ensure item directories exists
			Stellarmap.DeadSouls.Globals.DomainDirectories.ValidateDirectoriesExist(this.refMVCDomain.DomainRootDir);
			
			
			//determine the path the item will be saved to
			StringBuilder itemPath = new StringBuilder(this.refMVCDomain.DomainRootDir + "\\");
			switch(saveType)
				{
				case ItemSaveType.Weapon:
					{
					itemPath.Append(this.refMVCDomain.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Weapon));
					break;
					}
				case ItemSaveType.Armor:
					{
					itemPath.Append(this.refMVCDomain.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Armor));
					break;
					}
				case ItemSaveType.Door:
					{
					itemPath.Append(this.refMVCDomain.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Door));
					break;
					}
				case ItemSaveType.Meal:
					{
					itemPath.Append(this.refMVCDomain.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Meal));
					break;
					}
				case ItemSaveType.NPC:
					{
					itemPath.Append(this.refMVCDomain.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Npc));
					break;
					}
				case ItemSaveType.Room:
					{
					itemPath.Append(this.refMVCDomain.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Room));
					break;
					}
				default:
					{
					//default to Objects directory
					itemPath.Append(this.refMVCDomain.IncludeFile.StripDeadSoulsPath(DeadSouls.Globals.DomainDirectories.Object));
					break;
					}
				}
			
			itemPath.Append("\\" + fileName);
			
			if(!itemPath.ToString().EndsWith(Globals.Model.RoomExtension))
				{
				itemPath.Append(Globals.Model.RoomExtension);
				}
			
			//TODO: choose save path based on type
			return this.Serialize(itemPath.ToString(),functions,inherits);
			}

		public void DeleteFunctionsInCode(string bodyName)
			{
			const int FUNC_PARAMS = 0;
			const int FUNC_BODY = 1;
			Parser.Token body = this.Code.GetFunctionBody(bodyName);
			Dictionary<string,Parser.Token> list = this.Code.GetAllFunctionCalls(body);
			
			body.Children[FUNC_BODY] = new List<Stellarmass.LPC.Parser.Token>();
			}
		
		public void AddLine(string line,string bodyName)
			{
			const int FUNC_BODY = 1;
			Parser.Token body = this.Code.GetFunctionBody(bodyName);
			body.InsertTextAsTokensBeginning(line,FUNC_BODY);
			}
		#endregion

		
		#region private methods
		
		private bool Serialize(string filePath,FunctionCallsCollection functions,FunctionCallsCollection inherits)
			{
			PushFunctionDataToCode(functions); //update parse map
			PushInheritDataToCode(inherits);
			
			StringBuilder str = new StringBuilder();
			foreach(Parser.Token token in this.Code.Tokens)
				{
				str.Append(token.GetCode(Globals.WorkspaceSave.LineEndingMap[Globals.WorkspaceSave.LineEndings]));
				}
			
			//make sure this is what we want
			if(System.IO.File.Exists(filePath))
				{
				if(MessageBox.Show("A file with this name already exists in this location. Do you want to overwrite it?","File Already Exists",MessageBoxButtons.YesNo) == DialogResult.No)
					{
					return false;
					}
				}

			using(System.IO.StreamWriter stream = new System.IO.StreamWriter(filePath,false,Globals.WorkspaceSave.LPCEncoding))
				{
				stream.Write(str);
				stream.Flush();
				}
			
			return true;
			}
		
		/// <summary>
		/// Converts the room model's data back into parsed LPC.
		/// </summary>
		private void PushFunctionDataToCode(FunctionCallsCollection functions)
			{
			//first, find main function
			Parser.Token body = this.Code.GetFunctionBody("create");
			if(body == null)
				{
				MessageBox.Show("DEBUG ERROR: Item model's code failed to update properly. The 'create' function body was not found.");
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
			List<string> remaining = new List<string>(functions.CallList.Keys);

			//build list of relevant function calls within body
			foreach(string codeCall in list.Keys)
				{
				foreach(string dataCall in functions.CallList.Keys)
					{
					if(dataCall == codeCall.Trim())
						{
						//edit currently existsing data within the parsed LPC object
						if(list[dataCall].Type == Parser.TokenType.FunctionCall)
							{
							//make sure hash symbol is removed before adding
							string[] temp = dataCall.Split(Globals.Generator.FunctionHashSymbol.ToCharArray());
							string functionName = temp[0];
							
							//edit exiting function
							const int FUNCTION_CALL_PARAMS = 0;
							list[dataCall].SetTextAsTokens(functions.CallList[functionName],FUNCTION_CALL_PARAMS);
							}
						remaining.Remove(dataCall);
						}
					}
				}

			//now deal with all the remaining function calls from
			//the model that aren't in the LPC parse map yet.
			List<string> removedFunctions = new List<string>();
			foreach(string dataCall in remaining)
				{
				/*
				const int FUNCTION_BODY = 1;
				//make sure hash symbol is removed before adding
				string[] temp = dataCall.Split(Globals.Generator.FunctionHashSymbol.ToCharArray());
				string functionName = temp[0];
				
				//add functiuons that don't exist within the parsed LPC
				//but do exist within the model
				body.InsertTextAsTokensEnd("\t" + functionName + "();\n",FUNCTION_BODY);
				 */

				const int FUNCTION_BODY = 1;
				//make sure hash symbol is removed before adding
				string parameters = functions.CallList[dataCall];
				if(parameters.Length > 0 && parameters != null && parameters != "({})" && parameters != "([])" && parameters != "\"\"")
					{
					string[] temp = dataCall.Split(Globals.Generator.FunctionHashSymbol.ToCharArray());
					string functionName = temp[0];

					//add functiuons that don't exist within the parsed LPC
					//but do exist within the model
				
					body.InsertTextAsTokensEnd("\t" + functionName + "();\n",FUNCTION_BODY);
					}
				else{
					removedFunctions.Add(dataCall);
					}
				}
			
			//loop through again to fill in the parameters for everything we just created
			list = this.Code.GetAllFunctionCalls(body);
			
			//HACK ALERT!!!
			//when we entered the functions with the hash symbol they used whatever value was tagged onto the end of them
			//However, now they are using a number count assigned by the 'GetAllFunctionCalls()' which will inevitably make
			//the results turn sour. So we need to convert all hashed function values to a numeric system, while
			//also keeing in mind that the first function with a given name is not given a hash symbol by 'GetAllFunctionCalls()'.
			//Basically we are going to tack on a numbered count after each instance of a function call except the first.
			Dictionary<string,int> funcHashCount = new Dictionary<string,int>();
			
			foreach(string dataCall in remaining)
				{
				string functionKey = dataCall;

				if(!removedFunctions.Contains(dataCall))
					{
					//hack alert: see above
					if(functionKey.Contains(Globals.Generator.FunctionHashSymbol))
						{
						string[] temp = functionKey.Split(Globals.Generator.FunctionHashSymbol.ToCharArray());
						functionKey = temp[0];
						//don't add hash to first instance of a function call
						if(!funcHashCount.ContainsKey(functionKey))
							{
							funcHashCount.Add(functionKey,0);
							}
						else{
							funcHashCount[functionKey]++;
							functionKey = functionKey + Globals.Generator.FunctionHashSymbol + funcHashCount[functionKey];
							}
						
						}
					//end hack alert
					
					const int FUNCTION_CALL_PARAMS = 0;
					if(functionKey != null && functionKey.Length > 0 && list[functionKey].Type == Parser.TokenType.FunctionCall)
						{
						//make sure hash symbol is removed before adding
						string[] temp = functionKey.Split(Globals.Generator.FunctionHashSymbol.ToCharArray());
						string functionName = temp[0];
						
						list[functionKey].SetTextAsTokens(functions.CallList[dataCall],FUNCTION_CALL_PARAMS);
						}
					}//end if not in removed
				}

			foreach(string file in functions.AdditionalIncludes)
				{
				this.Code.AddInclude(file);
				}
			
			return;
			}
		
		private void PushInheritDataToCode(FunctionCallsCollection inherits)
			{
			//assume we have children in the function body
			if(this.Code.Tokens.Count < 2)
				{
				MessageBox.Show("DEBUG ERROR: Missing function body for 'create'. Parser mismatched token children.");
				return;
				}

			//build a list of all inherits that already exist and just need to be changed
			Dictionary<string,Parser.Token> list = this.Code.GetAllInherits();
			List<string> remaining = new List<string>(inherits.CallList.Keys);
			
			//edit or remove ihherits that already exist in the code
			foreach(string codeCall in list.Keys)
				{
				foreach(string dataCall in inherits.CallList.Keys)
					{
					string temp = codeCall.Trim();
					if(dataCall == temp)
						{
						//edit currently existsing data within the parsed LPC object
						if(list[dataCall].Type == Parser.TokenType.InheritCommand)
							{
							//do we remove or edit?
							if(inherits.RemoveFlag.ContainsKey(dataCall))
								{
								//remove
								list[dataCall].SetTextAsThisToken(ItemModel.InheritRemovalValueReplacer);
								}
							}
						remaining.Remove(dataCall);
						}
					}
				}
			
			//now we add the model inherits to the code...maybe
			foreach(string dataCall in remaining)
				{
				//do we add or just move on?
				if(!inherits.RemoveFlag.ContainsKey(dataCall))
					{
					//we can add it
					this.Code.AddInherit(dataCall);
					}
				}
			
			return;
			}
		
		#endregion	
		}
	}
