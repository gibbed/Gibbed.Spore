using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using Gibbed.Spore.Helpers;
using Gibbed.Spore.Package;
using System.Threading;

namespace Gibbed.Spore.PackageViewer
{
	public partial class SaveAllProgress : Form
	{
		public SaveAllProgress()
		{
			InitializeComponent();
		}

		delegate void SetStatusDelegate(string status, int percent);
		private void SetStatus(string status, int percent)
		{
			if (this.progressBar.InvokeRequired || this.statusLabel.InvokeRequired)
			{
				SetStatusDelegate callback = new SetStatusDelegate(SetStatus);
				this.Invoke(callback, new object[] { status, percent });
				return;
			}

			this.statusLabel.Text = status;
			this.progressBar.Value = percent;
		}

		delegate void SaveDoneDelegate();
		private void SaveDone()
		{
			if (this.InvokeRequired)
			{
				SaveDoneDelegate callback = new SaveDoneDelegate(SaveDone);
				this.Invoke(callback);
				return;
			}

			this.Close();
		}

		public void SaveAll(object oinfo)
		{
			SaveAllInformation info = (SaveAllInformation)oinfo;

			XmlTextWriter writer = new XmlTextWriter(Path.Combine(info.BasePath, "files.xml"), Encoding.UTF8);
			writer.Formatting = Formatting.Indented;

			writer.WriteStartDocument();
			writer.WriteStartElement("files");

			for (int i = 0; i < info.Files.Length; i++)
			{
				DatabaseIndex index = info.Files[i];

				string fileName = null;
				string typeName = null;
				string groupName = null;

				if (info.FileNames.ContainsKey(index.InstanceId))
				{
					fileName = info.FileNames[index.InstanceId];
				}
				else
				{
					fileName = "#" + index.InstanceId.ToString("X8");
				}

				if (info.GroupNames.ContainsKey(index.GroupId))
				{
					groupName = info.GroupNames[index.GroupId];
				}
				else
				{
					groupName = "#" + index.GroupId.ToString("X8");
				}

				typeName = Types.GetExtensionFromId(index.TypeId);

				if (typeName == null)
				{
					typeName = "#" + index.TypeId.ToString("X8");
				}
				else
				{
					fileName += "." + typeName;
				}

				string fragmentPath = Path.Combine(typeName, groupName);
				Directory.CreateDirectory(Path.Combine(info.BasePath, fragmentPath));

				string path = Path.Combine(fragmentPath, fileName);

				this.SetStatus(path, i);

				writer.WriteStartElement("file");
				writer.WriteAttributeString("groupid", "0x" + index.GroupId.ToString("X8"));
				writer.WriteAttributeString("instanceid", "0x" + index.InstanceId.ToString("X8"));
				writer.WriteAttributeString("typeid", "0x" + index.TypeId.ToString("X8"));
				writer.WriteValue(path);
				writer.WriteEndElement();

				path = Path.Combine(info.BasePath, path);

				if (index.Compressed)
				{
					info.Archive.Seek(index.Offset, SeekOrigin.Begin);
					byte[] d = info.Archive.RefPackDecompress(index.CompressedSize, index.DecompressedSize);
					FileStream output = new FileStream(path, FileMode.Create);
					output.Write(d, 0, d.Length);
					output.Close();
				}
				else
				{
					info.Archive.Seek(index.Offset, SeekOrigin.Begin);
					byte[] d = new byte[index.DecompressedSize];
					info.Archive.Read(d, 0, d.Length);
					FileStream output = new FileStream(path, FileMode.Create);
					output.Write(d, 0, d.Length);
					output.Close();
				}
			}

			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();
			this.SaveDone();
		}

		private struct SaveAllInformation
		{
			public string BasePath;
			public Stream Archive;
			public DatabaseIndex[] Files;
			public Dictionary<uint, string> FileNames;
			public Dictionary<uint, string> GroupNames;
		}

		private Thread SaveThread;
		public void ShowSaveProgress(IWin32Window owner, Stream archive, DatabaseIndex[] files, Dictionary<uint, string> fileNames, Dictionary<uint, string> groupNames, string basePath)
		{
			SaveAllInformation info;
			info.BasePath = basePath;
			info.Archive = archive;
			info.Files = files;
			info.FileNames = fileNames;
			info.GroupNames = groupNames;

			this.progressBar.Value = 0;
			this.progressBar.Maximum = files.Length;

			this.SaveThread = new Thread(new ParameterizedThreadStart(SaveAll));
			this.SaveThread.Start(info);
			this.ShowDialog(owner);
		}

		private void OnCancel(object sender, EventArgs e)
		{
			if (this.SaveThread != null)
			{
				this.SaveThread.Abort();
			}

			this.Close();
		}
	}
}
