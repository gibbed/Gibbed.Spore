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
			this.Value = Encoding.UTF8.GetString(data);
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteS32BE(this.Value.Length);
			byte[] data = Encoding.UTF8.GetBytes(this.Value);
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
