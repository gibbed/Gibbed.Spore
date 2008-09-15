﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Gibbed.Spore.Properties
{
	public abstract class Property
	{
		public abstract void ReadProp(Stream input, bool array);
		public abstract void WriteProp(Stream input, bool array);
		public abstract void WriteXML(XmlWriter output);
		public abstract void ReadXML(XmlReader input);
	}

	public class PropertyDefinitionAttribute : Attribute
	{
		public string Name;
		public string PluralName;
		public ushort FileType;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">Singular name of the property</param>
		/// <param name="pluralName">Plural name of the property</param>
		/// <param name="fileType">The id of the property as represented in a property file</param>
		public PropertyDefinitionAttribute(string name, string pluralName, ushort fileType)
		{
			this.Name = name;
			this.PluralName = pluralName;
			this.FileType = fileType;
		}
	}
}
