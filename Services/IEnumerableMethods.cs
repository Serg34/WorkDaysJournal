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
	}
}
