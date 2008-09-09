using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

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
        public bool Compressed { get; set; }
        public uint TypeId { get; set; }
        public uint GroupId { get; set; }
        public uint InstanceId { get; set; }
        public uint Offset { get; set; }
        public uint CompressedSize { get; set; }
        public uint DecompressedSize { get; set; }
        public short CompressedFlags { get; set; }
        public ushort Flags { get; set; }
        public uint Unknown { get; set; }

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
        public Version Version { get; set; }
        public int Unknown0C;
        public int Unknown10 { get; set; }
        public int Unknown20 { get; set; }
        public int Unknown28 { get; set; }
        public int Unknown44 { get; set; }
        public DatabaseIndex[] Index { get; set; }

        public void Read(Stream stream)
        {
			bool bigger = false;
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
				bigger = true;
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

            this.Index = new DatabaseIndex[indexCount];
            for (int i = 0; i < indexCount; i++)
            {
                this.Index[i] = new DatabaseIndex();
                #region this.Index[i].TypeId
                if ((indexHeaderValues & (1 << 0)) == 1 << 0)
                {
                    this.Index[i].TypeId = indexTypeId;
                }
                else
                {
                    this.Index[i].TypeId = stream.ReadU32();
                }
                #endregion
                #region this.Index[i].GroupId
                if ((indexHeaderValues & (1 << 1)) == 1 << 1)
                {
                    this.Index[i].GroupId = indexGroupId;
                }
                else
                {
                    this.Index[i].GroupId = stream.ReadU32();
                }
                #endregion
                #region this.Index[i].Unknown
                if ((indexHeaderValues & (1 << 2)) == 1 << 2)
                {
                    this.Index[i].Unknown = indexUnknown;
                }
                else
                {
                    this.Index[i].Unknown = stream.ReadU32();
                }
                #endregion
                this.Index[i].InstanceId = stream.ReadU32();
                this.Index[i].Offset = stream.ReadU32();
                this.Index[i].CompressedSize = stream.ReadU32();
                this.Index[i].DecompressedSize = stream.ReadU32();
                this.Index[i].CompressedFlags = stream.ReadS16();
                this.Index[i].Flags = stream.ReadU16();
                this.Index[i].CheckCompressed();
            }
		}
    }

	public static class DatabasePackedFileStreamHelpers
	{
		public static byte ReadU8(this Stream stream)
		{
			return (byte)stream.ReadByte();
		}

		public static Int16 ReadS16(this Stream stream)
		{
			byte[] data = new byte[2];
			stream.Read(data, 0, 2);
			return BitConverter.ToInt16(data, 0);
		}

		public static UInt16 ReadU16(this Stream stream)
		{
			byte[] data = new byte[2];
			stream.Read(data, 0, 2);
			return BitConverter.ToUInt16(data, 0);
		}

		public static Int32 ReadS32(this Stream stream)
		{
			byte[] data = new byte[4];
			stream.Read(data, 0, 4);
			return BitConverter.ToInt32(data, 0);
		}

		public static UInt32 ReadU32(this Stream stream)
		{
			byte[] data = new byte[4];
			stream.Read(data, 0, 4);
			return BitConverter.ToUInt32(data, 0);
		}

		public static UInt64 ReadU64(this Stream stream)
		{
			byte[] data = new byte[8];
			stream.Read(data, 0, 8);
			return BitConverter.ToUInt64(data, 0);
		}

		public static void ReadCompressionHeader(this Stream stream)
		{
			byte[] header = new byte[2];
			stream.Read(header, 0, header.Length);

			bool stop = false;

			// hdr & 0x3EFF) == 0x10FB 
			if ((header[0] & 0x3E) != 0x10 || (header[1] != 0xFB))
			{
				// stream is not compressed 
				stop = true;
			}

			// read destination (uncompressed) length 
			bool isLong = ((header[0] & 0x80) != 0);
			bool hasMore = ((header[0] & 0x01) != 0);

			byte[] data = new byte[(isLong ? 4 : 3) * (hasMore ? 2 : 1)];
			stream.Read(data, 0, data.Length);

			UInt32 realLength = (uint)((((data[0] << 8) + data[1]) << 8) + data[2]);
			if (isLong)
			{
				realLength = (realLength << 8) + data[3];
			}
		}

		public static byte[] Decompress(this Stream stream, uint compressedSize, uint decompressedSize)
		{
			long baseOffset = stream.Position;
			byte[] outputData = new byte[decompressedSize];
			uint offset = 0;
			
			stream.ReadCompressionHeader();

			while (stream.Position < baseOffset + compressedSize)
			{
				bool stop = false;
				UInt32 plainSize = 0;
				UInt32 copySize = 0;
				UInt32 copyOffset = 0;

				byte prefix = stream.ReadU8();

				if (prefix >= 0xC0)
				{
					if (prefix >= 0xE0)
					{
						if (prefix >= 0xFC)
						{
							plainSize = (uint)(prefix & 3);
							stop = true;
						}
						else
						{
							plainSize = (uint)(((prefix & 0x1F) + 1) * 4);
						}
					}
					else
					{
						byte[] extra = new byte[3];
						stream.Read(extra, 0, extra.Length);
						plainSize = (uint)(prefix & 3);
						copySize = (uint)((((prefix & 0x0C) << 6) | extra[2]) + 5);
						copyOffset = (uint)((((((prefix & 0x10) << 4) | extra[0]) << 8) | extra[1]) + 1);
					}
				}
				else
				{
					if (prefix >= 0x80)
					{
						byte[] extra = new byte[2];
						stream.Read(extra, 0, extra.Length);
						plainSize = (uint)(extra[0] >> 6);
						copySize = (uint)((prefix & 0x3F) + 4);
						copyOffset = (uint)((((extra[0] & 0x3F) << 8) | extra[1]) + 1);
					}
					else
					{
						byte extra = stream.ReadU8();
						plainSize = (uint)(prefix & 3);
						copySize = (uint)(((prefix & 0x1C) >> 2) + 3);
						copyOffset = (uint)((((prefix & 0x60) << 3) | extra) + 1);
					}
				}

				if (plainSize > 0)
				{
					stream.Read(outputData, (int)offset, (int)plainSize);
					offset += plainSize;
				}

				if (copySize > 0)
				{
					for (uint i = 0; i < copySize; i++)
					{
						outputData[offset + i] = outputData[(offset - copyOffset) + i];
					}

					offset += copySize;
				}

				if (stop)
				{
					break;
				}
			}
			
			return outputData;
		}	
	}
}
