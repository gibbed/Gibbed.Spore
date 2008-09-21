using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("uint32", "uint32s", 10)]
	public class UInt32Property : Property
	{
		public uint Value;

		public override void ReadProp(System.IO.Stream input, bool array)
		{
			this.Value = input.ReadU32BE();
		}

		public override void WriteProp(System.IO.Stream output, bool array)
		{
			output.WriteU32BE(this.Value);
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(this.Value);
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			this.Value = uint.Parse(input.ReadString());
		}
	}
}
