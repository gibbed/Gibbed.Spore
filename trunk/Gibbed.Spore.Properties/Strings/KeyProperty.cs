using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("key", "keys", 32)]
	class KeyProperty : Property
	{
		public uint TypeId;
		public uint GroupId;
		public uint InstanceId;

		public override void Read(Stream input, bool array)
		{
			this.InstanceId = input.ReadU32();
			this.TypeId = input.ReadU32();
			this.GroupId = input.ReadU32();

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
			set
			{
				throw new NotImplementedException();
			}

			get
			{
				string rez = "";

				if (this.GroupId != 0)
				{
					rez = "0x" + this.GroupId.ToString("X8") + "!";
				}

				rez += "0x" + this.InstanceId.ToString("X8");

				if (this.TypeId != 0)
				{
					rez += ".0x" + this.TypeId.ToString("X8");
				}

				return rez;
			}
		}
	}
}
