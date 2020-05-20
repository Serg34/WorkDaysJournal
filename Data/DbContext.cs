using System;
using System.Linq;
using System.Linq.Expressions;
using Furmanov.Data.Data;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using SC.Common.Services;

namespace Furmanov.Data
{
	public class DbContext : DataConnection
	{
		private static string _connectionString;
		public DbContext() : this(GetConnectionString()) { }

		public DbContext(string connectionString) : base(GetDataProvider(), connectionString) { }

		public static string DataSource
		{
			get
			{
#if DEBUG
				return "Progerx.svr.vc";
				return "mizeroff.net";
#else
				return "mizeroff.net";
#endif
			}
		}

		public static string GetConnectionString()
		{
			if (_connectionString.IsEmpty())
			{
#if DEBUG
				_connectionString = $"Data Source = {DataSource}; Initial Catalog = SwissClean; User ID = scuser; Password = !QAZ1qaz;";
#else
				var connectionString = new ConnectionStringRepository().Load();
				_connectionString = connectionString.Main;
#endif
			}
			return _connectionString;
		}

		private static IDataProvider GetDataProvider()
		{
			// you can move this line to other place, but it should be
			// allways set before LINQ to DB provider instance creation
			LinqToDB.Common.Configuration.AvoidSpecificDataProviderAPI = true;

			return new SqlServerDataProvider("", SqlServerVersion.v2017);
		}

		public T GetById<T>(int? id) where T : class, IHasID
		{
			if (id == null) return null;
			return GetTable<T>().FirstOrDefault(o => o.ID == id.Value);
		}
		public int GetId<T>(string name) where T : class, IHasID, IHasName
		{
			var res = GetTable<T>().FirstOrDefault(o => o.Name == name);
			return res?.ID ?? -1;
		}
		public int GetId<T>(Func<T, bool> cond) where T : class, IHasID
		{
			var res = GetTable<T>().FirstOrDefault(cond);
			return res?.ID ?? -1;
		}
		public T[] GetWhere<T>(Func<T, bool> cond) where T : class => GetTable<T>().Where(cond).ToArray();
		public string[] AllNames<T>() where T : class, IHasName
		{
			return GetTable<T>()
				.Where(o => !string.IsNullOrEmpty(o.Name))
				.Select(o => o.Name)
				.OrderBy(o => o)
				.ToArray();
		}
		public T GetOrCreate<T>(string name) where T : class, IHasID, IHasName, new()
		{
			if (name.IsEmpty()) return null;
			using (var db = new DbContext())
			{
				var res = FirstOrDefault<T>(a => a.Name == name);
				if (res != null) return res;

				res = new T { Name = name };
				res.ID = db.InsertWithInt32Identity(res);
				return res;
			}
		}
		public string[] AllNames<T>(Expression<Func<T, bool>> cond) where T : class, IHasName
		{
			return GetTable<T>()
				.Where(cond)
				.Where(o => !string.IsNullOrEmpty(o.Name))
				.Select(o => o.Name)
				.OrderBy(o => o)
				.ToArray();
		}
		public T FirstOrDefault<T>(string name) where T : class, IHasName
		{
			return GetTable<T>().FirstOrDefault(o => o.Name == name);
		}
		public T FirstOrDefault<T>(Expression<Func<T, bool>> cond) where T : class
		{
			return GetTable<T>().FirstOrDefault(cond);
		}
		public void DeleteById<T>(int id) where T : class, IHasID
		{
			GetTable<T>().Where(o => o.ID == id).Delete();
		}
		public void Delete<T>(T obj) where T : class, IHasID
		{
			GetTable<T>().Where(o => o.ID == obj.ID).Delete();
		}
		public void Delete<T>(Expression<Func<T, bool>> cond) where T : class
		{
			GetTable<T>().Where(cond).Delete();
		}
	}
	public class FeedbackDbContext : DbContext
	{
		private static string _connectionString;
		public FeedbackDbContext() : base(GetConnectionString()) { }
		public new static string GetConnectionString()
		{
			if (_connectionString.IsEmpty())
			{
				var connectionString = new ConnectionStringRepository().Load();
				_connectionString = connectionString.Feedback;
			}
			return _connectionString;
		}
	}
	public class TestDbContext : DbContext
	{
		private static string _connectionString;
		public TestDbContext() : base(GetConnectionString())
		{
			if (ConnectionString.Contains("mizeroff.net"))
			{
				throw new Exception("Тест выполняется на боевой базе данных");
			}
		}
		public new static string GetConnectionString()
		{
			if (_connectionString.IsEmpty())
			{
				var connectionString = new ConnectionStringRepository().Load();
				_connectionString = connectionString.Test;
			}
			return _connectionString;
		}
	}
	public class HistoryDbContext : DbContext
	{
		private static string _connectionString;
		public HistoryDbContext() : base(GetConnectionString()) { }
		public new static string GetConnectionString()
		{
			if (_connectionString.IsEmpty())
			{
#if DEBUG
				_connectionString = $"Data Source = {DataSource}; Initial Catalog = SC_History; User ID = scuser; Password = !QAZ1qaz;";
#else
				var connectionString = new ConnectionStringRepository().Load();
				_connectionString = connectionString.History;
#endif
			}
			return _connectionString;
		}
	}
}
