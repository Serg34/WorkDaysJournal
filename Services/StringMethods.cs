using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SC.Common.Services
{
	public static class StringMethods
	{
		private static readonly string DefaultSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		public static string ToString<T>(this IEnumerable<T> enumerable, string splitter)
		{
			var bld = new StringBuilder();
			var array = enumerable as T[] ?? enumerable.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				bld.Append(array.ElementAt(i));
				if (i < array.Length - 1) bld.Append(splitter);
			}
			return bld.ToString();
		}
		public static double ToDouble(this string str)
		{
			if (string.IsNullOrEmpty(str)) return 0;
			str = str.Replace(",", DefaultSeparator).Replace(".", DefaultSeparator);
			return double.Parse(str);
		}
		public static decimal ToDecimal(this string str)
		{
			if (string.IsNullOrEmpty(str)) return 0;
			str = str
				.Replace(" ", "")
				.Replace(",", DefaultSeparator)
				.Replace(".", DefaultSeparator);
			return decimal.Parse(str);
		}
		public static decimal ToDecimal(this object obj) => obj?.ToString().ToDecimal() ?? 0;
		public static decimal? ToNullableDecimal(this string str)
		{
			if (string.IsNullOrEmpty(str)) return null;
			str = str.Replace(",", DefaultSeparator).Replace(".", DefaultSeparator);
			return decimal.Parse(str);
		}

		/// <summary>
		/// Указывает, имеет ли указанная строка значение <see langword="null" />, является ли она пустой строкой или строкой, состоящей только из символов-разделителей
		/// </summary>
		public static bool IsEmpty(this string txt) => string.IsNullOrWhiteSpace(txt);
		/// <summary>
		/// Указывает, не имеет ли указанная строка значение <see langword="null" />, не является ли она пустой строкой или строкой, состоящей только из символов-разделителей
		/// </summary>
		public static bool NoEmpty(this string txt) => !string.IsNullOrWhiteSpace(txt);
	}
}