using System;
using System.IO;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Package
{
	public class DatabasePackedFileException : Exception
	{
	}

	public class NotAPackageException : DatabasePackedFileException
	{
	}

	public class UnsupportedPackageVersionException : DatabasePackedFileException
	{
	}

	public class DatabaseIndex
	{
		public bool Compressed;
		public uint TypeId;
		public uint GroupId;
		public uint InstanceId;
		public uint Offset;
		public uint CompressedSize;
		public uint DecompressedSize;
		public short CompressedFlags;
		public ushort Flags;
		public uint Unknown;

		public void CheckCompressed()
		{
			this.CompressedSize &= ~0x80000000;
			if (this.CompressedFlags != 0 && this.CompressedFlags != -1)
			{
				throw new InvalidDataException("compressed flags");
			}
			this.Compressed = this.CompressedFlags == -1 ? true : false;
		}

		public override string ToString()
		{
			return base.ToString() + ": " + this.TypeId.ToString("X8") + ", " + this.GroupId.ToString("X8") + " @ " + this.InstanceId.ToString("X8");
		}
	}

	public class DatabasePackedFile
	{
		public Version Version;
		public int Unknown0C;
		public int Unknown10;
		public int Unknown20;
		public int Unknown28;
		public int Unknown44;
		public DatabaseIndex[] Indices;

		public void Read(Stream stream)
		{
			//bool bigger = false;
			int indexCount = 0;
			int indexSize = 0;
			uint indexOffset = 0;

			uint magic = stream.ReadU32();
			if (magic != 0x46504244 && magic != 0x46424244) // DBPF & DBBF
			{
				throw new NotAPackageException();
			}

			if (magic == 0x46424244) // DBBF
			{
				//bigger = true;
			}

			int majorVersion = stream.ReadS32();
			if (majorVersion != 2)
			{
				throw new UnsupportedPackageVersionException();
			}

			int minorVersion = stream.ReadS32();
			if (minorVersion != 0)
			{
				throw new UnsupportedPackageVersionException();
			}

			this.Version = new Version(majorVersion, minorVersion);

			this.Unknown0C = stream.ReadS32();
			this.Unknown10 = stream.ReadS32();

			if (stream.ReadS32() != 0)
			{
				throw new InvalidDataException("should be zero");
			}

			if (stream.ReadS32() != 0)
			{
				throw new InvalidDataException("should be zero");
			}

			if (stream.ReadS32() != 0)
			{
				throw new InvalidDataException("should be zero");
			}

			this.Unknown20 = stream.ReadS32();
			indexCount = stream.ReadS32();
			if (indexCount < 0)
			{
				throw new InvalidDataException("entry count is < 0");
			}

			this.Unknown28 = stream.ReadS32();
			indexSize = stream.ReadS32();

			stream.Seek(12, SeekOrigin.Current);

			if (stream.ReadS32() != 3)
			{
				throw new InvalidDataException("should be three");
			}

			indexOffset = stream.ReadU32();
			this.Unknown44 = stream.ReadS32();

			stream.Seek(24, SeekOrigin.Current);

			// Should be at the end of the header now

			// ...nothing here...

			// Read index
			stream.Seek(indexOffset, SeekOrigin.Begin);

			int indexHeaderValues = stream.ReadS32();
			if (indexHeaderValues < 4 || indexHeaderValues > 7)
			{
				throw new InvalidDataException("don't know how to handle this index data");
			}

			uint indexTypeId = 0xCAFEBABE;
			// type id
			if ((indexHeaderValues & (1 << 0)) == 1 << 0)
			{
				indexTypeId = stream.ReadU32();
			}

			uint indexGroupId = 0xCAFEBABE;
			// group id
			if ((indexHeaderValues & (1 << 1)) == 1 << 1)
			{
				indexGroupId = stream.ReadU32();
			}

			uint indexUnknown = 0xCAFEBABE;
			// unknown value
			if ((indexHeaderValues & (1 << 2)) == 1 << 2)
			{
				indexUnknown = stream.ReadU32();
			}

			this.Indices = new DatabaseIndex[indexCount];
			for (int i = 0; i < indexCount; i++)
			{
				this.Indices[i] = new DatabaseIndex();
				#region this.Index[i].TypeId
				if ((indexHeaderValues & (1 << 0)) == 1 << 0)
				{
					this.Indices[i].TypeId = indexTypeId;
				}
				else
				{
					this.Indices[i].TypeId = stream.ReadU32();
				}
				#endregion
				#region this.Index[i].GroupId
				if ((indexHeaderValues & (1 << 1)) == 1 << 1)
				{
					this.Indices[i].GroupId = indexGroupId;
				}
				else
				{
					this.Indices[i].GroupId = stream.ReadU32();
				}
				#endregion
				#region this.Index[i].Unknown
				if ((indexHeaderValues & (1 << 2)) == 1 << 2)
				{
					this.Indices[i].Unknown = indexUnknown;
				}
				else
				{
					this.Indices[i].Unknown = stream.ReadU32();
				}
				#endregion
				this.Indices[i].InstanceId = stream.ReadU32();
				this.Indices[i].Offset = stream.ReadU32();
				this.Indices[i].CompressedSize = stream.ReadU32();
				this.Indices[i].DecompressedSize = stream.ReadU32();
				this.Indices[i].CompressedFlags = stream.ReadS16();
				this.Indices[i].Flags = stream.ReadU16();
				this.Indices[i].CheckCompressed();
			}
		}
	}
}
