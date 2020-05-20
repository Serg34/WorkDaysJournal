using System.Globalization;

namespace SC.Common.Services
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
	}
}
