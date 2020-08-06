using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Furmanov.Services
{
	public static class StringMethods
	{
		#region Fields
		private static readonly string DefaultSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		private static readonly string[,] RusToEn =
		{
			{"а","a"},
			{"б","b"},
			{"в","v"},
			{"г","g"},
			{"д","d"},
			{"е","e"},
			{"ё","yo"},
			{"ж","zh"},
			{"з","z"},
			{"и","i"},
			{"й","y"},
			{"к","k"},
			{"л","l"},
			{"м","m"},
			{"н","n"},
			{"о","o"},
			{"п","p"},
			{"р","r"},
			{"с","s"},
			{"т","t"},
			{"у","u"},
			{"ф","f"},
			{"х","h"},
			{"ц","ts"},
			{"ч","ch"},
			{"ш","sh"},
			{"щ","sch"},
			{"ъ","'"},
			{"ы","yi"},
			{"ь",""},
			{"э","e"},
			{"ю","u"},
			{"я","ya"}
		};
		#endregion

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

		public static string ToEn(this string rus)
		{
			rus = rus.ToLower();
			for (int i = 0; i < 33; i++)
			{
				rus = rus.Replace(RusToEn[i, 0], RusToEn[i, 1]);
			}
			return rus;
		}
	}
}