using System;
using System.Collections.Generic;
using System.Text;
using Reflection = System.Reflection;

using Lexer = Stellarmass.LPC.Lexer;


namespace Stellarmass.LPC.Parser
	{
	/// <summary>
	/// An object that will automatgically apply all syntax rules defined appropriately
	/// in the 'LPCSyntaxRules' class to the given 'ParseMap'. Some meta-programming reflection
	/// is used to obtain the functions from the companion class.
	/// </summary>
	public class SyntaxRules
		{
		private List<Syntax> Syntaxes;
		private List<string> RuleDefinitions = new List<string>();
		private string RuleDefinitionPrefix = "DefineRules_";
		
		public SyntaxRules(out ParseMap map)
			{
			Syntaxes = CreateRules();
			map = new ParseMap(Syntaxes);
			}
		
		private List<Syntax> CreateRules()
			{
			List<Syntax> list = new List<Syntax>(20);
			LPCSyntaxRulesObject rules = new LPCSyntaxRulesObject();
			Type t = typeof(LPCSyntaxRulesObject);
			
			Reflection.MethodInfo[] mi = t.GetMethods();
			
			foreach(Reflection.MethodInfo info in mi)
				{
				string str = info.Name;
				if(str.StartsWith(RuleDefinitionPrefix))
					{
					object result = info.Invoke(rules,null);
					if(result != null)
						{
						List<Syntax> temp = (List<Syntax>)result;
						list.AddRange(temp);
						}
					}
				}
			
			return list;
			}
		}
	}
