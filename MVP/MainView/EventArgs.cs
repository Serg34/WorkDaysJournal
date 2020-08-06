using System;

namespace Furmanov.MVP.MainView
{
	public class MonthEventArgs : EventArgs
	{
		public MonthEventArgs(int year, int month)
		{
			Year = year;
			Month = month;
		}

		public int Year { get; }
		public int Month { get; }
	}
}
