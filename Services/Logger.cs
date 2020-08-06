using System;

namespace Furmanov.Services
{
	public static class Logger
	{
		public static void Log1C(string log, bool isBegin = false)
		{
#if DEBUG
			System.IO.File.AppendAllText($"log {DateTime.Today:yyyy-MM-dd}.txt",
				(isBegin ? "\n*** " : "") +
				$"{DateTime.Now}: {log}" +
				(isBegin ? " ***" : "") +
				"\n");
#endif
		}
	}
}
