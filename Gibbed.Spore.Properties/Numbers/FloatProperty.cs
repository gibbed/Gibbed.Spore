using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("float", "floats", 13)]
	public class FloatProperty : Property
	{
		public float Value;

		public override void Read(Stream input, bool array)
		{
			this.Value = input.ReadF32BE();
		}

		public override void Write(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override string Literal
		{
			get
			{
				return this.Value.ToString();
			}
			set
			{
				this.Value = float.Parse(value);
			}
		}
	}
}
