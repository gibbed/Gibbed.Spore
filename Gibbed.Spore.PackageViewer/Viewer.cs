using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Gibbed.Spore.Package;

namespace Gibbed.Spore.PackageViewer
{
	public partial class Viewer : Form
	{
		public Viewer()
		{
			InitializeComponent();
		}

		private Dictionary<uint, string> HashedNames;
		private Dictionary<uint, string> TypeExtensions;

		// FNV hash that EA loves to use :-)
		private static uint FNV(string input)
		{
			uint rez = 0x811C9DC5;

			for (int i = 0; i < input.Length; i++)
			{
				rez *= 0x1000193;
				rez ^= (char)(input[i]);
			}

			return rez;
		}

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

			string listPath = Path.Combine(Application.StartupPath, "filelist.txt");
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

					uint hash = FNV(line);
					this.HashedNames[hash] = line;
				}

				reader.Close();
			}

			// This should probably go in a file but for now it's hardcoded.
			this.TypeExtensions = new Dictionary<uint, string>();

			// Unhashed or unknown extension
			this.TypeExtensions[0x00B1B104] = "prop";
			this.TypeExtensions[0x00E6BCE5] = "gmdl";
			this.TypeExtensions[0x011989B7] = "plt"; // NEW
			this.TypeExtensions[0x01AD2416] = "creature_traits"; // NEW
			this.TypeExtensions[0x01AD2417] = "building_traits"; // NEW
			this.TypeExtensions[0x01AD2418] = "vehicle_traits"; // NEW
			this.TypeExtensions[0x01C135DA] = "gmsh"; // NEW
			this.TypeExtensions[0x01C3C4B3] = "trait_pill"; // NEW
			this.TypeExtensions[0x0248F226] = "css";
			this.TypeExtensions[0x024A0E52] = "trigger";
			this.TypeExtensions[0x02523258] = "formation";
			this.TypeExtensions[0x02D5C9AF] = "summary"; // NEW
			this.TypeExtensions[0x02D5C9B0] = "summary_pill"; // NEW
			this.TypeExtensions[0x02FAC0B6] = "txt";
			this.TypeExtensions[0x030BDEE3] = "pollen_metadata"; // NEW
			this.TypeExtensions[0x0376C3DA] = "hm"; // NEW
			this.TypeExtensions[0x0472329B] = "htra";
			this.TypeExtensions[0x2F4E681C] = "raster"; // NEW
			this.TypeExtensions[0x2F7D0002] = "jpeg";
			this.TypeExtensions[0x2F7D0004] = "png";
			this.TypeExtensions[0x2F7D0005] = "bmp";
			this.TypeExtensions[0x2F7D0006] = "tga";
			this.TypeExtensions[0x2F7D0007] = "gif";
			this.TypeExtensions[0x4AEB6BC6] = "tlsa";
			this.TypeExtensions[0x7C19AA7A] = "pctp";
			this.TypeExtensions[0xEFBDA3FF] = "layout";

			// Hashed version of the extension
			this.TypeExtensions[0x12952634] = "dat";
			this.TypeExtensions[0x1A99B06B] = "bem";
			this.TypeExtensions[0x1E99B626] = "bat";
			this.TypeExtensions[0x1F639D98] = "xls";
			this.TypeExtensions[0x2399BE55] = "bld"; // building
			this.TypeExtensions[0x24682294] = "vcl"; // vehicle
			this.TypeExtensions[0x250FE9A2] = "spui"; // SPore User Interface
			this.TypeExtensions[0x25DF0112] = "gait";
			this.TypeExtensions[0x2B6CAB5F] = "txt"; // localized text
			this.TypeExtensions[0x2B978C46] = "crt"; // creature
			this.TypeExtensions[0x37979F71] = "cfg";
			this.TypeExtensions[0x3C77532E] = "psd";
			this.TypeExtensions[0x3C7E0F63] = "mcl"; // muscle
			this.TypeExtensions[0x3D97A8E4] = "cll"; // cell
			this.TypeExtensions[0x3F9C28B5] = "ani";
			this.TypeExtensions[0x438F6347] = "flr"; // flier
			this.TypeExtensions[0x448AE7E2] = "hkx"; // havok physics (or effect?)
			this.TypeExtensions[0x476A98C7] = "ufo"; // spaceship
			this.TypeExtensions[0x497767B9] = "pfx"; // particle effect
			this.TypeExtensions[0x617715C4] = "py";
			this.TypeExtensions[0x617715D9] = "pd";
			this.TypeExtensions[0x9B8E862F] = "world";
			this.TypeExtensions[0xDFAD9F51] = "cell";
		}

		private string GetExtensionForType(uint typeId)
		{
			if (this.TypeExtensions.ContainsKey(typeId))
			{
				return this.TypeExtensions[typeId];
			}

			return null;
		}

		// A stupid way to do it but it's for the Save All function.
		private DatabaseIndex[] DatabaseFiles;

		private void OnOpen(object sender, EventArgs e)
		{
			if (this.openDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Stream input = this.openDialog.OpenFile();
			DatabasePackedFile db = new DatabasePackedFile();
			db.Read(input);

			this.DatabaseFiles = db.Indices;

			Dictionary<uint, TreeNode> typeNodes = new Dictionary<uint, TreeNode>();

			this.typeList.Nodes.Clear();
			this.typeList.BeginUpdate();

			TreeNode knownNode = this.typeList.Nodes.Add("Known");
			TreeNode unknownNode = this.typeList.Nodes.Add("Unknown");

			for (int i = 0; i < db.Indices.Length; i++)
			{
				DatabaseIndex index = db.Indices[i];

				TreeNode typeNode = null;
				if (typeNodes.ContainsKey(index.TypeId) == false)
				{
					typeNode = new TreeNode();
					#region typeNode.Text = extension or typeid
					string text = this.GetExtensionForType(index.TypeId);
					bool isUnknown = false;
					if (text == null)
					{
						text = index.TypeId.ToString("X8");
						isUnknown = true;
					}
					else
					{
						text = "." + text;
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

			foreach (uint typeId in typeNodes.Keys)
			{

			}

			this.typeList.Sort();
			this.typeList.EndUpdate();
			knownNode.Expand();
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
				ListViewItem listViewItem = new ListViewItem();

				if (this.HashedNames.ContainsKey(file.InstanceId))
				{
					listViewItem.Text = this.HashedNames[file.InstanceId];
				}
				else
				{
					listViewItem.Text = "<" + file.InstanceId.ToString("X8") + ">";
				}

				listViewItem.SubItems.Add(file.GroupId.ToString("X8"));
				listViewItem.SubItems.Add(file.DecompressedSize.ToString());
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

			for (int i = 0; i < this.DatabaseFiles.Length; i++)
			{
				DatabaseIndex index = this.DatabaseFiles[i];

				string fileName = null;
				string typeName = null;
				string groupName = null;

				if (this.HashedNames.ContainsKey(index.InstanceId))
				{
					fileName = this.HashedNames[index.InstanceId];
				}
				else
				{
					fileName = index.InstanceId.ToString("X8");
				}

				groupName = index.GroupId.ToString("X8");

				typeName = this.GetExtensionForType(index.TypeId);
				
				if (typeName == null)
				{
					typeName = index.TypeId.ToString("X8");
				}
				else
				{
					// this is just a silly way to get real extensions sorted on the top before the unknown types
					fileName += "." + typeName;
				}

				string path = Path.Combine(Path.Combine(basePath, typeName), groupName);
				Directory.CreateDirectory(path);

				path = Path.Combine(path, fileName);

				if (index.Compressed)
				{
					input.Seek(index.Offset, SeekOrigin.Begin);
					byte[] d = input.Decompress(index.CompressedSize, index.DecompressedSize);
					FileStream output = new FileStream(path, FileMode.Create);
					output.Write(d, 0, d.Length);
					output.Close();
				}
				else
				{
					input.Seek(index.Offset, SeekOrigin.Begin);
					byte[] d = new byte[index.DecompressedSize];
					input.Read(d, 0, d.Length);
					FileStream output = new FileStream(path, FileMode.Create);
					output.Write(d, 0, d.Length);
					output.Close();
				}
			}

			input.Close();
		}
	}
}
