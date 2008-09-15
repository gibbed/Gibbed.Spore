using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using Gibbed.Spore.Helpers;
using Gibbed.Spore.Properties;

namespace Gibbed.Spore.XMLToProp
{
	public class Converter
	{
		public Converter()
		{
		}

		public void Convert(Stream input, Stream output)
		{
			PropertyFile file = new PropertyFile();
			XmlTextReader reader = new XmlTextReader(input);

			reader.ReadToFollowing("properties");

			while (reader.Read())
			{
				if (reader.IsStartElement())
				{
					string typeName = reader.Name;
					string hashText = reader.GetAttribute("id");
					uint hash;

					hash = hashText.GetHexNumber();

					PropertyLookup lookup = file.FindPropertyType(typeName);

					if (lookup == null)
					{
						throw new Exception("unknown type " + typeName);
					}

					Property property = null;

					// Singular
					if (lookup.Definition.Name == typeName)
					{
						property = Activator.CreateInstance(lookup.Type) as Property;
						property.ReadXML(reader);
					}
					// Plural
					else if (lookup.Definition.PluralName == typeName)
					{
						ArrayProperty array = new ArrayProperty();
						array.PropertyType = lookup.Type;

						XmlReader subtree = reader.ReadSubtree();
						subtree.ReadToFollowing(typeName);

						while (subtree.Read())
						{
							if (subtree.IsStartElement())
							{
								if (subtree.Name != lookup.Definition.Name)
								{
									throw new Exception("array element for " + typeName + " is not the right type");
								}

								Property subproperty = Activator.CreateInstance(lookup.Type) as Property;
								subproperty.ReadXML(subtree);

								array.Values.Add(subproperty);
							}
						}

						property = array;
					}

					file.Values[hash] = property;
				}
			}

			file.Write(output);
		}
	}
}
