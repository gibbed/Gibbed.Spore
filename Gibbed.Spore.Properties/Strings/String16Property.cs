using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("string16", "string16s", 19)]
	class String16Property : Property
	{
		public string Value;

		public override void Read(Stream input, bool array)
		{
			int length = input.ReadS32BE();
			byte[] data = new byte[length * 2];
			input.Read(data, 0, length * 2);
			this.Value = Encoding.Unicode.GetString(data);
		}

		public override void Write(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override string Literal
		{
			get
			{
				return '"' + this.Value + '"';
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
