using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	class Vector3Property : ComplexProperty
	{
		public float X;
		public float Y;
		public float Z;

		public override void Read(Stream input, bool array)
		{
			this.X = input.ReadF32BE();
			this.Y = input.ReadF32BE();
			this.Z = input.ReadF32BE();

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
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
