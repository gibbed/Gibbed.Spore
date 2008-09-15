using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinitionAttribute("vector3", "vector3s", 49)]
	public class Vector3Property : ComplexProperty
	{
		public float X;
		public float Y;
		public float Z;

		public override void ReadProp(Stream input, bool array)
		{
			this.X = input.ReadF32();
			this.Y = input.ReadF32();
			this.Z = input.ReadF32();

			if (array == false)
			{
				input.Seek(4, SeekOrigin.Current);
			}
		}

		public override void WriteProp(Stream output, bool array)
		{
			throw new NotImplementedException();
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteValue(string.Format("{0}, {1}, {2}", this.X, this.Y, this.Z));
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			throw new NotImplementedException();
		}
	}
}
