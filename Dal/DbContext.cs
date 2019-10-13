using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;

namespace Furmanov.Dal
{
	public class DbDataContext : DataConnection
	{
		public DbDataContext(string connectionString) : base(GetDataProvider(), connectionString) { }

		private static IDataProvider GetDataProvider()
		{
			// you can move this line to other place, but it should be
			// allways set before LINQ to DB provider instance creation
			LinqToDB.Common.Configuration.AvoidSpecificDataProviderAPI = true;

			return new SqlServerDataProvider("", SqlServerVersion.v2017);
		}
	}
}
