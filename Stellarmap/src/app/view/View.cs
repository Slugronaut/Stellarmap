using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmap
	{
	public abstract class DomainView
		{
		private IDomainViewAdapter refController;
		
		private GUIMouseStateMachine RenderTargetMouse;
		private VirtualDomain VirtualDomain;
		private ControlRenderDevice RenderDevice;
		
		
		public DomainView()
			{
			}
		
		public void SetSubscriber(IDomainViewAdapter controller)
			{
			refController = controller;
			}
		
		public void DisplayModel(string roomFileName,FunctionCallsCollection functionCalls)
			{
			}
		
		public FunctionCallsCollection PropogateViewToModel(string roomName)
			{
			return null;
			}
		}
	}
