using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("int32", "int32s", 9)]
	class Int32Property : Property
	{
		public Int32 Value;

		public override void Read(Stream input, bool array)
		{
			this.Value = input.ReadS32();
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
				throw new NotImplementedException();
			}
		}
	}
}
