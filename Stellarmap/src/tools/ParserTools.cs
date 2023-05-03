using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public class ParserException : Exception
		{
		public ParserException() : base() {}
		public ParserException(string message) : base(message) {}
		public ParserException(string message,Exception inner) : base(message,inner) {}
		}
	
	
	public static class ParserTools
		{
		#region string parse
		public static Dictionary<string,string> StringIntoMap(string str,string agent)
			{
			List<StringBuilder> parsed;
			List<string> list = new List<string>();
			Dictionary<string,string> pairs = new Dictionary<string,string>();
			
			
			//make sure it is a map
			str = str.Trim();
			int len = str.Length;
			
			if(str.Length < 4 || str[0] != '(' || str[1] != '[' || str[len-2] != ']' || str[len-1] != ')')
			    {
			    throw new ParserException("Invalid mapping passed to " + agent + ".");
				}

            				
			str = str.Replace("([","");
			str = str.Replace("])","");
            // HACK ALERT
            // If the mapping was constructed on multiple lines (as QCS and many LP coders tend to do)
            // the trailing letter could be anything from newlines to tabs and spaces. As much as I wanted
            // to avoid changing previously existing code, the easiest way was to simply strip white space
            // before checking for the trailing comma (that comma is important because the parser will choke on it)
            str = str.TrimEnd();
			str = str.TrimEnd(','); //don't forget any trailing commas
			
			//create and sanitize list of elements
			try {
				parsed = ParseCode(str,',');
				}
			catch(ParserException e)
				{
				throw new ParserException(e.Message + " in " + agent + ".");
				}
			
			foreach(StringBuilder element in parsed)
				{
				//now parse each mapping pair
				List<StringBuilder> temp;
				string trimKey,trimValue;
				
				try {
					temp = ParseCode(element.ToString(),':');//	list.Add(element.ToString());
					}
				catch(ParserException e)
					{
					throw new ParserException(e.Message + " in " + agent + ".");
					}
				
				if(temp.Count != 2)
					{
					throw new ParserException("Invalid key/value pair in " + agent + ".");
					}
				
				
				//remove trailing whitespace on each element
				trimKey = temp[0].ToString();
				trimKey = trimKey.Trim();
				
				trimValue = temp[1].ToString();
				trimValue = trimValue.Trim();
				
				pairs.Add(trimKey,trimValue);
				}
			
			return pairs;
			}
						
		public static List<string> StringIntoArray(string str,string agent)
			{
			List<StringBuilder> parsed;
			List<string> list = new List<string>();
			
			
			//make sure it *is* an array
			str = str.Trim();
			int len = str.Length;
			
			if(str.Length < 4 || str[0] != '(' || str[1] != '{' || str[len-2] != '}' || str[len-1] != ')')
			    {
			    throw new ParserException("Invalid array passed to " + agent + ".");
				}
			
			str = str.Replace("({","");
			str = str.Replace("})","");
			str = str.TrimEnd(','); //don't forget any trailing comma
			
			//create and sanitize list of elements
			try {
				parsed = ParseCode(str,',');
				}
			catch(ParserException e)
				{
				throw new ParserException(e.Message + " in " + agent + ".");
				}
			
			foreach(StringBuilder element in parsed)
				{
				list.Add(element.ToString());
				}
			
			//remove trailing space on each token
			for(int index = 0; index < list.Count; index++)
				{
				list[index] = list[index].Trim();
				}
			
			return list;
			}
		
		public static List<string> StringIntoORList(string str,string agent)
			{
			List<string> list = new List<string>();
			
			if(str.Length < 1) return list;
			
			//we are going to assume that all data is passed as variable names or number literals
			//so there is no need for the preceeding '$'. This also means that if we find signs
			//of non-variable name data (quotes) we should throw an exception.
			if(str.Contains("\""))//str[0] == '\"' || str[str.Length-1] == '\"')
				{
				throw new ParserException("Cannot pass a string literal to an ORed list of variables in " + agent + ".");			
				}
			if(str[0] == Stellarmap.Globals.Generator.FunctionParameterLiteralChar)
				{
				throw new ParserException("Variable signifier '" + Stellarmap.Globals.Generator.FunctionParameterLiteral + "' not needed in " + agent + ".");
				}
			
			str = str.Trim();
			
			list.AddRange(str.Split('|'));
			for(int index = 0; index < list.Count; index++)
				{
				list[index] = list[index].Trim();
				}
			
			//remove trailing space on each token
			for(int index = 0; index < list.Count; index++)
				{
				list[index] = list[index].Trim();
				}
			
			return list;
			}	
		#endregion
		
		
		#region string compose
		public static string MapIntoString(Dictionary<string,string> map)
			{
			StringBuilder str = new StringBuilder("([");
			
			foreach(string key in map.Keys)
				{
				str.Append(key + ":" + map[key] + ",");
				}
			
			//remove trailing comma
			if(str[str.Length-1] == ',') str.Remove(str.Length-1,1);
					
			str.Append("])");
			return str.ToString();
			}
		
		public static string ArrayIntoString(List<string> array)
			{
			StringBuilder str = new StringBuilder("({");
			
			foreach(string element in array)
				{
				str.Append(element + ",");
				}
			
			//remove trailing comma
			if(str[str.Length-1] == ',') str.Remove(str.Length-1,1);
					
			str.Append("})");
			return str.ToString();
			}
		
		public static string ORListIntoString(List<string> array)
			{
			//StringBuilder str = new StringBuilder("({");
			if(array.Count < 1)	return "";
			StringBuilder str = new StringBuilder();
			
			foreach(string element in array)
				{
				str.Append(element + "|");
				}
			
			//remove trailing OR
			if(str[str.Length-1] == '|') str.Remove(str.Length-1,1);
					
			//str.Append("})");
			return str.ToString();
			}		
		#endregion
		
		
		#region helpers
        /* 
         * Basically, a highly specialized 'split' function. It is used for breaking apart
         * sections of LPC arrays and mappings.
         * 
         * UPDATED: I've tried to edit things a bit so that it will properly handle mappings
         *          where the key is an array. A long-term improvement would be
         *          to build a proper state-driven mechanism... but screw that.
         */
		public static List<StringBuilder> ParseCode(string str,char seperator)
			{
			List<StringBuilder> list = new List<StringBuilder>(0);
			bool insideStringLiteral = false;
			bool escaped = false;
			bool skipSeperator = false;
            int chunkIndex = 0;

            //stuff for above-mentioned update
            bool subArrayStartChar = false; //set when a '(' is found
            bool subArrayEndChar = false;//set when ')' is found
            int subArrayBodyLevel  = 0;//incremented when a '{' is found right after '(', deincrmeented in reverse conditions
            		
			if(str == "")
				{
				return list;
				}
			
			StringBuilder comp = new StringBuilder();
			list.Add(comp);

            //NOTE: Regexes would probably make this WAAAAY easier
			foreach(char c in str.ToCharArray())
				{
				if(c == '\\')	{escaped = !escaped;}
                
                if(subArrayBodyLevel < 0)
                    {
                    throw new ParserException("Improperly formatted sub array within an element.");
                    }
				if(!insideStringLiteral && subArrayBodyLevel < 1)
					{
					//we are parsing a normal code chunk

					if(escaped)
						{
						throw new ParserException("Illegal escape sequence");
						}

                    //check fro sub-array entry
                    if(subArrayStartChar && c == '{')
                        {
                        subArrayBodyLevel ++;
                        subArrayStartChar = false;
                        }
                    if(c == '(')
                        {subArrayStartChar = true;}
                    else{subArrayStartChar = false;}
                    
                    //check for an invalid sub-array exit
                    if(subArrayStartChar && c == ')')
                        {throw new ParserException("Improperly formatted sub array within an element.");}
                    if(c == '}')
                        {subArrayEndChar = true;}
                    else{subArrayEndChar = false;}
                    
                    
                    if(c == '"')	insideStringLiteral = true;
                    if(c == seperator)
						{
						//skip to next element
						comp = new StringBuilder();
						list.Add(comp);
						chunkIndex++;
						skipSeperator = true;
						}					
					}
                else if(subArrayBodyLevel > 0)
                    {
                    //we are parsing a sub array, just leave whole

                    if(subArrayStartChar && c == '{')
                        {
                        subArrayBodyLevel ++;
                        subArrayStartChar = false;
                        }
                    if(c == '(')
                        {subArrayStartChar = true; }
                    else{subArrayStartChar = false;}
                    

                    if(subArrayEndChar && c == ')')
                        {
                        subArrayBodyLevel --;
                        if(subArrayBodyLevel < 0)
                            {
                            throw new ParserException("Improperly formatted sub array within an element.");
                            }
                        subArrayEndChar = false;
                        }
                    if(c == '}')
                        {subArrayEndChar = true;}
                    else{subArrayEndChar = false;}
                    
                    }
				else{
					//we are parsing an LPC string literal
					if(c == '"' && !escaped)	insideStringLiteral = false;
					if(c == '"' && escaped)		escaped = false;
                    if(c == '\'' && escaped)		escaped = false;
					}
				
				if(!skipSeperator)	list[chunkIndex].Append(c);
				skipSeperator = false;
				}
			
			if(insideStringLiteral)
				{
				throw new ParserException("Unclosed string literal");
				}
            if(subArrayStartChar || subArrayBodyLevel > 0)
                {
                throw new ParserException("Improperly formatted sub array within an element.");
                }
			
					
			return list;
			}
		
		public static string ProcessInputText(string str,EntryType type)
			{
			str = str.Trim();
			if(str.Length < 1) return str;

			switch(type)
				{
				case EntryType.Mixed:
						{
						//first check if it is a variable or a string literal
						if(str[0] == '\"')
							{
							if(str[str.Length - 1] != '\"')
								{
								throw new FunctionControlException("Invalid string literal passed: " + str);
								}
							//literal
							str = str.TrimStart('\"');
							str = str.TrimEnd('\"');
							}
						else
							{
							//identifier
							if(str[0] != Stellarmap.Globals.Generator.FunctionParameterLiteralChar) str = Stellarmap.Globals.Generator.FunctionParameterLiteral + str;
							}
						break;
						}

				case EntryType.Strings:
						{
						if(str[0] == '\"')
							{
							if(str[str.Length - 1] != '\"')
								{
								throw new FunctionControlException("Invalid string literal passed: " + str);
								}
							//literal
							str = str.TrimStart('\"');
							str = str.TrimEnd('\"');
							}
						break;
						}
				}

			return str;
			}	
		#endregion	
		}
	}
