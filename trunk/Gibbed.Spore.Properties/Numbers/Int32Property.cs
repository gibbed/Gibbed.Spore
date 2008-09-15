using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("int32", "int32s", 9)]
	public class Int32Property : Property
	{
		public Int32 Value;

		public override void ReadProp(Stream input, bool array)
		{
			this.Value = input.ReadS32BE();
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteS32BE(this.Value);
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(this.Value);
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			this.Value = int.Parse(input.ReadString());
		}
	}
}
