using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("colorRGB", "colorRGBs", 52)]
	public class ColorRGBAProperty : ComplexProperty
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
			throw new NotImplementedException();
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(string.Format("{0}, {1}, {2}, {3}", this.R, this.G, this.B, this.A));
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			throw new NotImplementedException();
		}
	}
}
