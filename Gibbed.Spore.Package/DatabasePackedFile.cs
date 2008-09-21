using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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
		public Version Version = new Version();
		public List<DatabaseIndex> Indices = new List<DatabaseIndex>();

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

			this.Indices = new List<DatabaseIndex>();

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
					DatabaseIndex index = new DatabaseIndex();
					#region index.TypeId
					if ((indexHeaderValues & (1 << 0)) == 1 << 0)
					{
						index.TypeId = indexTypeId;
					}
					else
					{
						index.TypeId = stream.ReadU32();
					}
					#endregion
					#region index.GroupId
					if ((indexHeaderValues & (1 << 1)) == 1 << 1)
					{
						index.GroupId = indexGroupId;
					}
					else
					{
						index.GroupId = stream.ReadU32();
					}
					#endregion
					#region index.Unknown
					if ((indexHeaderValues & (1 << 2)) == 1 << 2)
					{
						index.Unknown = indexUnknown;
					}
					else
					{
						index.Unknown = stream.ReadU32();
					}
					#endregion
					index.InstanceId = stream.ReadU32();

					if (big == true)
					{
						index.Offset = stream.ReadS64();
					}
					else
					{
						index.Offset = stream.ReadS32();
					}

					index.CompressedSize = stream.ReadU32();
					index.DecompressedSize = stream.ReadU32();
					index.CompressedFlags = stream.ReadS16();
					index.Flags = stream.ReadU16();
					index.CheckCompressed();

					this.Indices.Add(index);
				}
			}
		}
	
		private static byte[] StructureToBytes(object structure)
		{
			int size = Marshal.SizeOf(structure.GetType());
			byte[] data = new byte[size];
			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			Marshal.StructureToPtr(structure, handle.AddrOfPinnedObject(), false);
			handle.Free();
			return data;
		}

		public void WriteHeader(Stream output, int indexOffset, int indexSize)
		{
			bool big = false;

			output.WriteASCII("DBPF");
			DatabasePackedFileHeader header = new DatabasePackedFileHeader();
			header.MajorVersion = this.Version.Major;
			header.MinorVersion = this.Version.Minor;
			header.Always3 = 3;
			header.IndexCount = this.Indices.Count;
			header.IndexOffset = indexOffset;
			header.IndexSize = indexSize;

			byte[] data = StructureToBytes(header);
			output.Write(data, 0, data.Length);
		}

		public void WriteIndex(Stream output)
		{
			output.WriteU32(4); // index header values
			output.WriteU32(0); // unknown

			foreach (DatabaseIndex index in this.Indices)
			{
				output.WriteU32(index.TypeId);
				output.WriteU32(index.GroupId);
				output.WriteU32(index.InstanceId);
				output.WriteS32((int)index.Offset);
				output.WriteU32(index.CompressedSize);
				output.WriteU32(index.DecompressedSize);
				output.WriteS16(index.CompressedFlags);
				output.WriteU16(index.Flags);
			}
		}
	}
}
