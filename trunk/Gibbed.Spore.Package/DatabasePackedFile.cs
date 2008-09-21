using System;
using System.IO;
using Gibbed.Spore.Helpers;
using System.Runtime.InteropServices;

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
		public Int64 Offset;
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

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DatabasePackedFileHeader
	{
		//public uint Magic;		// 00
		public int MajorVersion;	// 04
		public int MinorVersion;	// 08
		public uint Unknown0C;		// 0C
		public uint Unknown10;		// 10
		public uint Unknown14;		// 14 - always 0?
		public uint Unknown18;		// 18 - always 0?
		public uint Unknown1C;		// 1C - always 0?
		public uint Unknown20;		// 20
		public int IndexCount;		// 24 - Number of index entries in the package.
		public uint Unknown28;		// 28
		public int IndexSize;		// 2C - The total size in bytes of index entries.
		public uint Unknown30;		// 30
		public uint Unknown34;		// 34
		public uint Unknown38;		// 38
		public uint Always3;		// 3C - Always 3?
		public int IndexOffset;		// 40 - Absolute offset in package to the index header.
		public uint Unknown44;		// 44
		public uint Unknown48;		// 48
		public uint Unknown4C;		// 4C
		public uint Unknown50;		// 50
		public uint Unknown54;		// 54
		public uint Unknown58;		// 58
		public uint Unknown5C;		// 5C
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DatabaseBigPackageFileHeader
	{
		//public uint Magic;		// 00
		public int MajorVersion;	// 04
		public int MinorVersion;	// 08
		public uint Unknown0C;		// 0C
		public uint Unknown10;		// 10
		public uint Unknown14;		// 14 - always 0?
		public uint Unknown18;		// 18 - always 0?
		public uint Unknown1C;		// 1C - always 0?
		public uint Unknown20;		// 20
		public int IndexCount;		// 24 - Number of index entries in the package.
		public Int64 IndexSize;		// 28 - The total size in bytes of index entries.
		public uint Unknown30;		// 30
		public uint Always3;		// 34 - Always 3?
		public Int64 IndexOffset;	// 38 - Absolute offset in package to the index header.
		public uint Unknown40;		// 40
		public uint Unknown44;		// 44
		public uint Unknown48;		// 48
		public uint Unknown4C;		// 4C
		public uint Unknown50;		// 50
		public uint Unknown54;		// 54
		public uint Unknown58;		// 58
		public uint Unknown5C;		// 5C
		public uint Unknown60;		// 60
		public uint Unknown64;		// 64
		public uint Unknown68;		// 68
		public uint Unknown6C;		// 6C
		public uint Unknown70;		// 70
		public uint Unknown74;		// 74
	}

	public class DatabasePackedFile
	{
		public Version Version;
		public DatabaseIndex[] Indices;

		public void Read(Stream stream)
		{
			bool big = false;
			Int64 indexCount;
			Int64 indexSize;
			Int64 indexOffset;

			uint magic = stream.ReadU32();
			if (magic != 0x46504244 && magic != 0x46424244) // DBPF & DBBF
			{
				throw new NotAPackageException();
			}

			if (magic == 0x46424244) // DBBF
			{
				big = true;

				DatabaseBigPackageFileHeader header;
				byte[] data = new byte[Marshal.SizeOf(typeof(DatabaseBigPackageFileHeader))];

				if (data.Length != (120 - 4))
				{
					throw new Exception("DatabaseBigPackageFileHeader is wrong size (" + data.Length.ToString() + ")");
				}

				stream.Read(data, 0, data.Length);
				header = (DatabaseBigPackageFileHeader)data.BytesToStructure(typeof(DatabaseBigPackageFileHeader));

				if (header.Always3 != 3)
				{
					throw new Exception("the value in the DBBF header that is always 3 was not 3");
				}

				// Nab useful stuff
				this.Version = new Version(header.MajorVersion, header.MinorVersion);
				indexCount = header.IndexCount;
				indexOffset = header.IndexOffset;
				indexSize = header.IndexSize;
			}
			else
			{
				big = false;

				DatabasePackedFileHeader header;
				byte[] data = new byte[Marshal.SizeOf(typeof(DatabasePackedFileHeader))];

				if (data.Length != (96 - 4))
				{
					throw new Exception("DatabasePackageFileHeader is wrong size (" + data.Length.ToString() + ")");
				}

				stream.Read(data, 0, data.Length);
				header = (DatabasePackedFileHeader)data.BytesToStructure(typeof(DatabasePackedFileHeader));

				if (header.Always3 != 3)
				{
					throw new Exception("the value in the DBPF header that is always 3 was not 3");
				}

				// Nab useful stuff
				this.Version = new Version(header.MajorVersion, header.MinorVersion);
				indexCount = header.IndexCount;
				indexOffset = header.IndexOffset;
				indexSize = header.IndexSize;
			}

			this.Indices = new DatabaseIndex[indexCount];

			if (indexCount > 0)
			{
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

					if (big == true)
					{
						this.Indices[i].Offset = stream.ReadS64();
					}
					else
					{
						this.Indices[i].Offset = stream.ReadS32();
					}

					this.Indices[i].CompressedSize = stream.ReadU32();
					this.Indices[i].DecompressedSize = stream.ReadU32();
					this.Indices[i].CompressedFlags = stream.ReadS16();
					this.Indices[i].Flags = stream.ReadU16();
					this.Indices[i].CheckCompressed();
				}
			}
		}
	}
}
