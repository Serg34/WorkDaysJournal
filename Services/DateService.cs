using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SC.Common.Services
{
	public static class DateService
	{
		public static (DateTime start, DateTime end) UpdateStartEnd(
			BarEditItem btnsIntervalMode,
			BarEditItem deMonth,
			BarEditItem deStart,
			BarEditItem deEnd)
		{
			var isMonth = (bool)(btnsIntervalMode?.EditValue ?? false);
			deMonth.Visibility = isMonth ? BarItemVisibility.Always : BarItemVisibility.Never;
			deStart.Visibility =
			deEnd.Visibility = !isMonth ? BarItemVisibility.Always : BarItemVisibility.Never;

			var month = deMonth.EditValue as DateTime?;
			var monthStart = month == null ? new DateTime(1900, 1, 1)
				: new DateTime(month.Value.Year, month.Value.Month, 1);
			var monthEnd = month == null ? DateTime.MaxValue
				: new DateTime(month.Value.Year, month.Value.Month, 1).AddMonths(1);

			var start = isMonth ? monthStart
				: (DateTime?)deStart.EditValue ?? DateTime.MinValue;
			var end = isMonth ? monthEnd
				: ((DateTime?)deEnd.EditValue)?.AddDays(1) ?? DateTime.MaxValue;

			if (deStart.Edit is RepositoryItemDateEdit riStart) riStart.MaxValue = end.AddDays(-1);
			if (deEnd.Edit is RepositoryItemDateEdit riEnd) riEnd.MinValue = start;

			return (start, end);
		}

		public static IEnumerable<DateTime> AllDaysInMonth(this DateTime month)
		{
			var start = new DateTime(month.Year, month.Month, 1);
			var count = DateTime.DaysInMonth(month.Year, month.Month);

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
