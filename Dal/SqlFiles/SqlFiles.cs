using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Furmanov.Dal.Queries
{
	public static class SqlFiles
	{
		private static readonly IDictionary<string, string> _queries;

		static SqlFiles()
		{
			string GetShortResourceName(string r)
			{
				var res = Path.GetFileNameWithoutExtension(r);
				res = res?.Replace('.', Path.DirectorySeparatorChar);
				res = Path.GetFileNameWithoutExtension(res);
				res += ".sql";
				return res;
			}

			var assembly = Assembly.GetExecutingAssembly();
			var resources = assembly?.GetManifestResourceNames()
				.Where(n => n.EndsWith(".sql", true, CultureInfo.CurrentCulture))
				.Select(r => new
				{
					key = GetShortResourceName(r),
					query = new StreamReader(assembly.GetManifestResourceStream(r)).ReadToEnd()
				})
				.ToArray();

			_queries = resources?.ToDictionary(r => r.key, r => r.query);
		}
		public static string Get(string fileName) => _queries[fileName];
	}
}
