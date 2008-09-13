using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("colorRGB", "colorRGBs", 50)]
	class ColorRGBProperty : ComplexProperty
	{
		public float R;
		public float G;
		public float B;

		public override void Read(Stream input, bool array)
		{
			this.R = input.ReadF32();
			this.G = input.ReadF32();
			this.B = input.ReadF32();

			if (array == false)
			{
				input.Seek(4, SeekOrigin.Current);
			}
		}

		public override void Write(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override string Literal
		{
			get
			{
				return String.Format("({0}, {1}, {2})", this.R, this.G, this.B);
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
