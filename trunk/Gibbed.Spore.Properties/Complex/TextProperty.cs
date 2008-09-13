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

		public override void Read(Stream input, bool array)
		{
			if (array == true)
			{
				this.TableId = input.ReadU32BE();
				this.InstanceId = input.ReadU32BE();

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
