using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinitionAttribute("transform", "transforms", 56)]
	public class TransformProperty : ComplexProperty
	{
		public uint Unknown01;
		public float Unknown02;
		public float Unknown03;
		public float Unknown04;
		public float Unknown05;
		public float Unknown06;
		public float Unknown07;
		public float Unknown08;
		public float Unknown09;
		public float Unknown10;
		public float Unknown11;
		public float Unknown12;
		public float Unknown13;

		public override void ReadProp(Stream input, bool array)
		{
			this.Unknown01 = input.ReadU32BE();
			this.Unknown02 = input.ReadF32BE();
			this.Unknown03 = input.ReadF32BE();
			this.Unknown04 = input.ReadF32BE();
			this.Unknown05 = input.ReadF32BE();
			this.Unknown06 = input.ReadF32BE();
			this.Unknown07 = input.ReadF32BE();
			this.Unknown08 = input.ReadF32BE();
			this.Unknown09 = input.ReadF32BE();
			this.Unknown10 = input.ReadF32BE();
			this.Unknown11 = input.ReadF32BE();
			this.Unknown12 = input.ReadF32BE();
			this.Unknown13 = input.ReadF32BE();
		}

		public override void WriteProp(Stream output, bool array)
		{
			throw new NotImplementedException();
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12}",
				this.Unknown01,
				this.Unknown02,
				this.Unknown03,
				this.Unknown04,
				this.Unknown05,
				this.Unknown06,
				this.Unknown07,
				this.Unknown08,
				this.Unknown09,
				this.Unknown10,
				this.Unknown11,
				this.Unknown12,
				this.Unknown13));
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			throw new NotImplementedException();
		}
	}
}
