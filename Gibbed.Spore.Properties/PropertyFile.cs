using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	public static class TypeHelpers
	{
		private static PropertyDefinitionAttribute GetPropertyDefinition(this Type type)
		{
			if (type.IsSubclassOf(typeof(Property)) == false)
			{
				return null;
			}

			object[] attributes = type.GetCustomAttributes(typeof(PropertyDefinitionAttribute), false);

			if (attributes.Length == 0)
			{
				return null;
			}

			return (PropertyDefinitionAttribute)(attributes[0]);
		}

		public static string GetSingularName(this Type type)
		{
			PropertyDefinitionAttribute def = type.GetPropertyDefinition();

			if (def == null)
			{
				return null;
			}

			return def.Name;
		}

		public static string GetPluralName(this Type type)
		{
			PropertyDefinitionAttribute def = type.GetPropertyDefinition();

			if (def == null)
			{
				return null;
			}

			return def.PluralName;
		}
	}

	public class PropertyFile
	{
		public Dictionary<uint, Property> Values = new Dictionary<uint, Property>();

		public PropertyFile()
		{
			this.BuildFileTypes();
		}

		internal class PropertyLookup
		{
			public Type Type;
			public PropertyDefinitionAttribute Definition;
		}

		private Dictionary<ushort, PropertyLookup> PropertyTypes;
		private void BuildFileTypes()
		{
			this.PropertyTypes = new Dictionary<ushort, PropertyLookup>();
			foreach (Type type in Assembly.GetAssembly(this.GetType()).GetTypes())
			{
				if (type.IsSubclassOf(typeof(Property)))
				{
					object[] attributes = type.GetCustomAttributes(typeof(PropertyDefinitionAttribute), false);
					if (attributes.Length > 0)
					{
						PropertyDefinitionAttribute propDef = (PropertyDefinitionAttribute)(attributes[0]);

						if (this.PropertyTypes.ContainsKey(propDef.FileType) == true)
						{
							throw new Exception("duplicate property type id " + propDef.FileType.ToString());
						}

						this.PropertyTypes[propDef.FileType] = new PropertyLookup();
						this.PropertyTypes[propDef.FileType].Type = type;
						this.PropertyTypes[propDef.FileType].Definition = propDef;
					}
				}
			}
		}

		private Type GetTypeFromFileType(ushort dataType)
		{
			if (this.PropertyTypes.ContainsKey(dataType))
			{
				return this.PropertyTypes[dataType].Type;
			}
			
			return null;
		}

		public string Literal(Dictionary<uint, string> names)
		{
			StringBuilder builder = new StringBuilder();

			foreach (uint hash in this.Values.Keys)
			{
				Property property = this.Values[hash];

				builder.Append("property");
				builder.Append(" " + (names.ContainsKey(hash) ? names[hash] : "0x" + hash.ToString("X8")));
				builder.Append(" " + "0x" + hash.ToString("X8"));

				if (property is ArrayProperty)
				{
					ArrayProperty array = (ArrayProperty)(property);
					builder.Append(" " + array.PropertyType.GetPluralName());
				}
				else
				{
					builder.Append(" " + property.GetType().GetSingularName());
				}

				string literal = property.Literal;

				if (literal.Length > 0)
				{
					builder.Append(" " + literal);
				}

				builder.AppendLine();
			}
			
			return builder.ToString();
		}

		public void Read(Stream input)
		{
			uint count = input.ReadU32BE();

			for (uint i = 0; i < count; i++)
			{
				uint hash = input.ReadU32BE();
				ushort fileType = input.ReadU16BE();
				ushort flags = input.ReadU16BE();

				if (this.Values.ContainsKey(hash))
				{
					throw new Exception("property file already has " + hash.ToString("X8") + " defined");
				}

				Property property = null;
				Type type = this.GetTypeFromFileType(fileType);

				if (type == null)
				{
					throw new Exception("invalid type " + fileType.ToString());
				}

				if ((flags & 0x30) == 0) // is not variant?
				{
					property = Activator.CreateInstance(type) as Property;
					property.Read(input, false);

					this.Values[hash] = property;
				}
				// Variant
				else if ((flags & 0x40) == 0) // is not empty?
				{
					ArrayProperty array = new ArrayProperty();
					array.PropertyType = type;

					int arrayCount = input.ReadS32BE();
					int arrayItemSize = input.ReadS32BE();

					for (uint j = 0; j < arrayCount; j++)
					{
						MemoryStream memory = new MemoryStream();
						byte[] data = new byte[arrayItemSize];
						input.Read(data, 0, arrayItemSize);
						memory.Write(data, 0, arrayItemSize);
						memory.Seek(0, SeekOrigin.Begin);

						Property subproperty = Activator.CreateInstance(type) as Property;
						subproperty.Read(memory, true);
						array.Values.Add(subproperty);
					}

					property = array;

					this.Values[hash] = property;
				}
			}
		}

		public void Write(Stream input)
		{

		}
	}
}
