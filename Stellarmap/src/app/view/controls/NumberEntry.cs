using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	/// <summary>
	/// Represents a basic function call with a single string as a parameter.
	/// </summary>
	public partial class NumberEntry : AFunctionControl
		{
		FuncParamType ParamType = FuncParamType.Number;
		
		
		public NumberEntry()
			{
			InitializeComponent();
			this.Entry.Validated += new EventHandler(PostUpdate);
			}
		
		public NumberEntry(string labelText,int defaultValue,int min,int max) : this()
			{
			this.LabelText = labelText;	
			this.Entry.Value = defaultValue;
			this.Entry.Minimum = min;
			this.Entry.Maximum = max;
			}

        public NumberEntry(string labelText,decimal defaultValue,int min,int max) : this()
			{
			this.LabelText = labelText;	
			this.Entry.Value = defaultValue;
			this.Entry.Minimum = min;
			this.Entry.Maximum = max;
			}
		
		
		#region properties		
		public override ControlType FunctionControlType
			{
			get { return ControlType.NumberEntry; }
			}
		
		public override FuncParamType ParameterType
			{
			set {
				if(value != FuncParamType.Number)
					{
					throw new Stellarmap.InvalidFunctionControlParamException(this.ToString() + " Control \"" + Label.Text + "\" did not receive a valid FuncParamType."); 		
					}	
				this.ParamType = value;
				}
			get {
				return this.ParamType;
				}
			}

		public override string LabelText
			{
			set {
				this.Label.Text = value;
				
				//position entry text box just after label (it may have been resized for text)
				int tempY = Entry.Location.Y;
				this.Entry.Location = new System.Drawing.Point(this.Label.Location.X + this.Label.Width + (int)Metrics.SpaceAfterLable,tempY);
			
				//Resize container control to hold all of the contents
				tempY = this.Height;
				this.Size = new System.Drawing.Size((int)Metrics.SpaceAfterLable + (int)Metrics.CustomControlBorder + this.Label.Location.X + this.Label.Width + this.Entry.Width,tempY);
				}
			get {
				return this.Label.Text;
				}
			}
		
		public decimal EntryValue
			{
			set	{
				if(value > Entry.Maximum) {value = Entry.Maximum;}
				if(value < Entry.Minimum) {value = Entry.Minimum;}
				Entry.Value = value;
				}
			get {
				return Entry.Value;
				}
			}
		
		public decimal EntryIncrement
			{
			set {
				Entry.Increment = value;
				}
			get {
				return Entry.Increment;
				}
			}
		
		public decimal EntryMinimum
			{
			set {
				Entry.Minimum = value;
				}
			get {
				return Entry.Minimum;
				}
			}
		
		public decimal EntryMaxmimum
			{
			set {
				Entry.Maximum = value;
				}
			get {
				return Entry.Maximum;
				}
			}	

        public int DecimalPlaces
            {
            set {
                Entry.DecimalPlaces = value;
                }
            get {
                return Entry.DecimalPlaces;
                }
            }
		#endregion
		
		
		#region public interface
		/// <summary>
		/// Returns the contents of the control formatted as a single string
		/// so that it can be used in code generation.
		/// </summary>
		public override string PullEntry()
			{
			if(EntryValue < EntryMinimum || EntryValue > EntryMaxmimum)
				{
				this.EntryValue = this.EntryMinimum;				
				}
			
			return this.EntryValue.ToString();
			}
		
		/// <summary>
		/// Parses a string into it's component parts for use in the control's native format.
		/// </summary>
		public override void PushEntry(string entry)
			{
			//check to see if it was a string literal or an identifier name
			if(entry[0] == '\"' &&  entry[entry.Length-1] == '\"')
				{				
				throw new FunctionControlException("String literal passed to a number box " + this.FunctionName + ".");
				}
			else{
                if(this.DecimalPlaces == 0)
                    {
				    try {
                        this.EntryValue = Convert.ToInt32(entry);
					    }
				    catch(System.FormatException e)
					    {
					    throw new FunctionControlException("Variable name or floting-point number passed to integer number box " + this.FunctionName + ".",e);
					    }
                    }
                else{
                    try {
                        this.EntryValue = Convert.ToDecimal(entry);
					    }
				    catch(System.FormatException e)
					    {
					    throw new FunctionControlException("Variable name passed to decimal number box " + this.FunctionName + ".",e);
					    }
                    }
				}
			}
		#endregion
		}
	}
