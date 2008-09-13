using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("bool", "bools", 1)]
	public class BoolProperty : Property
	{
		public bool Value;

		public override void Read(Stream input, bool array)
		{
			this.Value = input.ReadBoolean();
		}

		public override void Write(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override string Literal
		{
			get
			{
				return this.Value ? "true" : "false";
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
