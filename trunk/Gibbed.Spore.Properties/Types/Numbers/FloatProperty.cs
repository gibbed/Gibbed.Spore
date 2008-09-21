using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("float", "floats", 13)]
	public class FloatProperty : Property
	{
		public float Value;

		public override void ReadProp(Stream input, bool array)
		{
			this.Value = input.ReadF32BE();
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteF32BE(this.Value);
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(this.Value);
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			this.Value = float.Parse(input.ReadString());
		}
	}
}
