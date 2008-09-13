using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	class Vector2Property : ComplexProperty
	{
		public float X;
		public float Y;

		public override void Read(Stream input, bool array)
		{
			this.X = input.ReadF32();
			this.Y = input.ReadF32();

			if (array == false)
			{
				input.Seek(8, SeekOrigin.Current);
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
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
