using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Spore.Package
{
	public static class Types
	{
		public static string GetExtensionFromId(uint id)
		{
			switch (id)
			{
				// Unhashed or unknown extension
				case 0x00B1B104: return "prop";
				case 0x00E6BCE5: return "gmdl";
				case 0x011989B7: return "plt"; // Palette
				case 0x01AD2416: return "creature_traits";
				case 0x01AD2417: return "building_traits";
				case 0x01AD2418: return "vehicle_traits";
				case 0x01C135DA: return "gmsh";
				case 0x01C3C4B3: return "trait_pill";
				case 0x0248F226: return "css";
				case 0x024A0E52: return "trigger";
				case 0x02523258: return "formation";
				case 0x027C5CEF: return "ttf"; // NEW
				case 0x02D5C9AF: return "summary";
				case 0x02D5C9B0: return "summary_pill";
				case 0x02FAC0B6: return "locale";
				case 0x030BDEE3: return "pollen_metadata";
				case 0x0376C3DA: return "hm";
				case 0x0472329B: return "htra";
				case 0x065266B7: return "xhtml"; // NEW
				case 0x2F4E681C: return "raster";
				case 0x2F7D0002: return "jpeg";
				case 0x2F7D0004: return "png";
				case 0x2F7D0005: return "bmp";
				case 0x2F7D0006: return "tga";
				case 0x2F7D0007: return "gif";
				case 0x376840D7: return "movie";
				case 0x4AEB6BC6: return "tlsa";
				case 0x7C19AA7A: return "pctp";
				case 0xCF6C21B8: return "xml"; // NEW
				case 0xEFBDA3FF: return "layout"; // source format for .spui?

				// Hashed version of the extension
				case 0x12952634: return "dat";
				case 0x1A99B06B: return "bem";
				case 0x1E99B626: return "bat";
				case 0x1F639D98: return "xls";
				case 0x2399BE55: return "bld"; // building
				case 0x24682294: return "vcl"; // vehicle (land, sea, air)
				case 0x250FE9A2: return "spui"; // SPore User Interface
				case 0x25DF0112: return "gait";
				case 0x2B6CAB5F: return "txt";
				case 0x2B978C46: return "crt"; // creature
				case 0x37979F71: return "cfg";
				case 0x3C77532E: return "psd";
				case 0x3C7E0F63: return "mcl"; // muscle
				case 0x3D97A8E4: return "cll"; // cell
				case 0x3F9C28B5: return "ani";
				case 0x438F6347: return "flr"; // flora
				case 0x448AE7E2: return "hkx"; // havok physics (or effect?)
				case 0x476A98C7: return "ufo"; // spaceship
				case 0x497767B9: return "pfx"; // particle effect
				case 0x5C74D18B: return "density";
				case 0x617715C4: return "py";
				case 0x617715D9: return "pd"; // sound related
				case 0x9B8E862F: return "world";
				case 0xDFAD9F51: return "cell";
				case 0xEE17C6AD: return "animation";
			}

			return null;
		}
	}
}
