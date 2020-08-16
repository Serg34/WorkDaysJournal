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
		public BugEventArgs(Exception exception, UserDto user)
		{
			Exception = exception;
			User = user;
		}
		public BugEventArgs(Exception exception, string infoForDeveloper, UserDto user)
		{
			Exception = exception;
			InfoForDeveloper = infoForDeveloper;
			User = user;
		}

		public Exception Exception { get; }
		public string InfoForDeveloper { get; }
		public UserDto User { get; }

		public Bug Bug { get; set; }
	}
}
