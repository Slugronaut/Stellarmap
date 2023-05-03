using System;
using System.Collections.Generic;
using System.Text;

using Lexer = Stellarmass.LPC.Lexer;
using Stellarmass.LPC.Lexer;

namespace Stellarmass.LPC.Parser
	{
	/// <summary>
	/// Contains a linear array of lexmes as a hierarchy where lexmes within braces, brackets, or parentheses are
	/// considered children of the lexmes before it. Each lexme is still stored with it's original linear index
	/// number for fast lookup.
	/// </summary>
	internal class LexmeHierarchy
		{
		public List<LexmeContainer> LexmeContainers;
		public LexmeHierarchy Parent;
		public int LexmeCount = 0;
		
		public LexmeHierarchy(List<Lexer.Lexme> lexmes,LexmeHierarchy parent,int index,int recursionDepth)
			{
			LexmeContainer ActiveContainer;
			LexmeContainers = new List<LexmeContainer>();
			Parent = parent;
			

			//for(int index = 0; index < lexmes.Count; index++)
			while(index < lexmes.Count)
				{
				Lexer.Lexme lexme = lexmes[index]; //shortcut to convert from a foreach loop that was previously used
				string lexstr = lexme.Data.ToString();

				if(LPC.LPCKeywords.IsClosingPair(lexstr))
					{
					if (recursionDepth < 1)
						{
						throw new ParserException("Unmatched " + lexstr + "found.");
						}

					recursionDepth--;
					return;
					}
				
				LexmeContainers.Add(new LexmeContainer(lexme,index));
				ActiveContainer = LexmeContainers[LexmeContainers.Count-1];
				this.LexmeCount++;
				
				if(LPC.LPCKeywords.IsOpenningPair(lexstr))
					{
					//we have stepped into the realm of recursion, watch your step!
					ActiveContainer.Children = new LexmeHierarchy(lexmes,this,index+1,recursionDepth+1);
					
					//in order to track indexing correctly through recursion each hierarchy
					//needs to store not only the number of lexmecontainers it has but the
					//recursed number within each sub-hierarhchy within each lexmecontainer.
					//If there were no consumed lexmes in the recursion we can just remove the children stub.
					//(UPDATE: Actually, we can't remove that stub, I need it XD )
					this.LexmeCount += ActiveContainer.Children.LexmeCount;
					index += ActiveContainer.Children.LexmeCount;

					//we know now that the next lexme is ending the recursion so we can just consume it now
					this.LexmeCount++;
					index++;
					LexmeContainers.Add(new LexmeContainer(lexmes[index],index));
					
					//remove empty children stub
					//UPDATE: nevermind, I need it
					/*if(ActiveContainer.Children.LexmeCount <= 0)
						{
						ActiveContainer.Children = null;
						}*/
					}
				
				
				index++;
				}//end foreach

			return;
			}
		
		}


	internal class LexmeContainer
		{
		public Lexer.Lexme Lexme;
		public int Index = 0;
		public LexmeHierarchy Children;
		public int SyntaxID = 0; //0 means unknown token
		
		public LexmeContainer(Lexer.Lexme lexme, int index)
			{
			Lexme = lexme;
			Index = index;
			}
		}
	
	
	/// <summary>
	/// A collection of rules that may be concurrently traversed in
	/// order to identify a particular LPC language construct
	/// </summary>
	public class ParseMap
		{
		private List<Syntax> Constructs = new List<Syntax>();
		
		
		public ParseMap(List<Syntax> constructs)
			{
			if(constructs == null) throw new System.ArgumentNullException();
			
			Constructs = constructs;
			}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lexmes"></param>
		/// <returns></returns>
		public List<Token> Parse(List<Lexer.Lexme> lexmes)
			{
			//arranges lexmes in a hierarchy for easier pasring
			LexmeHierarchy group = new LexmeHierarchy(lexmes,null,0,0);

			//unit test
			//Console.WriteLine("---- Hierarchy ----\n");
			//Console.WriteLine(PrintHierarchy(group).ToString() + "\n\n");

			//tag all lexmes as having been ided or not. Also collects know tokens
			List<Token> tokens = TagLexmes(group);

			//review remaining tokens that are tagged unknown
			return tokens;
			}	
		
		private void ResetConstructs(List<Syntax> constructs)
			{
			foreach(Syntax con in constructs)
				{
				con.ResetValidation(true);
				}
			}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="hier"></param>
		/// <returns></returns>
		private StringBuilder PrintHierarchy(LexmeHierarchy hier)
			{
			StringBuilder str = new StringBuilder();

			foreach(LexmeContainer con in hier.LexmeContainers)
				{
				str.Append(con.Lexme.Data.ToString());
				if(con.Children != null)
					{
					str.Append(PrintHierarchy(con.Children));
					}
				}

			return str;
			}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="spawn"></param>
		private void ScanSpawnedGrammars(Lexer.Lexme activeLexme,ref LinkedList<Syntax> spawned,List<Token> identifiedTokens)
			{
			int spawnedCount = 0;
			LinkedList<Syntax> removalList = new LinkedList<Syntax>();
			foreach(Syntax spawn in spawned)
				{
				//if a spawned item is complete and it is the front of the list, we have our man
				if(spawn.IsComplete() && spawn == spawned.First.Value)
					{
					//we got ooooooonnne!  *riiiiiiing*
					int startIndex = spawn.StartIndex;
					identifiedTokens.Add(new Token(spawn.LexmeToken.Lexmes,spawn.TokenID));
					

					//ok, now every spawned syntax that overlaps with this one's lexme indices
					//can also be ruled out. However, non overlaping ones naturally come afterward so
					//we just leave them alone. They may be validating our next token already.
					//(Note this also removes the spawn we just identified)
					foreach(Syntax de in spawned)
						{
						if(de.StartIndex <= startIndex)
							{
							removalList.AddLast(de);
							}
						}
					spawnedCount = 0;
					}
				//nope, continue validating any noncompleted ones until they are ruled out or completed
				else
					{
					if(!spawn.IsAcceptableInput(activeLexme) && !spawn.IsComplete())
						{
						//this puppy is ruled out, remove from list
						//spawned.Remove(spawn);
						removalList.AddLast(spawn);
						}
					}
				spawnedCount++;
				}
			
			//finally, remove the unwanted fluff
			foreach(Syntax syn in removalList)
				{
				spawned.Remove(syn);
				}
			}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="hier"></param>
		/// <returns></returns>
		private List<Token> ParseGrammars(LexmeHierarchy hier)
			{
			
			List<Token> identifiedTokens = new List<Token>(100);
			Lexer.Lexme activeLexme;
			List<Syntax> syntaxes = new List<Syntax>(this.Constructs); //a local copy of the global syntax rules
			List<Syntax> candidates = new List<Syntax>();
			
			LinkedList<Syntax> spawned = new LinkedList<Syntax>();
			
			
			//loop through each lexme at a time while comparing to all syntaxes.
			//We must continue with this until all syntaxes have either been ruled out or
			//confirmed as valid. Once that has happened we must figure out which of the valid
			//rules is actually the right one
			for(int lexmeContainerIndex = 0; lexmeContainerIndex < hier.LexmeContainers.Count; lexmeContainerIndex++)
				{
				try
					{
					activeLexme = hier.LexmeContainers[lexmeContainerIndex].Lexme;
					activeLexme.Index = lexmeContainerIndex;
					}
				catch(IndexOutOfRangeException)
					{
					Console.WriteLine("Index error during lexme tagging in ParseMap.");
					return null;
					}
				
				
				//build a list of syntaxes that can spawn from this lexme index
				foreach(Syntax syntax in syntaxes)
					{
					if(syntax.IsAcceptableInput(activeLexme))
						{
						spawned.AddLast(new Syntax(syntax));
						syntax.ResetValidation(true);

						//we need to reset this because it will be check again below
						spawned.Last.Value.ResetValidation(true);
						}
					}
				ResetConstructs(syntaxes);
				
				ScanSpawnedGrammars(activeLexme,ref spawned,identifiedTokens);
				
				
				}//lexme index for-loop
			
			//buffer flush
			return identifiedTokens;
			}	
		
		/// <summary>
		/// Runs through each lexme in the hierarchy recursively, comparing each collection of lexmes
		/// to all known syntax constructs until each lexme container is tagged with an index that identifies
		/// which syntax it belongs too.
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		private List<Token> TagLexmes(LexmeHierarchy hier)
			{
			//go through every lexme and compare against every grammar combo until all known
			//ones have been identified.
			List<Token> identifiedTokens = ParseGrammars(hier);
			
			// (SUPERDUPER SLOW)
			//now we need to go back and flag all of our identified lexmes in each token
			//we also need to perform recursion for any children
			foreach(Token token in identifiedTokens)
				{
				token.Lexmes[0].Flag = LexmeFlag.BeginKnownToken;
				}

			//gathers up all unidentified lexmes and places them into unknow tokens at the correct locations
			List<Token> allTokens = CollectAllRemainingTokens(hier,identifiedTokens);

			//now to perform recursion on all tokens
			try{
				allTokens =  RecurseTokenization(hier,allTokens);
			}
			catch(IndexOutOfRangeException)
				{
				Console.WriteLine("Recursion index error in ParseMap.");
				}
			return allTokens;
			}
		
		/// <summary>
		/// Takes a pre-identified set of tokens and a lexme hierarchy and fills in the spaces
		/// by identifying the 'unknow' token types (oximoron, I know)
		/// </summary>
		/// <param name="hier"></param>
		/// <param name="knownTokens"></param>
		/// <returns></returns>
		private List<Token> CollectAllRemainingTokens(LexmeHierarchy hier,List<Token> knownTokens)
			{
			Lexer.Lexme activeLexme;
			List<Token> finalTokenList = new List<Token>();
			List<Lexer.Lexme> unknownLexmes = new List<Lexme>();
			int knownTokenUse = 0;

			for(int allLexmeContainerIndex = 0; allLexmeContainerIndex < hier.LexmeContainers.Count; allLexmeContainerIndex++)
				{
				activeLexme = hier.LexmeContainers[allLexmeContainerIndex].Lexme;
				activeLexme.Index = allLexmeContainerIndex;
				

				//if the token is unset we know if belongs to an unknow token. It will then be added
				//to a group that will be finalized when we finally come across a know token or have
				//finished the list.
				if(activeLexme.Flag == LexmeFlag.Unset)
					{
					unknownLexmes.Add(activeLexme);
					activeLexme.Flag = LexmeFlag.BelongsToUnidentifiedToken;
					}
				else {
					//now we can add any previously built up list of unknowns before adding the known
					finalTokenList.Add(new Token(unknownLexmes,TokenType.Unknown));
					finalTokenList.Add(knownTokens[knownTokenUse]);

					//reset the list
					unknownLexmes = new List<Lexme>();

					//we are assuming that it must be a 'LexmeFlag.BeginKnownToken' since we will
					//the skip to the next unknown one
					allLexmeContainerIndex += knownTokens[knownTokenUse].Lexmes.Count - 1; //minus 1 because the loop will take of that last one
					knownTokenUse++;	
					}
				}

			//check for any dangling unknowns that were triggered due to a lack of knowns
			if(unknownLexmes.Count > 0)
				{
				finalTokenList.Add(new Token(unknownLexmes,TokenType.Unknown));
				}

			return finalTokenList;
			}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="allTokens"></param>
		/// <returns></returns>
		private List<Token> RecurseTokenization(LexmeHierarchy hier,List<Token> allTokens)
			{
			if(allTokens == null) return null;

			foreach(Token token in allTokens)
				{
				foreach(Lexer.Lexme lexme in token.Lexmes)
					{
					//flag each lexme as identified so that we don't have to worry about it later
					if(lexme.Flag != LexmeFlag.BelongsToUnidentifiedToken)
						{lexme.Flag = LexmeFlag.Set;}
					//did the lexme container have a child?
					//UPDATED: J.C. If a lexme container has a child we do not bother to check the count
					//we simply copy all elements, even if there are none, to the child count for the lexme.
					//Otherwise, it will get out of sync if we have multiple potential child-bearing lexmes (yeah, I said it)
					//but only one child which would then be attributed to the first parent regardless of
					//its pregnacy.
					//
					//Wow.
					//
					//Metaphor.
					//
					if(hier.LexmeContainers[lexme.Index].Children != null)
						{
						if(hier.LexmeContainers[lexme.Index].Children.LexmeContainers.Count > 0)
							{token.Children.Add(TagLexmes(hier.LexmeContainers[lexme.Index].Children));}
						else {token.Children.Add(null);}
						}
					/*
					if(hier.LexmeContainers[lexme.Index].Children != null && 
					   hier.LexmeContainers[lexme.Index].Children.LexmeContainers.Count > 0)
						{
						//recurse
						//token.Children.Add(new List<Token>());
						//token.Children[token.Children.Count-1] = TagLexmes(hier.LexmeContainers[lexme.Index].Children);

						token.Children.Add(TagLexmes(hier.LexmeContainers[lexme.Index].Children));
						}
					 **/
					}
				}

			return allTokens;
			}
		
		}
	}

	
	