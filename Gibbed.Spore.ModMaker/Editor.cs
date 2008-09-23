using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Gibbed.Spore.ModMaker
{
	public partial class Editor : Form
	{
		#region private Modification Mod
		private Modification Mod
		{
			get
			{
				return (Modification)(this.bindingSource.DataSource);
			}

			set
			{
				this.bindingSource.DataSource = value;
			}
		}
		#endregion

		public Editor()
		{
			this.InitializeComponent();
		}

		private Properties.Settings Settings;

		private void OnLoad(object sender, EventArgs e)
		{
			this.Settings = new Properties.Settings();
			if (this.Settings.RecentFiles == null)
			{
				this.Settings.RecentFiles = new StringCollection();
			}

			this.Mod = new Modification();
		}

		private void OnExit(object sender, EventArgs e)
		{
			this.Close();
		}

		private void OnFormatId(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(string))
			{
				return;
			}

			e.Value = String.Format("{0:X8}", e.Value);
		}

		private void OnParseId(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(UInt32))
			{
				return;
			}

			e.Value = UInt32.Parse((string)(e.Value), System.Globalization.NumberStyles.AllowHexSpecifier);
		}

		private void OnModNew(object sender, EventArgs e)
		{
			this.Mod = new Modification();
		}

		private void RecentFilesCheck(string path)
		{
			if (this.Settings.RecentFiles.Contains(path) == true)
			{
				this.Settings.RecentFiles.Remove(path);
			}

			while (this.Settings.RecentFiles.Count > 9)
			{
				this.Settings.RecentFiles.RemoveAt(9);
			}

			this.Settings.RecentFiles.Insert(0, path);
		}

		private void ModOpen(string path)
		{
			Stream input = null;
			try
			{
				input = File.OpenRead(path);
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Failed to open " + Path.GetFileName(path) + "!" +
					Environment.NewLine + Environment.NewLine +
					ex.Message,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Modification), new Type[] { typeof(ModificationFile) });
				this.Mod = (Modification)(serializer.Deserialize(input));
				this.Mod.FilePath = path;
			}
			catch (Exception ex)
			{
				input.Close();
				MessageBox.Show(
					"Failed to read " + Path.GetFileName(path) + "!" +
					Environment.NewLine + Environment.NewLine +
					ex.Message,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			input.Close();
			this.RecentFilesCheck(path);
			this.saveModDialog.FileName = path;
		}

		private void OnModOpen(object sender, EventArgs e)
		{
			if (this.openModDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			this.ModOpen(this.openModDialog.FileName);
		}

		private void OnModOpenRecent(object sender, ToolStripItemClickedEventArgs e)
		{
			this.ModOpen(e.ClickedItem.Text);
		}

		private void OnModSave(object sender, EventArgs e)
		{
			if (this.saveModDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Stream output = null;
			
			try
			{
				output = this.saveModDialog.OpenFile();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Failed to open " + Path.GetFileName(this.saveModDialog.FileName) + "!" +
					Environment.NewLine + Environment.NewLine +
					ex.Message,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.Mod.FilePath = this.saveModDialog.FileName;
			this.Mod.FixPaths();
			
			XmlSerializer serializer = new XmlSerializer(typeof(Modification), new Type[] { typeof(ModificationFile) });
			serializer.Serialize(output, this.Mod);
			output.Close();

			this.bindingSource.ResetBindings(false);

			this.RecentFilesCheck(this.saveModDialog.FileName);
		}

		private void OnFileAdd(object sender, EventArgs e)
		{
			if (this.addFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			this.filesBindingSource.Add(new ModificationFile(this.addFileDialog.FileName));
		}

		private void OnFileAddDirectory(object sender, EventArgs e)
		{
			if (this.addDirectoryDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			string[] paths = Directory.GetFiles(this.addDirectoryDialog.SelectedPath, "*", SearchOption.AllDirectories);
			Array.Sort(paths);
			foreach (string path in paths)
			{
				this.filesBindingSource.Add(new ModificationFile(path));
			}
		}

		private void OnFileRemove(object sender, EventArgs e)
		{
			if (this.filesBindingSource.Current == null)
			{
				return;
			}

			this.filesBindingSource.RemoveCurrent();
		}

		private void OnBuildPackage(object sender, EventArgs e)
		{
			if (this.savePackageDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Stream output = this.savePackageDialog.OpenFile();
			BuildProgress progress = new BuildProgress();
			progress.ShowBuildProgress(this, output, this.Mod);
			output.Close();
		}

		private void OnRecentMods(object sender, EventArgs e)
		{
			this.openModButton.DropDown.Items.Clear();

			foreach (string path in this.Settings.RecentFiles)
			{
				this.openModButton.DropDown.Items.Add(path);
			}
		}

		private void OnClose(object sender, FormClosedEventArgs e)
		{
			this.Settings.Save();
		}
	}
}
