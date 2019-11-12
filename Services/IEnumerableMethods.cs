using System;
using System.Collections.Generic;
using System.Linq;

namespace Furmanov.Services
{
	public static class EnumerableMethods
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			var array = enumerable as T[] ?? enumerable.ToArray();
			foreach (var item in array)
			{
				action?.Invoke(item);
			}
		}

		public static IEnumerable<DateTime> AllDaysInMonth(this DateTime month)
		{
			var start = new DateTime(month.Year, month.Month, 1);
			var count = DateTime.DaysInMonth(month.Year, month.Month);

			return Enumerable.Range(0, count).Select(i => start.AddDays(i));
		}
	}
}
