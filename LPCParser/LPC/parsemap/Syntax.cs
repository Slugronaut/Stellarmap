using System;
using System.Collections.Generic;
using System.Text;

using Lexer = Stellarmass.LPC.Lexer;

namespace Stellarmass.LPC.Parser
	{
	/// <summary>
	/// An LPC language syntax construct that contains a token ID for a series of rules.
	/// If the current token violates any internal rule, this construct can
	/// be safely ruled out (no pun intended) of the current token-series parsing.
	/// </summary>
	public class Syntax
		{
		#region members
		private bool Complete = false;
		private int RuleIndex = 0;
		private int Matches = 0;
		private Token Token = new Token(new List<Lexer.Lexme>(),TokenType.none);
		
		public int StartIndex = 0; //used by the parser for grammar matching
		public int Length = 0; //used by the parser for grammar matching
		public readonly TokenType TokenID;
		public readonly List<Rule> Rules;
		#endregion
		
		
		public int ConsumedLexmesCount
			{
			get {
				int result =  this.Token.Lexmes.Count;
				
				foreach(List<Token> child in this.Token.Children)
					{
					foreach(Token tok in child)
						{
						result += tok.Lexmes.Count;
						}
					}
				return result;
				}
			}
				
		public int MatchCount
			{
			get{return Matches;}
			set{Matches = value;}
			}
		
		public Token LexmeToken
			{
			get {return Token;}
			}
		
		
		
		public Syntax(List<Rule> rules,TokenType type)
			{
			if(rules == null) throw new System.ArgumentNullException();
			
			Rules = rules;
			TokenID = type;
			}
		
		public Syntax(Rule[] rules,TokenType type)
			{
			if(rules == null) throw new System.ArgumentNullException();
			
			Rules = new List<Rule>(rules);
			TokenID = type;
			}
		
		public Syntax(Syntax copy)
			{
			this.Complete = copy.Complete;
			this.RuleIndex = copy.RuleIndex;
			this.Matches = copy.Matches;
			this.Rules = new List<Rule>();
			foreach(Rule rule in copy.Rules)
				{
				this.Rules.Add(new Rule(rule));
				}
			this.TokenID = copy.TokenID;
			this.Token = new Token(copy.Token);
			}
		
		public bool IsAcceptableInput(Lexer.Lexme lexme)
			{
			if(Complete) return false;
			Token.Lexmes.Add(lexme);
			
			//Here, we are systematically checking every rule until we have a match or an invalid lexme.
			//In the event of a match we can increment the rules-index so that we start with the next
			//rule the next time this function is called. If we have a match and it was the final rule
			//we set the completed flag.
			
			//Keep in mind that optional rules will still return valid if we don't have a match, this allows us
			//to keep moving forward until we match a rule or we come across a required rule that cannot be
			//matched at which point it is invalid. Thus, we only need to return a success or fail state based
			//on a found match or an invalid lexme.
			for(int i = RuleIndex; i < Rules.Count; i++)
				{
				//-slight hack-
				//we are going to remove any leading whitespace symbols by returning invalid
				if(lexme.Type == Lexer.LexmeType.Whitespace || lexme.Type == Lexer.LexmeType.NewLine)
					{
					if(this.Length < 1)	return false;
					}
				
				switch(Rules[i].Validate(lexme))
					{
					case RuleMatchResult.Match:
						{
						//if we've matched all required rules, flag the construct as completed
						if(i == Rules.Count-1)
							{Complete = true;}
						
						//otherwise, we are at least one step closer
						RuleIndex = i+1;
						Length++;
						return true;
						}
					case RuleMatchResult.Valid:
						{
						//no match but we are still in the game
						Length++;
						return true;
						}
					case RuleMatchResult.Waiting:
						{
						RuleIndex++;
						break;
						}
					case RuleMatchResult.Invalid:
						{
						/*if(Rules[i].Optional)
							{
							RuleIndex = i + 1;
							Length++;
							return true;
							}*/
						ResetValidation(true);
						return false;
						}
					}
				}
			
			
			return false;
			}
		
		public void ResetValidation(bool resetLexmeFlags)
			{
			Complete = false;
			RuleIndex = 0;
			Matches = 0;
			//Lexmes = new List<Stellarmass.LPC.Lexer.Lexme>();
			/*if(resetLexmeFlags)
				{
				foreach(Lexer.Lexme lexme in this.LexmeToken.Lexmes)
					{
					lexme.Flag = Lexer.LexmeFlag.Unset;
					}
				}*/

			Token = new Token(new List<Lexer.Lexme>(),TokenType.none);
			
			foreach(Rule rule in Rules)
				{
				rule.Reset();
				}
			}
		
		public bool IsComplete()
			{
			return Complete;
			}
		
		public void SetChildren(List<List<Token> >children)
			{
			Token.Children = children;
			}	
		
		}
	
	}
