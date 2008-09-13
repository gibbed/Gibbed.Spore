using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	class ColorRGBAProperty : ComplexProperty
	{
		public float R;
		public float G;
		public float B;
		public float A;

		public override void Read(Stream input, bool array)
		{
			this.R = input.ReadF32();
			this.G = input.ReadF32();
			this.B = input.ReadF32();
			this.A = input.ReadF32();
		}

		public override void Write(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override string Literal
		{
			get
			{
				return String.Format("({0}, {1}, {2}, {3})", this.R, this.G, this.B, this.A);
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
