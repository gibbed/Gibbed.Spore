using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

	public class PropertyLookup
	{
		public Type Type;
		public PropertyDefinitionAttribute Definition;
	}

	public class PropertyFile
	{
		public Dictionary<uint, Property> Values = new Dictionary<uint, Property>();

		public PropertyFile()
		{
			this.BuildFileTypes();
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

		public PropertyDefinitionAttribute FindPropertyType(Type type)
		{
			foreach (PropertyLookup lookup in this.PropertyTypes.Values)
			{
				if (lookup.Type == type)
				{
					return lookup.Definition;
				}
			}

			return null;
		}

		public PropertyLookup FindPropertyType(string name)
		{
			foreach (PropertyLookup lookup in this.PropertyTypes.Values)
			{
				if (lookup.Definition.Name == name || lookup.Definition.PluralName == name)
				{
					return lookup;
				}
			}
			
			return null;
		}

		private Type GetTypeFromFileType(ushort dataType)
		{
			if (this.PropertyTypes.ContainsKey(dataType))
			{
				return this.PropertyTypes[dataType].Type;
			}
			
			return null;
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
					property.ReadProp(input, false);

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
						Property subproperty = Activator.CreateInstance(type) as Property;

						if (subproperty is ComplexProperty)
						{
							MemoryStream memory = new MemoryStream();
							byte[] data = new byte[arrayItemSize];
							input.Read(data, 0, arrayItemSize);
							memory.Write(data, 0, arrayItemSize);
							memory.Seek(0, SeekOrigin.Begin);

							subproperty.ReadProp(memory, true);
						}
						else
						{
							subproperty.ReadProp(input, true);
						}

						array.Values.Add(subproperty);
					}

					property = array;

					this.Values[hash] = property;
				}
			}
		}

		public void Write(Stream output)
		{
			output.WriteS32BE(this.Values.Count);

			foreach (uint hash in this.Values.Keys)
			{
				Property property = this.Values[hash];

				output.WriteU32BE(hash);

				if (property is ArrayProperty)
				{
					ArrayProperty array = (ArrayProperty)property;
					output.WriteU16BE(this.FindPropertyType(array.PropertyType).FileType);
					output.WriteU16BE(0x30);
				}
				else if (property is ComplexProperty)
				{
					output.WriteU16BE(this.FindPropertyType(this.Values[hash].GetType()).FileType);
					output.WriteU16BE(0x30);
				}
				else
				{
					output.WriteU16BE(this.FindPropertyType(this.Values[hash].GetType()).FileType);
					output.WriteU16BE(0);

				}

				if (property is ArrayProperty)
				{
					ArrayProperty array = (ArrayProperty)property;

					MemoryStream[] memories = new MemoryStream[array.Values.Count];

					int index = 0;
					uint maxSize = 0;
					foreach (Property subproperty in array.Values)
					{
						memories[index] = new MemoryStream();
						subproperty.WriteProp(memories[index], true);

						if (memories[index].Length > maxSize)
						{
							maxSize = (uint)memories[index].Length;
						}

						index++;
					}

					output.WriteU32BE((uint)memories.Length);
					output.WriteU32BE(maxSize);

					for (int i = 0; i < memories.Length; i++)
					{
						byte[] data = new byte[maxSize];
						memories[i].Seek(0, SeekOrigin.Begin);
						memories[i].Read(data, 0, (int)(memories[i].Length));
						output.Write(data, 0, data.Length);
					}
				}
				else
				{
					MemoryStream memory = new MemoryStream();
					property.WriteProp(memory, false);

					memory.Seek(0, SeekOrigin.Begin);

					if (property is ComplexProperty)
					{
						output.WriteU32BE(1);
						output.WriteU32BE((uint)memory.Length);
					}

					byte[] data = new byte[memory.Length];
					memory.Read(data, 0, (int)memory.Length);
					output.Write(data, 0, data.Length);
				}
			}
		}
	}
}
