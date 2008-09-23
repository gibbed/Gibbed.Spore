using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.ModMaker
{
	public class ModificationFile
	{
		[XmlAttribute(AttributeName = "instanceid", DataType = "hexBinary")]
		#region public byte[] XmlInstanceId
		public byte[] XmlInstanceId
		{
			get
			{
				return BitConverter.GetBytes(this.InstanceId.Swap());
			}
			set
			{
				this.InstanceId = BitConverter.ToUInt32(value, 0).Swap();
			}
		}
		#endregion

		[XmlAttribute(AttributeName = "groupid", DataType = "hexBinary")]
		#region public byte[] XmlGroupId
		public byte[] XmlGroupId
		{
			get
			{
				return BitConverter.GetBytes(this.GroupId.Swap());
			}
			set
			{
				this.GroupId = BitConverter.ToUInt32(value, 0).Swap();
			}
		}
		#endregion

		[XmlAttribute(AttributeName = "typeid", DataType = "hexBinary")]
		#region public byte[] XmlTypeId
		public byte[] XmlTypeId
		{
			get
			{
				return BitConverter.GetBytes(this.TypeId.Swap());
			}
			set
			{
				this.TypeId = BitConverter.ToUInt32(value, 0).Swap();
			}
		}
		#endregion

		[XmlIgnore]
		public UInt32 InstanceId { get; set; }

		[XmlIgnore]
		public UInt32 GroupId { get; set; }

		[XmlIgnore]
		public UInt32 TypeId { get; set; }

		public override string ToString()
		{
			return String.Format("I:{1:X8} G:{2:X8} T:{3:X8}  {0}", this.FilePath, this.InstanceId, this.GroupId, this.TypeId);
		}

		[XmlText]
		public string FilePath { get; set; }

		public ModificationFile()
		{
			this.InstanceId = 0xFFFFFFFD;
			this.GroupId = 0xFFFFFFFE;
			this.TypeId = 0xFFFFFFFF;
		}

		public ModificationFile(string path)
		{
			this.InstanceId = 0xFFFFFFFD;
			this.GroupId = 0xFFFFFFFE;
			this.TypeId = 0xFFFFFFFF;
			this.FilePath = path;
		}

		public ModificationFile(UInt32 instance, UInt32 group, UInt32 type, string path)
		{
			this.InstanceId = instance;
			this.GroupId = group;
			this.TypeId = type;
			this.FilePath = path;
		}

		private static string RelativePathTo(string fromPath, string toPath)
		{
			if (fromPath == null)
			{
				throw new ArgumentNullException("fromDirectory");
			}
			
			if (toPath == null)
			{
				throw new ArgumentNullException("toPath");
			}
			
			if (Path.IsPathRooted(fromPath) == true && Path.IsPathRooted(toPath) == true)
			{
				if (string.Compare(Path.GetPathRoot(fromPath), Path.GetPathRoot(toPath), true) != 0)
				{
					return toPath;
				}
			}

			List<string> relativePath = new List<string>();
			string[] fromDirectories = fromPath.Split(Path.DirectorySeparatorChar);
			string[] toDirectories = toPath.Split(Path.DirectorySeparatorChar);

			int length = Math.Min(fromDirectories.Length, toDirectories.Length);
			int lastCommonRoot = -1;

			// find common root
			for (int x = 0; x < length; x++)
			{
				if (string.Compare(fromDirectories[x], toDirectories[x], true) != 0)
				{
					break;
				}

				lastCommonRoot = x;
			}

			if (lastCommonRoot == -1)
			{
				return toPath;
			}

			// add relative folders in from path
			for (int x = lastCommonRoot + 1; x < fromDirectories.Length; x++)
			{
				if (fromDirectories[x].Length > 0)
				{
					relativePath.Add("..");
				}
			}

			// add to folders to path
			for (int x = lastCommonRoot + 1; x < toDirectories.Length; x++)
			{
				relativePath.Add(toDirectories[x]);
			}

			// create relative path
			return string.Join(Path.DirectorySeparatorChar.ToString(), relativePath.ToArray());
		}

		public void FixPath(string modPath)
		{
			if (Path.IsPathRooted(this.FilePath) == false)
			{
				this.FilePath = Path.GetFullPath(this.FilePath);
			}

			this.FilePath = Path.Combine(RelativePathTo(modPath, Path.GetDirectoryName(this.FilePath)), Path.GetFileName(this.FilePath));
		}
	}

	[XmlRoot(ElementName = "mod")]
	public class Modification
	{
		[XmlElement(ElementName = "name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "author")]
		public string Author { get; set; }

		[XmlElement(ElementName = "email")]
		public string Email { get; set; }

		[XmlElement(ElementName = "website")]
		public string Website { get; set; }

		[XmlElement(ElementName = "description")]
		public string Description { get; set; }

		[XmlElement(ElementName = "version")]
		public string Version { get; set; }

		[XmlArray(ElementName = "files")]
		[XmlArrayItem(ElementName = "file")]
		public List<ModificationFile> Files { get; set; }

		[XmlIgnore]
		public string FilePath;

		public Modification()
		{
			this.Name = "Unnamed Mod";
			this.Author = "Modder";
			this.Email = "anonymous@spore.modder";
			this.Website = "http://www.spore.com/";
			this.Description = "A mod for Spore. It does... stuff.";
			this.Version = "1.0";
			this.Files = new List<ModificationFile>();
		}

		public void FixPaths()
		{
			if (this.FilePath == null)
			{
				return;
			}

			string modPath = Path.GetDirectoryName(this.FilePath);

			string oldPath = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(modPath);

			foreach (ModificationFile file in this.Files)
			{
				file.FixPath(modPath);
			}

			Directory.SetCurrentDirectory(oldPath);
		}
	}
}
