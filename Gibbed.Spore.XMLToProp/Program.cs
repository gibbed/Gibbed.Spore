﻿using System;
using System.IO;
using System.Windows.Forms;
using Gibbed.Spore.Properties;

namespace Gibbed.Spore.XMLToProp
{
	class Program
	{
		static void HandleDirectory(Converter converter, string path, string filter, bool recursive)
		{
			string[] inputPaths = Directory.GetFiles(path, filter, SearchOption.TopDirectoryOnly);

			foreach (string inputPath in inputPaths)
			{
				string outputPath = inputPath;
				if (Path.GetExtension(Path.ChangeExtension(outputPath, null)) == ".prop")
				{
					outputPath = Path.ChangeExtension(outputPath, null);
				}

				outputPath = Path.ChangeExtension(outputPath, ".propnew");

				Console.WriteLine("{0} => {1}", inputPath, outputPath);

				PropertyFile file = new PropertyFile();

				Stream input = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
				Stream output = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

				converter.Convert(input, output);

				input.Close();
				output.Close();
			}

			if (recursive == true)
			{
				inputPaths = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

				foreach (string inputPath in inputPaths)
				{
					HandleDirectory(converter, inputPath, filter, recursive);
				}
			}
		}

		static void Main(string[] args)
		{
			Converter converter = new Converter();

			string argFilter = "*.prop.xml";
			bool argRecursive = false;
			bool argsBad = true;

			if (args.Length == 1)
			{
				argFilter = args[0];
				argRecursive = false;
				argsBad = false;
			}
			else if (args.Length == 2)
			{
				if (args[0] == "-r")
				{
					argFilter = args[1];
					argRecursive = true;
					argsBad = false;
				}
			}

			if (argsBad)
			{
				Console.WriteLine("{0} [-r] <*.prop.xml>", Path.GetFileName(Application.ExecutablePath));
				return;
			}

			string inputDirectory = Path.GetDirectoryName(argFilter);
			if (inputDirectory == "")
			{
				inputDirectory = Directory.GetCurrentDirectory();
			}

			string inputFilter = Path.GetFileName(argFilter);

			HandleDirectory(converter, inputDirectory, inputFilter, argRecursive);
		}
	}
}
