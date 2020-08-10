using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Furmanov.Services
{
	public static class EnumerableMethods
	{
		[DebuggerStepThrough]
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			var array = enumerable as T[] ?? enumerable.ToArray();
			foreach (var item in array)
			{
				action?.Invoke(item);
			}
		}
		public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || !enumerable.Any();
		public static bool NoEmpty<T>(this IEnumerable<T> enumerable) => enumerable != null && enumerable.Any();
	}
}
