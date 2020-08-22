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
		public BugEventArgs(string appName, Exception exception)
		{
			ApplicationName = appName;
			Exception = exception;
		}
		
		public string ApplicationName { get; set; }
		public Exception Exception { get; }
		
		public string InfoForDeveloper { get; }

		public Bug Bug { get; set; }
	}
}
