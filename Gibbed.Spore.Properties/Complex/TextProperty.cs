using System;
using System.IO;
using System.Text;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	[PropertyDefinition("text", "texts", 34)]
	public class TextProperty : ComplexProperty
	{
		public uint TableId;
		public uint InstanceId;
		public string PlaceholderText;

		public override void ReadProp(Stream input, bool array)
		{
			if (array == true)
			{
				this.TableId = input.ReadU32();
				this.InstanceId = input.ReadU32();

				int size = (int)input.Length - 8;

				byte[] data = new byte[size];
				input.Read(data, 0, size);

				if (((size - 8) % 2) != 0)
				{
					throw new Exception("array size is not a multiple of two");
				}

				int end = 0;
				for (int i = 0; i < size - 8; i += 2)
				{
					if (data[i] == 0 && data[i + 1] == 0)
					{
						end = i;
						break;
					}
				}

				this.PlaceholderText = Encoding.Unicode.GetString(data, 0, end);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		public override void WriteProp(Stream output, bool array)
		{
			if (array == true)
			{
				output.WriteU32(this.TableId);
				output.WriteU32(this.InstanceId);
				byte[] data = Encoding.Unicode.GetBytes(this.PlaceholderText);
				output.Write(data, 0, data.Length);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		public override void WriteXML(System.Xml.XmlWriter output)
		{
			output.WriteAttributeString("tableid", "0x" + this.TableId.ToString("X8"));
			output.WriteAttributeString("instanceid", "0x" + this.InstanceId.ToString("X8"));
			output.WriteValue(this.PlaceholderText);
		}

		public override void ReadXML(System.Xml.XmlReader input)
		{
			this.TableId = input.GetAttribute("tableid").GetHexNumber();
			this.InstanceId = input.GetAttribute("instanceid").GetHexNumber();
			this.PlaceholderText = input.ReadString();
		}
	}
}
