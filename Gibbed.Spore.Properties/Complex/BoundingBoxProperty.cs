using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinitionAttribute("bbox", "bboxes", 57)]
	class BoundingBoxProperty : ComplexProperty
	{
		public float MinX;
		public float MinY;
		public float MinZ;
		public float MaxX;
		public float MaxY;
		public float MaxZ;

		public override void ReadProp(Stream input, bool array)
		{
			/*
			this.MinX = input.ReadF32BE();
			this.MinY = input.ReadF32BE();
			this.MinZ = input.ReadF32BE();
			this.MaxX = input.ReadF32BE();
			this.MaxY = input.ReadF32BE();
			this.MaxZ = input.ReadF32BE();
			*/

			this.MinX = input.ReadF32();
			this.MinY = input.ReadF32();
			this.MinZ = input.ReadF32();
			this.MaxX = input.ReadF32();
			this.MaxY = input.ReadF32();
			this.MaxZ = input.ReadF32();
		}

		public override void WriteProp(Stream output, bool array)
		{
			throw new NotImplementedException();
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteStartElement("min");
			output.WriteValue(string.Format("{0}, {1}, {2}", this.MinX, this.MinY, this.MinZ));
			output.WriteEndElement();

			output.WriteStartElement("max");
			output.WriteValue(string.Format("{0}, {1}, {2}", this.MaxX, this.MaxY, this.MaxZ));
			output.WriteEndElement();
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			throw new NotImplementedException();
		}
	}
}
