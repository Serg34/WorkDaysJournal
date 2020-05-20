using System;

namespace SC.Common.Services
{
	public static class Logger
	{
		public static void Log1C(string log, bool isBegin = false)
		{
#if DEBUG
			System.IO.File.AppendAllText("log1C.txt",
				(isBegin ? "\n*** " : "") +
				$"{DateTime.Now}: {log}" +
				(isBegin ? " ***" : "") +
				"\n");
#endif
		}
	}
}
