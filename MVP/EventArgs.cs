using System;
using Furmanov.Data.Data;

namespace Furmanov.MVP
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

	public class BugEventArgs : EventArgs
	{
		public BugEventArgs(Exception exception)
		{
			Exception = exception;
		}
		public BugEventArgs(Exception exception, string infoForDeveloper)
		{
			Exception = exception;
			InfoForDeveloper = infoForDeveloper;
		}

		public Exception Exception { get; set; }
		public string InfoForDeveloper { get; set; }

		public Bug Bug { get; set; }
	}
}
