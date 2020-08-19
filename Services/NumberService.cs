using System.Globalization;

namespace Furmanov.Services
{
	public static class NumberService
	{
		private static readonly string _defaultSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

		public static decimal ToDecimal(string txt)
		{
			txt = txt.Replace(",", _defaultSeparator).Replace(".", _defaultSeparator);
			var res = decimal.Parse(txt);
			return res;
		}
		public static double ToDouble(string txt)
		{
			txt = txt.Replace(",", _defaultSeparator).Replace(".", _defaultSeparator);
			var res = double.Parse(txt);
			return res;
		}

		public static string ToHtml(this decimal? num) => num?.ToHtml();

		public static string ToHtml(this decimal num)
		{
			return num.ToString("c2").Replace(" ", $"{(char)160}");
		}
	}
}
