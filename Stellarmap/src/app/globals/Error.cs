using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	
	public class InvalidFunctionControlParamException : Exception
		{
		public InvalidFunctionControlParamException()
			{			
			}
		
		public InvalidFunctionControlParamException(string message)
			: base(message)
			{
			}
		
		public InvalidFunctionControlParamException(string message, Exception inner)
			: base(message,inner)
			{
			}
		}
	
	namespace Globals
		{
		public static class ErrorStrings
			{
			readonly public static string FunctionControlErrorTitle = "Function Control Parsing Error";
			}
		}
	}