using System;
using System.IO;
using System.Windows.Forms;

namespace Gibbed.Spore.PackageMaker
{
	class Program
	{
		static void Main(string[] args)
		{
			Maker maker = new Maker();

			if (args.Length != 2)
			{
				Console.WriteLine("{0} <new.package> <files.xml>", Path.GetFileName(Application.ExecutablePath));
				return;
			}

			string outputPath = args[0];
			string inputPath = args[1];

			maker.Build(inputPath, outputPath);
		}
	}
}
