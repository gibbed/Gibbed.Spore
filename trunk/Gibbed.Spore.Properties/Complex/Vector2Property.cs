using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("vector2", "vector2", 48)]
	public class Vector2Property : ComplexProperty
	{
		public float X;
		public float Y;

		public override void ReadProp(Stream input, bool array)
		{
			this.X = input.ReadF32();
			this.Y = input.ReadF32();

			if (array == false)
			{
				input.Seek(8, SeekOrigin.Current);
			}
		}

		public override void WriteProp(Stream output, bool array)
		{
			throw new NotImplementedException();
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteAttributeString("x", this.X.ToString());
			output.WriteAttributeString("y", this.Y.ToString());
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			throw new NotImplementedException();
		}
	}
}
