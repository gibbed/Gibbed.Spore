using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gibbed.Spore.PropHashes
{
	class Program
	{
		static void HandleDirectory(Finder finder, string path)
		{
			string[] inputPaths = Directory.GetFiles(path, "*.prop", SearchOption.TopDirectoryOnly);

			foreach (string inputPath in inputPaths)
			{
				Console.WriteLine(inputPath);

				Stream input = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
				finder.Add(input);
				input.Close();
			}

			inputPaths = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

			foreach (string inputPath in inputPaths)
			{
				HandleDirectory(finder, inputPath);
			}
		}

		static void Main(string[] args)
		{
			Finder finder = new Finder();
			HandleDirectory(finder, Directory.GetCurrentDirectory());
			finder.Compile();
		}
	}
}
