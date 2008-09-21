using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinitionAttribute("vector4", "vector4s", 51)]
	class Vector4Property : Property
	{
		public float X;
		public float Y;
		public float Z;
		public float W;

		public override void ReadProp(Stream input, bool array)
		{
			this.X = input.ReadF32();
			this.Y = input.ReadF32();
			this.Z = input.ReadF32();
			this.W = input.ReadF32();
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteF32(this.X);
			output.WriteF32(this.Y);
			output.WriteF32(this.Z);
			output.WriteF32(this.W);
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteStartElement("x");
			output.WriteValue(this.X);
			output.WriteEndElement();

			output.WriteStartElement("y");
			output.WriteValue(this.Y);
			output.WriteEndElement();

			output.WriteStartElement("z");
			output.WriteValue(this.Z);
			output.WriteEndElement();

			output.WriteStartElement("w");
			output.WriteValue(this.W);
			output.WriteEndElement();
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			System.Xml.XmlReader subtree = input.ReadSubtree();

			subtree.ReadToFollowing("x");
			this.X = subtree.ReadElementContentAsFloat("x", "");

			subtree.ReadToFollowing("y");
			this.Y = subtree.ReadElementContentAsFloat("y", "");

			subtree.ReadToFollowing("z");
			this.Z = subtree.ReadElementContentAsFloat("z", "");

			subtree.ReadToFollowing("w");
			this.W = subtree.ReadElementContentAsFloat("w", "");
		}
	}
}
