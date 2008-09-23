using System;
using System.IO;
using System.Xml.XPath;
using Gibbed.Spore.Helpers;
using Gibbed.Spore.Package;

namespace Gibbed.Spore.PackageMaker
{
	public class Maker
	{
		public void Build(string filesPath, string outputPath)
		{
			string filesBasePath = Path.GetDirectoryName(filesPath);
			Stream output = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

			DatabasePackedFile dbpf = new DatabasePackedFile();
			dbpf.Version = new Version(2, 0);
			dbpf.WriteHeader(output, 0, 0);

			XPathDocument document = new XPathDocument(filesPath);
			XPathNavigator navigator = document.CreateNavigator();
			XPathNodeIterator nodes = navigator.Select("/files/file");

			while (nodes.MoveNext())
			{
				string groupText = nodes.Current.GetAttribute("groupid", "");
				string instanceText = nodes.Current.GetAttribute("instanceid", "");
				string typeText = nodes.Current.GetAttribute("typeid", "");

				if (groupText == null || instanceText == null || typeText == null)
				{
					throw new InvalidDataException("file missing attributes");
				}

				DatabaseIndex index = new DatabaseIndex();
				index.GroupId = groupText.GetHexNumber();
				index.InstanceId = instanceText.GetHexNumber();
				index.TypeId = typeText.GetHexNumber();
				index.Offset = output.Position;

				string inputPath;
				if (Path.IsPathRooted(nodes.Current.Value) == false)
				{
					// relative path, it should be relative to the XML file
					inputPath = Path.Combine(filesBasePath, nodes.Current.Value);
				}
				else
				{
					inputPath = nodes.Current.Value;
				}

				if (File.Exists(inputPath) == false)
				{
					throw new Exception(inputPath + " does not exist");
				}

				Stream input = new FileStream(nodes.Current.Value, FileMode.Open, FileAccess.Read);
				index.DecompressedSize = (uint)input.Length;
				index.CompressedSize = (uint)input.Length | 0x80000000;
				index.Flags = 1;
				index.Compressed = false;

				int left = (int)input.Length;
				byte[] data = new byte[4096];
				while (left > 0)
				{
					int chunk = Math.Min(data.Length, left);
					input.Read(data, 0, chunk);
					output.Write(data, 0, chunk);
					left -= chunk;
				}

				input.Close();
				dbpf.Indices.Add(index);
			}

			int indexOffset = (int)output.Position;
			dbpf.WriteIndex(output);
			int indexSize = (int)output.Position - indexOffset;

			output.Seek(0, SeekOrigin.Begin);
			dbpf.WriteHeader(output, indexOffset, indexSize);

			output.Close();
		}
	}
}
