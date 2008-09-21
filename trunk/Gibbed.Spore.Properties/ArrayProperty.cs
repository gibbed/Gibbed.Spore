using System;
using System.Collections.Generic;
using System.IO;

namespace Gibbed.Spore.Properties
{
	public class ArrayProperty : Property
	{
		public Type PropertyType;
		public List<Property> Values = new List<Property>();

		public override void ReadProp(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override void WriteProp(Stream output, bool array)
		{
			throw new NotImplementedException();
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			foreach (Property property in this.Values)
			{
				output.WriteStartElement(property.GetType().GetSingularName());
				property.WriteXML(output);
				output.WriteEndElement();
			}
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			throw new NotImplementedException();
		}
	}
}
