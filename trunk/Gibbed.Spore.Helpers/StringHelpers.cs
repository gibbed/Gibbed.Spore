namespace Gibbed.Spore.Helpers
{
	public static class StringHelpers
	{
		// FNV hash that EA loves to use :-)
		public static uint FNV(this string input)
		{
			string lower = input.ToLower();
			uint rez = 0x811C9DC5;

			for (int i = 0; i < lower.Length; i++)
			{
				rez *= 0x1000193;
				rez ^= (char)(lower[i]);
			}

			return rez;
		}

		public static uint GetHexNumber(this string input)
		{
			if (input.StartsWith("0x"))
			{
				return uint.Parse(input.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier);
			}

			return uint.Parse(input);
		}
	}
}
