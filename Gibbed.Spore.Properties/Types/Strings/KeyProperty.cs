using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("key", "keys", 32)]
	public class KeyProperty : Property
	{
		public uint TypeId;
		public uint GroupId;
		public uint InstanceId;

		public override void ReadProp(Stream input, bool array)
		{
			this.InstanceId = input.ReadU32();
			this.TypeId = input.ReadU32();
			this.GroupId = input.ReadU32();

			if (array == false)
			{
				input.Seek(4, SeekOrigin.Current);
			}
		}

		public override void WriteProp(Stream output, bool array)
		{
			output.WriteU32(this.InstanceId);
			output.WriteU32(this.TypeId);
			output.WriteU32(this.GroupId);

			if (array == false)
			{
				output.WriteU32(0); // junk
			}
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			if (this.GroupId != 0)
			{
				output.WriteAttributeString("groupid", "0x" + this.GroupId.ToString("X8"));
			}

			output.WriteAttributeString("instanceid", "0x" + this.InstanceId.ToString("X8"));

			if (this.TypeId != 0)
			{
				output.WriteAttributeString("typeid", "0x" + this.TypeId.ToString("X8"));
			}
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			string groupText = input.GetAttribute("groupid");
			string instanceText = input.GetAttribute("instanceid");
			string typeText = input.GetAttribute("typeid");

			if (groupText == null)
			{
				this.GroupId = 0;
			}
			else
			{
				this.GroupId = groupText.GetHexNumber();
			}

			if (instanceText == null)
			{
				throw new Exception("instanceid cannot be null for key");
			}
			else
			{
				this.InstanceId = instanceText.GetHexNumber();
			}

			if (typeText == null)
			{
				this.TypeId = 0;
			}
			else
			{
				this.TypeId = typeText.GetHexNumber();
			}
		}
	}
}
