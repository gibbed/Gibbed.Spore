using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using Gibbed.Spore.Helpers;
using Gibbed.Spore.Properties;

namespace Gibbed.Spore.PropToXML
{
	public class Converter
	{
		public Converter()
		{
			this.LoadPropertyNames(Path.Combine(Application.StartupPath, "property_names.xml"));
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

		public void Convert(Stream input, Stream output)
		{
			PropertyFile file = new PropertyFile();
			file.Read(input);

			XmlTextWriter writer = new XmlTextWriter(output, Encoding.Unicode);
			writer.Formatting = Formatting.Indented;

			writer.WriteStartDocument();
			writer.WriteStartElement("properties");

			foreach (uint hash in file.Values.Keys)
			{
				Property property = file.Values[hash];

				if (!(property is ArrayProperty))
				{
					writer.WriteStartElement(property.GetType().GetSingularName());
				}
				else
				{
					ArrayProperty array = (ArrayProperty)property;
					writer.WriteStartElement(array.PropertyType.GetPluralName());
				}

				if (this.PropertyNames.ContainsKey(hash))
				{
					writer.WriteAttributeString("name", this.PropertyNames[hash]);
				}

				writer.WriteAttributeString("id", "0x" + hash.ToString("X8"));

				property.WriteXML(writer);

				writer.WriteEndElement();
			}

			writer.WriteEndDocument();
			writer.Flush();
		}
	}
}
