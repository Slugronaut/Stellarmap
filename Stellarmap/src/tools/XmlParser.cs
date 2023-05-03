using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;


namespace Sluggy.Utility
	{
	public class XMLParserException : Exception
		{
		public XMLParserException() {}
		public XMLParserException(string message) : base(message){}
		public XMLParserException(string message,Exception inner) : base(message,inner) {}	
		}
	
	/// <summary>
	/// Respresents a single tag within an XML file. The XMLParser class
	/// stores a hierarchial list of these in memory after it is done
	/// reading a file
	/// </summary>
	public class Tag
		{
		public string Name = "Root";
		public string Value = "";
		public XmlNodeType Type;
		public int Depth = -1;
		public Dictionary<string,string> Attributes = new Dictionary<string,string>();
		public bool EmptyElement = false;
		public List<Tag> Children = new List<Tag>();
		public Tag Parent = null;
		
		public StringBuilder ToXML(string indent)
			{
			StringBuilder stream = new StringBuilder();
			if(Name == "Root" && Depth == -1)
				{
				stream.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
				foreach(Tag t in Children)
					{
					stream.Append(t.ToXML(indent + "\t"));
					}
				return stream;
				}
			
			stream.Append("\n" + indent +"<");
			stream.Append(Name);
			foreach(string attr in Attributes.Keys)
				{
				stream.Append(" " + attr + "=\"" + Attributes[attr] + "\"");
				}
			
			if(EmptyElement)
				{
				stream.Append(" />");
				}
			else{
				if(Children != null && Children.Count > 0)
					{
					//write children
					stream.Append(">");
					foreach(Tag t in Children)
						{
						stream.Append(t.ToXML(indent + "\t"));
						}
					stream.Append("\n" + indent + "</" + Name + ">");
					}
				
				else if(Value.Length > 0)
					{
					//no children
					stream.Append(">");
					stream.Append(Value);
					stream.Append("</" + Name + ">");
					}
				else{
					stream.Append("></" + Name + ">");
					}
				}
			
			return stream;
			}
		}
	
	/// <summary>
	/// An XML file parser. Reads in all tags and stores them in memory for sorting and searching.
	/// Useful as a base class to any specific XML file parsing objects.
	/// </summary>
	public class XmlParser
		{
		protected Tag Root = new Tag();
		protected string Path;
		
		public XmlParser(string fileName)
			{
			XmlReaderSettings xrs = new XmlReaderSettings();
			xrs.CheckCharacters = false; //magically makes the reader stop choking on certain things
			//System.IO.StreamReader sr = System.IO.File.OpenText(fileName);
			System.IO.StreamReader s = new System.IO.StreamReader(fileName,Encoding.UTF8);
			Path = fileName;
			
			using(XmlTextReader r = new XmlTextReader(s) )
			    {
				if(r != null)
					{
					Root.Children = new List<Tag>();

					r.Read(); //read past starting xml tag
					ParseTag(r,Root,null);
					//DisplayDebugTags(this.Root);
					}
				}
			}
		
		public string FilePath
			{
			get{return Path;}
			}
		
		public Tag Contents
			{
			get {return Root;}
			}
		
		private Tag ParseTag(XmlTextReader reader,Tag root,Tag last)
			{
			Tag t;			
			
			while(reader.Read())
				{
				t = new Tag();
				
				t.Name = reader.Name;
				t.Type = reader.NodeType;
				t.Depth = reader.Depth;
				
				
				switch(reader.NodeType)
					{					
					case XmlNodeType.EndElement:
						{
						//we are assuming this won't be called until after
						//a start tag so 'last' *should* be valid
						if(last == null)
							{
							throw new XMLParserException("Invalid XML Format. End element found before matching start element. Name: " + t.Name);
							}
						
						//sibling of root? this should happen only
						//after a recursive call due to t being a
						//child of the last tag
						Tag sibling = last;
						
						//we are searching for the matching sibling of this tag.
						//loop backwards through depth levels until it is found
						while(t.Depth < sibling.Depth)
							{
							sibling = sibling.Parent;
							}
						
						if(sibling.Name != t.Name)
							{
							throw new XMLParserException("Invalid XML Format. End element does not match start element of same depth. Name: " + t.Name);
							}
						
						//MessageBox.Show("Going backwards looking for silbing of: " + t.Name + "\n\nSibling Choice: " + sibling.Name + "\n\nParent of it: " + sibling.Parent.Name);						
						//NOTE: we are not going to save these tags, so there is no need
						//to add it to the parent's list of children.
						return sibling.Parent;						
						}
					
					case XmlNodeType.Element:
						{
						//check for and read attributes, return to element afterward
						if(reader.HasAttributes)
							{
							while(reader.MoveToNextAttribute())
								{
								t.Attributes.Add(reader.Name,reader.Value);
								}
							
							//go back to element
							reader.MoveToElement();
							}
						
						//is empty tag
						reader.MoveToElement();
						t.EmptyElement = reader.IsEmptyElement;
						
							
						//and now check for children
						if(t.Depth == root.Depth + 1)
							{
							//We know it is a child of the current root
							//because it is EXACTLY one level higher.
							t.Parent = root;
							root.Children.Add(t);
							last = t;
							break;
							}
						if(t.Depth > root.Depth + 1)
							{
							//More than one level higher, it must be a child
							//of the last element.
							t.Parent = last;
							last.Children.Add(t);								
							
							//begin recursion, return last processed tag
							last = ParseTag(reader,last,t);
							break;
							}
						break;
						}
					
					case XmlNodeType.Text:
						{						
						//This is the contents of the last tag parsed.
						last.Value = reader.Value;
						break;
						}
					
					default:
						{
						//fuck this shit
						break;
						}
					}					
					
				//release the local reference so that it 
				//is clean for the next loop iteration
				t = null;
				}
			
			return null;
			}
		
		public void DisplayDebugTags(Tag root)
			{
			if(root.Children == null) return;
			
			foreach(Tag t in root.Children)
				{											
				string s;
				s = "Name: " + t.Name;
				s += "\nVaue: " + t.Value;
				s += "\nDepth: " + t.Depth;
				s += "\nType: " + t.Type.ToString();
				s += "\n\nAttributes: ";
				foreach(string str in t.Attributes.Keys)
				    {
				    s += "\n	-" + str;
				    }				
				s += "\n\nParent: " + root.Name;

				MessageBox.Show(s);
				
				//yay recursion!
				if(t.Children.Count > 0)
					{
					DisplayDebugTags(t);
					}
				}
			}
		
		public List<Tag> FindAll(Tag root,string tagName)
			{
			List<Tag> tags = new List<Tag>();
			if(root == null) root = this.Root;
			if(root.Children == null)	return null;
			
			foreach(Tag t in root.Children)
				{
				if(t.Name == tagName)
					{
					tags.Add(t);
					}
				else{					
					//yay more recursion!
					if(t.Children.Count > 0)
						{
						List<Tag> temp = FindAll(t,tagName);
						if(temp != null)
							{
							tags.AddRange(temp);
							}
						temp = null;
						}
					}
				}
						
			if(tags.Count < 1)
				{
				tags = null;
				}			
			return tags;
			}
		
		/// <summary>
		/// A breadth-only search for a specifically named tag. No recursive
		/// searching of child nodes is performed.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="tagName"></param>
		/// <returns></returns>
		public Tag QuickSeekFirst(Tag root,string tagName)
			{
			if(root == null) root = this.Root;
			if(root.Children == null)	return null;
			
			foreach(Tag t in root.Children)
				{
				if(t.Name == tagName)
					{
					return t;
					}
				}
			return null;
			}
		
		/// <summary>
		/// A depth-first search of an entire tag tree using recursion.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="tagName"></param>
		/// <returns></returns>
		public Tag DepthSeekFirst(Tag root,string tagName)
			{
			if(root == null) root = this.Root;
			if(root.Children == null)	return null;
			Tag temp;
			
			foreach(Tag t in root.Children)
				{
				if(t.Name == tagName)	return t;
				else{
					temp = DepthSeekFirst(t,tagName);
					if(temp != null && temp.Name == tagName)	return temp;
					}
				}
			
			return null;
			}
		
		/// <summary>
		/// A breadth-first search of an entire tag tree using recursion.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="tagName"></param>
		/// <returns></returns>
		public Tag BreadthSeekFirst(Tag root,string tagName)
			{
			if(root == null) root = this.Root;
			Tag tag = QuickSeekFirst(root,tagName);
			if(tag != null)	return tag;
			
			foreach(Tag t in root.Children)
				{
				return BreadthSeekFirst(t,tagName);
				}
			
			return null;
			}
		
		public string GetTagAttribute(Tag tag,string attributeName)
			{
			return tag.Attributes[attributeName];
			}
		
		public string GetTagValue(Tag tag,string tagName)
			{
			return tag.Value;
			}
		
		public bool Save(string filePath)
			{
			if(Root.Children == null) return false;
			
			StringBuilder text = this.Contents.ToXML("");

			using(System.IO.StreamWriter stream = new System.IO.StreamWriter(filePath))
				{
				stream.Write(text);
				stream.Flush();
				}
			
			return true;
			}
		}
	}