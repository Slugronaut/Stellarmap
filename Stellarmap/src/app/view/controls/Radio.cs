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
	/// 
	/// </summary>
	public partial class Radio : AFunctionControl
		{
		FuncParamType ParamType = FuncParamType.Property;
		string PropertyName;
			
		public Radio()
			{
			InitializeComponent();
            }
		
		public Radio(string labelText,bool ticked,FuncParamType paramType,string propertyName) : this()
			{
			this.ParameterType = paramType;
			this.LabelText = labelText;
			this.Checked = ticked;
			this.PropertyName = propertyName;
			}

        public void SubscribeCheckedChanged(EventHandler eh)
            {
            Label.CheckedChanged += eh;
            }

        public void UnsubscribeCheckedChanged(EventHandler eh)
            {
            Label.CheckedChanged -= eh;
            }
			
		#region properties
		public override ControlType FunctionControlType
			{
			get { return ControlType.CheckBox; }
			}

        public RadioButton InnerRadio
            {
            get {return Label;}
            }

        public override FuncParamType ParameterType
			{
			set {
				if(value != FuncParamType.Property
				&& value != FuncParamType.Number
				&& value != FuncParamType.Raw
				&& value != FuncParamType.Inherit)
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
				
				//Resize container control to hold all of the contents
				int tempY = this.Height;
				this.Size = new System.Drawing.Size((int)Metrics.SpaceAfterLable + (int)Metrics.CustomControlBorder + this.Label.Location.X + this.Label.Width,tempY);
				}
			get {
				return this.Label.Text;
				}
			}
		
		public bool Checked
			{
			set {
				//to avoid potential infinte loops we check first
				if(this.Label.Checked != value)
				    {					
				    this.Label.Checked = value;
				    }
				}
			get {
				return this.Label.Checked;
				}
			}
		#endregion
		
		private void OnCheckedChanged(object sender,EventArgs args)
			{
			this.PostUpdate(this,null);
			}
		
		#region public interface
		public override string PullEntry()
			{			
			if(this.Label.Checked)	return "1";
			return "0";
			}
		
		public override void PushEntry(string entry)
			{
			if(entry != null && entry != "" && entry != "0")
				{
				this.Label.Checked = true;
				}
			else{
				this.Label.Checked = false;
				}
			}
		#endregion
		
		}
		
	}
