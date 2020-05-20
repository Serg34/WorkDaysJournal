namespace SC.Common.Services
{
	public class MoneyToString
	{
		public static string RurToWord(decimal money)
		{
			return CurPhrase(money, "рубль", "рубля", "рублей", "копейка", "копейки", "копеек");
		}
		public static string CountToWord(decimal money)
		{
			return CurPhrase(money, "наименование", "наименования", "наименований", "", "", "");
		}

		public static string UsdToWord(decimal money)
		{
			return CurPhrase(money, "доллар США", "доллара США", "долларов США", "цент", "цента", "центов");
		}

		public static string NumPhrase(ulong value, bool isMale, bool isGenitive = false)
		{
			if (value == 0UL) return "Ноль";
			string[] Dek1 = { "", " од", " дв", " три", " четыре", " пять", " шесть", " семь", " восемь", " девять", " десять", " одиннадцать", " двенадцать", " тринадцать", " четырнадцать", " пятнадцать", " шестнадцать", " семнадцать", " восемнадцать", " девятнадцать" };
			string[] Dek1Genitive = { "", " од", " дв", " трёх", " четырёх", " пять", " шесть", " семь", " восемь", " девять", " десять", " одиннадцать", " двенадцать", " тринадцать", " четырнадцать", " пятнадцать", " шестнадцать", " семнадцать", " восемнадцать", " девятнадцать" };
			string[] Dek2 = { "", "", " двадцать", " тридцать", " сорок", " пятьдесят", " шестьдесят", " семьдесят", " восемьдесят", " девяносто" };
			string[] Dek3 = { "", " сто", " двести", " триста", " четыреста", " пятьсот", " шестьсот", " семьсот", " восемьсот", " девятьсот" };
			string[] Th = { "", "", " тысяч", " миллион", " миллиард", " триллион", " квадрилион", " квинтилион" };
			string str = "";
			for (byte th = 1; value > 0; th++)
			{
				ushort gr = (ushort)(value % 1000);
				value = (value - gr) / 1000;
				if (gr > 0)
				{
					byte d3 = (byte)((gr - gr % 100) / 100);
					byte d1 = (byte)(gr % 10);
					byte d2 = (byte)((gr - d3 * 100 - d1) / 10);
					if (d2 == 1) d1 += (byte)10;
					bool ismale = (th > 2) || ((th == 1) && isMale);
					str = Dek3[d3]
						+ Dek2[d2]
						+ (isGenitive ? Dek1Genitive[d1] : Dek1[d1])
						+ (isGenitive ? EndDek1Genitive(d1, ismale) : EndDek1(d1, ismale))
						+ Th[th]
						+ EndTh(th, d1)
						+ str;
				};
			};
			str = str.Substring(1, 1).ToUpper() + str.Substring(2);
			return str;
		}

		#region Private members
		private static string CurPhrase(decimal money,
			string word1, string word234, string wordmore,
			string sword1 = null, string sword234 = null, string swordmore = null,
			bool isMale = true, bool isGenitive = false)
		{
			money = decimal.Round(money, 2);
			decimal decintpart = decimal.Truncate(money);
			ulong intpart = decimal.ToUInt64(decintpart);
			string str = "(" + NumPhrase(intpart, isMale, isGenitive);
			byte endpart = (byte)(intpart % 100UL);
			if (endpart > 19) endpart = (byte)(endpart % 10);
			switch (endpart)
			{
				case 1: str += ") " + word1; break;
				case 2:
				case 3:
				case 4: str += ") " + word234; break;
				default: str += ") " + wordmore; break;
			}
			byte fracpart = decimal.ToByte((money - decintpart) * 100M);
			if (string.IsNullOrEmpty(sword1)) return str;
			str += " " + ((fracpart < 10) ? "0" : "") + fracpart.ToString() + " ";
			if (fracpart > 19) fracpart = (byte)(fracpart % 10);
			switch (fracpart)
			{
				case 1: str += sword1; break;
				case 2:
				case 3:
				case 4: str += sword234; break;
				default: str += swordmore; break;
			};
			return str;
		}
		private static string EndTh(byte ThNum, byte Dek)
		{
			bool In234 = ((Dek >= 2) && (Dek <= 4));
			bool More4 = ((Dek > 4) || (Dek == 0));
			if (((ThNum > 2) && In234) || ((ThNum == 2) && (Dek == 1))) return "а";
			else if ((ThNum > 2) && More4) return "ов";
			else if ((ThNum == 2) && In234) return "и";
			else return "";
		}
		private static string EndDek1(byte dek, bool isMale)
		{
			if ((dek > 2) || (dek == 0)) return "";
			else if (dek == 1) return isMale ? "ин" : "на";
			else return isMale ? "а" : "е";
		}
		private static string EndDek1Genitive(byte dek, bool isMale)
		{
			if ((dek > 2) || (dek == 0)) return "";
			else if (dek == 1) return isMale ? "ного" : "ну";
			else return isMale ? "а" : "е";
		}
		#endregion
	}
}