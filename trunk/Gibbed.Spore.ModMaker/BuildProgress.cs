using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Gibbed.Spore.Helpers;
using Gibbed.Spore.Package;

namespace Gibbed.Spore.ModMaker
{
	public partial class BuildProgress : Form
	{
		public BuildProgress()
		{
			InitializeComponent();
		}

		delegate void SetProgressDelegate(int percent);
		private void SetProgress(int percent)
		{
			if (this.progressBar.InvokeRequired)
			{
				SetProgressDelegate callback = new SetProgressDelegate(SetProgress);
				this.Invoke(callback, new object[] { percent });
				return;
			}

			this.progressBar.Value = percent;
		}

		delegate void AddLogDelegate(string text);
		private void AddLog(string text)
		{
			if (this.logText.InvokeRequired)
			{
				AddLogDelegate callback = new AddLogDelegate(AddLog);
				this.Invoke(callback, new object[] { text });
				return;
			}

			if (this.logText.Text.Length > 0)
			{
				this.logText.AppendText(Environment.NewLine);
			}
			
			this.logText.AppendText(text);
		}

		delegate void BuildDoneDelegate();
		private void BuildDone()
		{
			if (this.InvokeRequired)
			{
				BuildDoneDelegate callback = new BuildDoneDelegate(BuildDone);
				this.Invoke(callback);
				return;
			}

			this.closeButton.Enabled = true;
		}

		private static void BufferedWrite(Stream input, Stream output, long length)
		{
			// Write out file contents
			int left = (int)length;
			byte[] data = new byte[4096];
			while (left > 0)
			{
				int chunk = Math.Min(data.Length, left);
				input.Read(data, 0, chunk);
				output.Write(data, 0, chunk);
				left -= chunk;
			}
		}

		public void BuildPackage(object oinfo)
		{
			BuildInformation info = (BuildInformation)oinfo;
			DatabasePackedFile dbpf = new DatabasePackedFile();

			string baseFilePath = Path.GetDirectoryName(info.Mod.FilePath);

			info.Output.Seek(0, SeekOrigin.Begin);

			dbpf.Version = new Version(2, 0);
			dbpf.WriteHeader(info.Output, 0, 0);

			int current = 1;
			foreach (ModificationFile file in info.Mod.Files)
			{
				string inputPath;
				if (Path.IsPathRooted(file.FilePath) == false)
				{
					if (info.Mod.FilePath != null)
					{
						// relative path, it should be relative to the mod file
						inputPath = Path.Combine(baseFilePath, file.FilePath);
					}
					else
					{
						inputPath = Path.GetFullPath(file.FilePath);
					}
				}
				else
				{
					inputPath = file.FilePath;
				}

				Stream input = null;
				try
				{
					input = File.OpenRead(inputPath);
				}
				catch (Exception e)
				{
					this.AddLog(string.Format("failed {0}: {1}", file.FilePath, e.Message));
					current++;
					continue;
				}

				DatabaseIndex index = new DatabaseIndex();
				index.GroupId = file.GroupId;
				index.InstanceId = file.InstanceId;
				index.TypeId = file.TypeId;
				index.Offset = info.Output.Position;
				index.DecompressedSize = (uint)input.Length;
				index.CompressedSize = (uint)input.Length | 0x80000000;
				index.Flags = 1;
				index.Compressed = false;

				// Write out file contents
				BufferedWrite(input, info.Output, input.Length);

				input.Close();

				dbpf.Indices.Add(index);
				this.SetProgress(current);
				this.AddLog("added " + file.FilePath);
				current++;
			}

			MemoryStream modinfo = new MemoryStream();
			XmlTextWriter xml = new XmlTextWriter(modinfo, Encoding.Unicode);
			xml.WriteStartDocument();
			xml.WriteStartElement("mod");
			xml.WriteElementString("name", info.Mod.Name);
			xml.WriteElementString("author", info.Mod.Author);
			xml.WriteElementString("email", info.Mod.Email);
			xml.WriteElementString("website", info.Mod.Website);
			xml.WriteElementString("description", info.Mod.Description);
			xml.WriteElementString("version", info.Mod.Version);
			xml.WriteElementString("createdwith", "Gibbed.Spore.ModMaker");
			xml.WriteEndElement();
			xml.WriteEndDocument();
			xml.Flush();
			
			DatabaseIndex modInfoIndex = new DatabaseIndex();
			modInfoIndex.GroupId = 0;
			modInfoIndex.InstanceId = 0;
			modInfoIndex.TypeId = "sporemod".FNV();
			modInfoIndex.Offset = info.Output.Position;
			modInfoIndex.DecompressedSize = (uint)modinfo.Length;
			modInfoIndex.CompressedSize = (uint)modinfo.Length | 0x80000000;
			modInfoIndex.Flags = 1;
			modInfoIndex.Compressed = false;

			// Write out file contents
			modinfo.Seek(0, SeekOrigin.Begin);
			BufferedWrite(modinfo, info.Output, modinfo.Length);

			dbpf.Indices.Add(modInfoIndex);

			this.AddLog("added mod info");

			int indexOffset = (int)info.Output.Position;
			dbpf.WriteIndex(info.Output);
			int indexSize = (int)info.Output.Position - indexOffset;

			info.Output.Seek(0, SeekOrigin.Begin);
			dbpf.WriteHeader(info.Output, indexOffset, indexSize);

			info.Output.Flush();

			this.BuildDone();
		}

		private struct BuildInformation
		{
			public Stream Output;
			public Modification Mod;
		}

		private Thread BuildThread;
		public void ShowBuildProgress(IWin32Window owner, Stream output, Modification mod)
		{
			BuildInformation info;
			info.Output = output;
			info.Mod = mod;

			this.progressBar.Value = 0;
			this.progressBar.Maximum = mod.Files.Count;

			this.BuildThread = new Thread(new ParameterizedThreadStart(BuildPackage));
			this.BuildThread.Start(info);
			this.ShowDialog(owner);
		}
	}
}
