using Furmanov.Data.Data;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Furmanov.Data
{
	public class DbContext : DataConnection
	{
		public DbContext(string connectionString) : base(GetDataProvider(), connectionString) { }

		private static IDataProvider GetDataProvider()
		{
			// you can move this line to other place, but it should be
			// allways set before LINQ to DB provider instance creation
			//LinqToDB.Common.Configuration.AvoidSpecificDataProviderAPI = true;

			return new SqlServerDataProvider("", SqlServerVersion.v2017);
		}
		public T FirstOrDefault<T>(Expression<Func<T, bool>> func) where T : class
		{
			var res = GetTable<T>().Where(func).FirstOrDefault();
			return res;
		}

		public T[] GetWhere<T>(Expression<Func<T, bool>> func) where T : class
		{
			var res = GetTable<T>().Where(func).ToArray();
			return res;
		}
		public void Delete<T>() where T : class => GetTable<T>().Delete();
	}
}
