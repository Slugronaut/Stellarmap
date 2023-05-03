using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public class FunctionCallsCollection
		{
		public Dictionary<string,string> CallList = new Dictionary<string,string>(); //function name /  parmeter string
		public Dictionary<string,bool> RemoveFlag = new Dictionary<string,bool>(); //used as a flag for outside code to pass along state. Useful when remove unwanted tokens
		public List<string>	AdditionalIncludes = new List<string>();
		
		public FunctionCallsCollection()
			{
			}
		
		public FunctionCallsCollection(Dictionary<string,string> callList)
			{
			CallList = callList;
			}
		
		public void AddHeaderFile(string header)
			{
			if(header != null && header.Length > 0 && !AdditionalIncludes.Contains(header))
				{
				AdditionalIncludes.Add(header);
				}
			}
		
		public void DefineCall(string func,string parameters)
			{
			if(CallList.ContainsKey(func))
				{
				//edit exisiting function
				CallList[func] = parameters;
				}
			else{
				//add new function
				CallList.Add(func,parameters);
				}
			}
		
		public bool RemoveCall(string func)
			{
			if(CallList.ContainsKey(func))
				{
				CallList.Remove(func);
				return true;
				}
			return false;
			}
				
		}
	}
