using System.Drawing;

namespace Furmanov.Services
{
	public static class ColorService
	{
		public static Color ToLight(this Color c, int diff)
		{
			int ligth(int from)
			{
				var res = from + diff;
				if (res > 255) return 255;
				if (res < 0) return 0;
				return res;
			}

			return Color.FromArgb(c.A, ligth(c.R), ligth(c.G), ligth(c.B));
		}
	}
}
