using System;
using System.IO;
using Furmanov.Services.Repositories;

namespace Furmanov.Services
{
	public class ConnectionStringRepository : XmlRepository<ConnectionString>
	{
		private static readonly string FileName =
			Path.Combine(Path.GetDirectoryName(
				        Path.GetDirectoryName(
					    Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))) 
			             ?? throw new InvalidOperationException(),
				"ConnectionString.xml");
		public ConnectionStringRepository() : base(FileName) { }
	}

	public class ConnectionString
	{
		public string Main { get; set; }
		public string Feedback { get; set; }
		public string Test { get; set; }
		public string History { get; set; }
	}
}
