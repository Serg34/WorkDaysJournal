using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;

namespace Dal
{
	public class DbDataContext : DataConnection
	{
		public DbDataContext() : base(GetDataProvider(), GetConnectionString()) { }

		private static IDataProvider GetDataProvider()
		{
			// you can move this line to other place, but it should be
			// allways set before LINQ to DB provider instance creation
			LinqToDB.Common.Configuration.AvoidSpecificDataProviderAPI = true;

			return new SqlServerDataProvider("", SqlServerVersion.v2017);
		}
		private static string GetConnectionString()
		{
			return "Server =.\\SQLExpress; Database = Furmanov; Trusted_Connection = True;";
		}
	}
}
