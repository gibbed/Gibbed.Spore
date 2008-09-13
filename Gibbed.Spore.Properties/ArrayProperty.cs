using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Spore.Helpers;

namespace Gibbed.Spore.Properties
{
	public class ArrayProperty : Property
	{
		public Type PropertyType;
		public List<Property> Values = new List<Property>();

		public override void Read(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override void Write(Stream input, bool array)
		{
			throw new NotImplementedException();
		}

		public override string Literal
		{
			get
			{
				string rez = "";

				foreach (Property value in this.Values)
				{
					if (rez.Length > 0)
					{
						rez += " ";
					}

					string literal = value.Literal;

					if (value is ComplexProperty && literal.StartsWith("(") == false && literal.EndsWith(")") == false)
					{
						literal = "(" + literal + ")";
					}

					rez += literal;
				}

				return rez;
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
