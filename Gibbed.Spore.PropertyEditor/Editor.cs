using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.XPath;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.PropertyEditor
{
	public partial class Editor : Form
	{
		public Editor()
		{
			InitializeComponent();
		}

		private Dictionary<uint, string> PropertyNames = new Dictionary<uint, string>();

		private void LoadPropertyNames(string path)
		{
			if (File.Exists(path) == false)
			{
				return;
			}

			XPathDocument document = new XPathDocument(path);
			XPathNavigator navigator = document.CreateNavigator();
			XPathNodeIterator nodes = navigator.Select("/names/name");

			while (nodes.MoveNext())
			{
				uint id;
				string key = nodes.Current.GetAttribute("id", "");
				string value = nodes.Current.Value;

				if (key.StartsWith("(hash(") && key.EndsWith("))"))
				{
					string tmp = key.Substring(6, key.Length - 8);
					id = tmp.FNV();
				}
				else
				{
					id = key.GetHexNumber();
				}

				this.PropertyNames[id] = value;
			}
		}

		private void OnLoad(object sender, EventArgs e)
		{
			this.LoadPropertyNames(Path.Combine(Application.StartupPath, "property_names.xml"));
		}

		private void OnOpen(object sender, EventArgs e)
		{
			if (this.openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Stream input = new FileStream(this.openFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite);

			Gibbed.Spore.Properties.PropertyFile file = new Gibbed.Spore.Properties.PropertyFile();
			file.Read(input);

			this.propertyView.Nodes.Clear();
			this.propertyView.BeginUpdate();

			foreach (uint hash in file.Values.Keys)
			{
				Gibbed.Spore.Properties.Property property = file.Values[hash];

				TreeNode node = new TreeNode();
				node.Tag = property;
				node.Text = (this.PropertyNames.ContainsKey(hash) ? this.PropertyNames[hash] : "0x" + hash.ToString("X8"));

				if (property is Gibbed.Spore.Properties.ArrayProperty)
				{
					Gibbed.Spore.Properties.ArrayProperty array = (Gibbed.Spore.Properties.ArrayProperty)(property);

					foreach (Gibbed.Spore.Properties.Property subproperty in array.Values)
					{
						TreeNode subnode = new TreeNode();
						subnode.Tag = subproperty;
						subnode.Text = "sub";
						node.Nodes.Add(subnode);
					}
				}

				this.propertyView.Nodes.Add(node);
			}

			this.propertyView.EndUpdate();

			this.activeFileLabel.Text = this.openFileDialog.FileName;

			input.Close();
		}

		private void OnSave(object sender, EventArgs e)
		{

		}

		private void OnSaveAs(object sender, EventArgs e)
		{

		}

		private void OnAfterNodeSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag is Gibbed.Spore.Properties.TextProperty)
			{
				Gibbed.Spore.Properties.TextProperty property = (Gibbed.Spore.Properties.TextProperty)(e.Node.Tag);
				
				this.tabControl.SelectTab(this.textTab);
				this.textTableIdTextBox.Text = "0x" + property.TableId.ToString("X8");
				this.textInstanceIdTextBox.Text = "0x" + property.InstanceId.ToString("X8");
				this.textPlaceholderTextBox.Text = property.PlaceholderText;
			}
		}
	}
}
