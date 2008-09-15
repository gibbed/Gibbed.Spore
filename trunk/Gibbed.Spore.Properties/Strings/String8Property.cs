using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("string8", "string8s", 18)]
	public class String8Property : Property
	{
		public string Value;

		public override void ReadProp(Stream input, bool array)
		{
			uint length = input.ReadU32BE();
			byte[] data = new byte[length];
			input.Read(data, 0, data.Length);
			this.Value = Encoding.ASCII.GetString(data);
		}

		public override void WriteProp(Stream output, bool array)
		{
			throw new NotImplementedException();
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(this.Value);
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			throw new NotImplementedException();
		}
	}
}
