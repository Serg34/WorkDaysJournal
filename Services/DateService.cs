using System;
using System.Collections.Generic;
using System.Linq;

namespace Furmanov.Services
{
	public static class DateService
	{
		public static IEnumerable<DateTime> AllDaysInMonth(this DateTime month)
		{
			var start = new DateTime(month.Year, month.Month, 1);
			var count = DateTime.DaysInMonth(month.Year, month.Month);

			return Enumerable.Range(0, count).Select(i => start.AddDays(i));
		}
		public static IEnumerable<DateTime> AllDaysInMonth(int year, int month)
		{
			var start = new DateTime(year, month, 1);
			var count = DateTime.DaysInMonth(year, month);

			return Enumerable.Range(0, count).Select(i => start.AddDays(i));
		}

		public static DateTime FirstMonday(this DateTime day)
		{
			var firstDay = new DateTime(day.Year, day.Month, 1);
			var dayOfWeek = firstDay.DayOfWeek;
			if (dayOfWeek == DayOfWeek.Monday) return firstDay;
			if (dayOfWeek == DayOfWeek.Sunday) return firstDay.AddDays(1);
			else return firstDay.AddDays(8 - (int)dayOfWeek);
		}

		public static IEnumerable<DateTime> Days(DateTime start, DateTime end)
		{
			start = start.Date; //округляем до даты на всякий случай
			var count = (end - start).Days + 1;
			return Enumerable.Range(0, count).Select(i => start.AddDays(i));
		}
	}
}
