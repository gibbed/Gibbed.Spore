using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using Gibbed.Spore.Helpers;
using Gibbed.Spore.Properties;

namespace Gibbed.Spore.PropHashes
{
	public class Finder
	{
		public List<uint> Hashes = new List<uint>();

		public Finder()
		{
			this.LoadPropertyNames(Path.Combine(Application.StartupPath, "property_names.xml"));

			foreach (uint hash in this.PropertyNames.Keys)
			{
				this.Hashes.Add(hash);
			}
		}

		#region properties
		private Dictionary<uint, string> PropertyNames = new Dictionary<uint, string>();

		private void LoadPropertyNames(string path)
		{
			this.LoadPropertyNames(path, true);
		}

		private void LoadPropertyNames(string path, bool clear)
		{
			if (clear == true)
			{
				this.PropertyNames.Clear();
			}

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
		#endregion

		public void Compile()
		{
			long unknown = 0;
			TextReader reader = new StreamReader(Path.Combine(Application.StartupPath, "hooked_hashes.txt"));

			while (true)
			{
				string line = reader.ReadLine();
				if (line == null)
				{
					break;
				}

				string[] parts = line.Split(new char[] {'='}, 2);
				uint hash = uint.Parse(parts[0], System.Globalization.NumberStyles.AllowHexSpecifier);

				if (this.Hashes.Contains(hash) && this.PropertyNames.ContainsKey(hash) == false)
				{
					this.PropertyNames[hash] = parts[1];
				}
			}

			this.Hashes.Sort();

			XmlTextWriter writer = new XmlTextWriter(Path.Combine(Application.StartupPath, "new_property_names.xml"), Encoding.Unicode);
			writer.Formatting = Formatting.Indented;
			writer.WriteStartDocument();
			writer.WriteStartElement("names");

			foreach (uint hash in this.Hashes)
			{
				if (this.PropertyNames.ContainsKey(hash))
				{
					writer.WriteStartElement("name");
					writer.WriteAttributeString("id", "0x" + hash.ToString("X8"));
					writer.WriteValue(this.PropertyNames[hash]);
					writer.WriteEndElement();
				}
				else
				{
					unknown++;
				}
			}

			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();

			Console.WriteLine("{0} known, {1} unknown, {2} total.", this.PropertyNames.Count, unknown, this.Hashes.Count);
			Console.WriteLine("{0}% completion.", (int)(((double)this.PropertyNames.Count / (double)this.Hashes.Count) * 100));
		}

		public void Add(Stream input)
		{
			PropertyFile file = new PropertyFile();
			file.Read(input);

			foreach (uint hash in file.Values.Keys)
			{
				if (this.Hashes.Contains(hash) == false)
				{
					this.Hashes.Add(hash);
				}
			}
		}
	}
}
