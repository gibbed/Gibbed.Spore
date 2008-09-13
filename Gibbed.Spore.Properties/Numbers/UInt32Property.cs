using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("uint32", "uint32s", 10)]
	class UInt32Property : Property
	{
		public uint Value;

		public override void Read(System.IO.Stream input, bool array)
		{
			this.Value = input.ReadU32();
		}

		public override void Write(System.IO.Stream input, bool array)
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
				this.Value = uint.Parse(value);
			}
		}
	}
}
