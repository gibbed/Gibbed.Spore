using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("colorRGBA", "colorRGBAs", 52)]
	public class ColorRGBAProperty : Property
	{
		public float R;
		public float G;
		public float B;
		public float A;

		public override void ReadProp(Stream input, bool array)
		{
			this.R = input.ReadF32();
			this.G = input.ReadF32();
			this.B = input.ReadF32();
			this.A = input.ReadF32();
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteF32(this.R);
			output.WriteF32(this.G);
			output.WriteF32(this.B);
			output.WriteF32(this.A);
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteStartElement("r");
			output.WriteValue(this.R);
			output.WriteEndElement();

			output.WriteStartElement("g");
			output.WriteValue(this.G);
			output.WriteEndElement();

			output.WriteStartElement("b");
			output.WriteValue(this.B);
			output.WriteEndElement();

			output.WriteStartElement("a");
			output.WriteValue(this.A);
			output.WriteEndElement();
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			System.Xml.XmlReader subtree = input.ReadSubtree();

			subtree.ReadToFollowing("r");
			this.R = subtree.ReadElementContentAsFloat("r", "");

			subtree.ReadToFollowing("g");
			this.G = subtree.ReadElementContentAsFloat("g", "");

			subtree.ReadToFollowing("b");
			this.B = subtree.ReadElementContentAsFloat("b", "");

			subtree.ReadToFollowing("a");
			this.A = subtree.ReadElementContentAsFloat("a", "");
		}
	}
}
