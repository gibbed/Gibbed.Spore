using System.IO;
using System.Text;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("string16", "string16s", 19)]
	public class String16Property : Property
	{
		public string Value;

		public override void ReadProp(Stream input, bool array)
		{
			int length = input.ReadS32BE();
			byte[] data = new byte[length * 2];
			input.Read(data, 0, length * 2);
			this.Value = Encoding.Unicode.GetString(data);
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteS32BE(this.Value.Length);
			byte[] data = Encoding.Unicode.GetBytes(this.Value);
			output.Write(data, 0, data.Length);
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(this.Value);
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			this.Value = input.ReadString();
		}
	}
}
