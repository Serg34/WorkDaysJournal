using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SC.Common.Services
{
	public static class EnumService
	{
		public static string DisplayName<T>(this T value) where T : Enum
		{
			var fi = typeof(T).GetField(Enum.GetName(typeof(T), value));
			var dna = (DisplayAttribute)Attribute.GetCustomAttribute(fi, typeof(DisplayAttribute));

			return dna?.Name ?? value.ToString();
		}

		public static List<string> AllNames<T>() where T : Enum
		{
			var res = new List<string>();
			var values = Enum.GetValues(typeof(T));
			foreach (var value in values)
			{
				res.Add(((T)value).DisplayName());
			}
			return res;
		}
	}
}
