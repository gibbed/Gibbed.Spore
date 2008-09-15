using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinitionAttribute("vector4", "vector4s", 51)]
	class Vector4Property : Property/* : ComplexProperty*/
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
			output.WriteValue(string.Format("{0}, {1}, {2}, {3}", this.X, this.Y, this.Z, this.W));
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			string line = input.ReadString();
			string[] numbers = line.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries);

			this.X = float.Parse(numbers[0]);
			this.Y = float.Parse(numbers[1]);
			this.Z = float.Parse(numbers[2]);
			this.W = float.Parse(numbers[3]);
		}
	}
}
