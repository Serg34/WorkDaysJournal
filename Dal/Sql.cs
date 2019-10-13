using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WindowsFormsApp1
{
	public static class Sql
	{
		public static IDictionary<string, string> Queries { get; }

		static Sql()
		{
			string GetShortResourceName(string r)
			{
				var res = Path.GetFileNameWithoutExtension(r);
				res = res?.Replace('.', Path.DirectorySeparatorChar);
				res = Path.GetFileNameWithoutExtension(res);
				res += ".sql";
				return res;
			}

			var assembly = Assembly.GetEntryAssembly();
			var resources = assembly?.GetManifestResourceNames()
				.Where(n => n.EndsWith(".sql", true, CultureInfo.CurrentCulture))
				.Select(r => new
				{
					key = GetShortResourceName(r),
					query = new StreamReader(assembly.GetManifestResourceStream(r)).ReadToEnd()
				})
				.ToArray();

			Queries = resources?.ToDictionary(r => r.key, r => r.query);
		}
	}
}
