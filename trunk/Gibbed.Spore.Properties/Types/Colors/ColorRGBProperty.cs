using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("colorRGB", "colorRGBs", 50)]
	public class ColorRGBProperty : Property
	{
		public float R;
		public float G;
		public float B;

		public override void ReadProp(Stream input, bool array)
		{
			this.R = input.ReadF32();
			this.G = input.ReadF32();
			this.B = input.ReadF32();

			if (array == false)
			{
				input.Seek(4, SeekOrigin.Current);
			}
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteF32(this.R);
			output.WriteF32(this.G);
			output.WriteF32(this.B);

			if (array == false)
			{
				output.WriteU32(0); // junk
			}
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
		}
	}
}
