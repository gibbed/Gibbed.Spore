using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("bool", "bools", 1)]
	public class BoolProperty : Property
	{
		public bool Value;

		public override void ReadProp(Stream input, bool array)
		{
			this.Value = input.ReadBoolean();
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteBoolean(this.Value);
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(this.Value);
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			this.Value = bool.Parse(input.ReadString());
		}
	}
}
