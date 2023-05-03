using System;
using System.Collections.Generic;
using System.Text;

using Lexer = Stellarmass.LPC.Lexer;

namespace Stellarmass.LPC.Parser
	{
	enum RuleMatchType
		{
		ByType,
		ByValue,
		BySubruling,
		}
	
	public enum RuleType
		{
		ValidateOnMatch,
		ValidateAllUntilMatch,
		InvalidateOnMatch,
		}
	
	public enum RuleFlags
	    {
	    DontAllowComments = 1,
	    DontAllowSpaces = 2,
	    DontAllowLinefeeds = 4,
	    }
	
	public enum RuleMatchResult
		{
		Invalid,
		Valid,
		Match,
		Waiting,
		}
	
	
	public class Rule
		{
		#region members
		private readonly int Flags;
		private readonly RuleMatchType MatchType;
		private readonly RuleType Type;
		private bool StopAutoValidate = true;
		
		public readonly bool Optional;
			
		private List<string> Values = new List<string>();
		private List<Lexer.LexmeType> Types = new List<Stellarmass.LPC.Lexer.LexmeType>();
		#endregion
		
		
		#region constructors
		public Rule(RuleType ruleType,bool optionalFlag,string value)
			{
			if(value == null) throw new ArgumentNullException();
			
			Type = ruleType;
			Optional = optionalFlag;
			Values.Add(value);
			MatchType = RuleMatchType.ByValue;
			
			Reset();
			}
		
		public Rule(RuleType ruleType,bool optionalFlag,string[] values)
			{
			if(values == null || values.Length < 1) throw new System.ArgumentNullException();
			
			Type = ruleType;
			Optional = optionalFlag;
			Values.AddRange(values);
			MatchType = RuleMatchType.ByValue;
			
			Reset();
			}
		
		public Rule(RuleType ruleType,bool optionalFlag,Lexer.LexmeType type)
			{
			Type = ruleType;
			Optional = optionalFlag;
			Types.Add(type);
			MatchType = RuleMatchType.ByType;
			
			Reset();
			}
		
		public Rule(RuleType ruleType,bool optionalFlag,Lexer.LexmeType[] types)
			{
			if(types == null || types.Length < 1) throw new ArgumentNullException();
			
			Type = ruleType;
			Optional = optionalFlag;
			Types.AddRange(types);
			MatchType = RuleMatchType.ByType;
			
			Reset();
			}
		
		public Rule(Rule copy)
			{
			this.Flags = copy.Flags;
			this.MatchType = copy.MatchType;
			this.Type = copy.Type;
			this.StopAutoValidate = true;
			this.Optional = copy.Optional;
			this.Values = copy.Values;
			this.Types = copy.Types;
			}
		
		#endregion
		
		
		#region default-value constructors
		public Rule(bool optionalFlag,string value) : this(RuleType.ValidateOnMatch,optionalFlag,value)	{}
		public Rule(bool optionalFlag,string[] values) : this(RuleType.ValidateOnMatch,optionalFlag,values) {}
		public Rule(bool optionalFlag,Lexer.LexmeType type) : this(RuleType.ValidateOnMatch,optionalFlag,type) {}
		public Rule(bool optionalFlag,Lexer.LexmeType[] types) : this(RuleType.ValidateOnMatch,optionalFlag,types) {}
		#endregion
		
		
		
		public RuleMatchResult Validate(Lexer.Lexme lexme)
			{
			//check for the potential that the rule itself is whitespace!
			if(this.Types.Count == 1 && this.Types[0] == Lexer.LexmeType.Whitespace && lexme.Type == Lexer.LexmeType.Whitespace)
				{
				return RuleMatchResult.Match;
				}
			 
			//make sure any whitespace, line feeds and comments are handled properly.
			//They can never be a match but they can be flagged as valid or invalid depending
			//on the rule's settings.
			if(lexme.Type == Lexer.LexmeType.NewLine)
				{
				if((Flags & (int)RuleFlags.DontAllowLinefeeds) != 0) return RuleMatchResult.Invalid;
				else return RuleMatchResult.Valid;
				}
			if(lexme.Type == Lexer.LexmeType.Whitespace)
				{
				if((Flags & (int)RuleFlags.DontAllowSpaces) != 0) return RuleMatchResult.Invalid;
				else return RuleMatchResult.Valid;
				}
			if(lexme.Type == Lexer.LexmeType.Comment)
				{
				if((Flags & (int)RuleFlags.DontAllowComments) != 0) return RuleMatchResult.Invalid;
				else return RuleMatchResult.Valid;
				}
			
			
			RuleMatchResult result = RuleMatchResult.Invalid;
			switch(MatchType)
				{
				case RuleMatchType.ByValue:
					{
					result = ValidateValue(lexme);
					break;
					}
				case RuleMatchType.ByType:
					{
					result = ValidateType(lexme);
					break;
					}
				}
			
			return result;
			}
		
		public void Reset()
			{
			if(this.Type == RuleType.ValidateAllUntilMatch)
				{
				StopAutoValidate = false;
				}
			else{
				StopAutoValidate = true;
				}
			}
		
		
		private RuleMatchResult ValidateValue(Lexer.Lexme lexme)
			{
			foreach(string value in Values)
				{
				if(lexme.Data.ToString() == value)
					{
					StopAutoValidate = true;
					return RuleMatchResult.Match;
					}
				}
			
			if(this.Type == RuleType.ValidateAllUntilMatch && !StopAutoValidate)
			    {
			    return RuleMatchResult.Waiting;
			    }
			
			//no matches, is it still valid?
			if(this.Optional) {return RuleMatchResult.Waiting;}
			else return RuleMatchResult.Invalid;
			}
		
		private RuleMatchResult ValidateType(Lexer.Lexme lexme)
			{
			foreach(Lexer.LexmeType type in Types)
				{
				if(lexme.Type == type)
					{
					StopAutoValidate = true;
					return RuleMatchResult.Match;
					}
				}
			
			if(this.Type == RuleType.ValidateAllUntilMatch && !StopAutoValidate)
			    {
			    return RuleMatchResult.Waiting;
			    }
			
			//no matches, is it still valid?
			if(this.Optional) {return RuleMatchResult.Waiting;}
			else return RuleMatchResult.Invalid;
			}
		
		}
	}
