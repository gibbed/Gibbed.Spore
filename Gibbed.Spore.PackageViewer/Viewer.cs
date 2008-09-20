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

namespace Gibbed.Spore.PackageViewer
{
	public partial class Viewer : Form
	{
		public Viewer()
		{
			InitializeComponent();
		}

		private Font MonospaceFont = new Font(FontFamily.GenericMonospace, 9.0f);

		private Dictionary<uint, string> HashedNames;

		private void OnLoad(object sender, EventArgs e)
		{
			string path;
			path = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Electronic Arts\\SPORE", "DataDir", "");
			if (path != null && path.Length > 0)
			{
				if (path.Length >= 2)
				{
					if (path[0] == '"' && path[path.Length - 1] == '"')
					{
						path = path.Substring(1, path.Length - 2);
					}
				}

				this.openDialog.InitialDirectory = path;
			}

			this.HashedNames = new Dictionary<uint, string>();

			string listPath = Path.Combine(Application.StartupPath, "hash_names.txt");
			if (File.Exists(listPath))
			{
				TextReader reader = new StreamReader(listPath);

				while (true)
				{
					string line = reader.ReadLine();
					if (line == null)
					{
						break;
					}

					uint hash = line.FNV();
					this.HashedNames[hash] = line;
				}

				reader.Close();
			}
		}

		// A stupid way to do it but it's for the Save All function.
		private DatabaseIndex[] DatabaseFiles;

		private void OnOpen(object sender, EventArgs e)
		{
			if (this.openDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			if (this.openDialog.InitialDirectory != null)
			{
				this.openDialog.InitialDirectory = null;
			}

			Stream input = this.openDialog.OpenFile();
			DatabasePackedFile db = new DatabasePackedFile();
			db.Read(input);

			this.DatabaseFiles = db.Indices;

			Dictionary<uint, TreeNode> typeNodes = new Dictionary<uint, TreeNode>();

			this.typeList.Nodes.Clear();
			this.typeList.BeginUpdate();

			TreeNode knownNode = new TreeNode("Known");
			TreeNode unknownNode = new TreeNode("Unknown");

			for (int i = 0; i < db.Indices.Length; i++)
			{
				DatabaseIndex index = db.Indices[i];

				TreeNode typeNode = null;
				if (typeNodes.ContainsKey(index.TypeId) == false)
				{
					typeNode = new TreeNode();
					#region typeNode.Text = extension or typeid
					string text = Types.GetExtensionFromId(index.TypeId);
					bool isUnknown = false;
					if (text == null)
					{
						text = "#" + index.TypeId.ToString("X8");
						isUnknown = true;
					}
					typeNode.Text = text;
					#endregion
					typeNode.Tag = new List<DatabaseIndex>();
					if (isUnknown == false)
					{
						knownNode.Nodes.Add(typeNode);
					}
					else
					{
						typeNode.NodeFont = this.MonospaceFont;
						unknownNode.Nodes.Add(typeNode);
					}
					typeNodes[index.TypeId] = typeNode;
				}
				else
				{
					typeNode = typeNodes[index.TypeId];
				}

				List<DatabaseIndex> files = typeNode.Tag as List<DatabaseIndex>;
				files.Add(index);
			}

			if (knownNode.Nodes.Count > 0)
			{
				this.typeList.Nodes.Add(knownNode);
			}

			if (unknownNode.Nodes.Count > 0)
			{
				this.typeList.Nodes.Add(unknownNode);
			}

			this.typeList.Sort();
			this.typeList.EndUpdate();

			if (knownNode.Nodes.Count > 0)
			{
				knownNode.Expand();
			}
			else if (unknownNode.Nodes.Count > 0)
			{
				unknownNode.Expand();
			}
		}

		private void OnSelectType(object sender, TreeViewEventArgs e)
		{
			if (e.Node == null || e.Node.Tag == null)
			{
				return;
			}

			List<DatabaseIndex> files = e.Node.Tag as List<DatabaseIndex>;

			this.fileList.BeginUpdate();
			this.fileList.Items.Clear();
			this.fileList.Sorting = SortOrder.None;

			foreach (DatabaseIndex file in files)
			{
				ListViewItem listViewItem = new ListViewItem("");

				if (this.HashedNames.ContainsKey(file.InstanceId))
				{
					listViewItem.Text = this.HashedNames[file.InstanceId];
				}
				else
				{
					listViewItem.Text = "#" + file.InstanceId.ToString("X8");
				}

				if (this.HashedNames.ContainsKey(file.GroupId))
				{
					listViewItem.SubItems.Add(this.HashedNames[file.GroupId]);
				}
				else
				{
					listViewItem.SubItems.Add("#" + file.GroupId.ToString("X8")).Font = this.MonospaceFont;
				}

				listViewItem.SubItems.Add(file.DecompressedSize.ToString());
				listViewItem.SubItems.Add(file.Unknown.ToString("X8")).Font = this.MonospaceFont;
				listViewItem.Tag = file;

				this.fileList.Items.Add(listViewItem);
			}

			this.fileList.Sorting = SortOrder.Ascending;
			this.fileList.Sort();
			this.fileList.EndUpdate();
		}

		private void OnSaveAll(object sender, EventArgs e)
		{
			if (this.saveAllFolderDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Stream input = this.openDialog.OpenFile();

			if (input == null)
			{
				return;
			}

			string basePath = this.saveAllFolderDialog.SelectedPath;

			SaveAllProgress progress = new SaveAllProgress();
			progress.ShowSaveProgress(this, input, this.DatabaseFiles, this.HashedNames, basePath);

			input.Close();
		}
	}
}
