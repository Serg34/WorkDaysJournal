using Furmanov.Dal;
using Furmanov.Data.Data;
using Furmanov.Data.Properties;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Furmanov.Data
{
	#region IDataAccessService
	public interface IDataAccessService
	{
		void CreateDataBase();
		LoginPassword GetAutoLoginPassword();
		void SaveAutoLoginPassword(LoginPassword loginPassword);
		User GetUser(string login, string password);

		List<SalaryPay> GetSalaryPays(User user, int year, int month);
		void SaveSalaryPay(SalaryPayDto salaryPayDto);

		List<WorkedDayDto> GetWorkedDays(int salaryPayId, int year, int month);
		void SaveWorkedDays(params WorkedDay[] workedDayDb);
		void Insert<T>(T obj);
	}
	#endregion

	public class DataAccessService : IDataAccessService
	{
		private readonly string _connectionString;

		public DataAccessService(string connectionString)
		{
			_connectionString = connectionString;
			CreateDataBase();
		}

		public void CreateDataBase()
		{
			using (var db = new DbContext(_connectionString))
			{
				//var baseDir = AppDomain.CurrentDomain.BaseDirectory;
				//db.Execute("CREATE DATABASE [Furmanov] ON PRIMARY " +
				//		  "(NAME = [Furmanov], " +
				//		  $"FILENAME = '{Path.Combine(baseDir, "Furmanov.mdf")}', " +
				//		  "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
				//		  "LOG ON (NAME = Furmanov_Log, " +
				//		  $"FILENAME = '{Path.Combine(baseDir, "Furmanov.ldf")}', " +
				//		  "SIZE = 1MB, " +
				//		  "MAXSIZE = 5MB, " +
				//		  "FILEGROWTH = 10%)");

				//db.CreateTable<EmployeeDto>();
				//db.CreateTable<RoleDto>();
				//db.CreateTable<ObjectDto>();
				//db.CreateTable<ProjectDto>();
				//db.CreateTable<SalaryPayDto>();
				//db.CreateTable<WorkedDayDto>();

				//var tables = new[]
				//{
				//	nameof(UserDto),
				//	nameof(SalaryPayDto),
				//	nameof(WorkedDayDto)
				//};
				//foreach (var table in tables)
				//{
				//	try
				//	{
				//		var tableName = table.Replace("Dto", "");
				//		db.Execute($"ALTER TABLE [dbo].[{tableName}] " +
				//				   $"ADD CONSTRAINT [DF_{tableName}_{nameof(Dto.CreatedDate)}] " +
				//				   $"DEFAULT (getdate()) FOR [{nameof(Dto.CreatedDate)}]\n" +
				//				   "GO");
				//	}
				//	catch { }
				//}
			}
		}

		public User GetUser(string login, string password)
		{
			using (var db = new DbContext(_connectionString))
			{
				var res = db.Query<User>(Resources.User,
					new DataParameter("@login", login),
					new DataParameter("@password", password))
					.FirstOrDefault();
				return res;
			}
		}
		public LoginPassword GetAutoLoginPassword()
		{
			var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"Furmanov",
				"WorkedDaysJournal",
				"LoginPassword.xml");

			var res = new XmlRepository<LoginPassword>(file).Load();
			return res;
		}
		public void SaveAutoLoginPassword(LoginPassword loginPassword)
		{
			var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"Furmanov",
				"WorkedDaysJournal",
				"LoginPassword.xml");
			new XmlRepository<LoginPassword>(file).Save(loginPassword);
		}

		public List<SalaryPay> GetSalaryPays(User user, int year, int month)
		{
			if (user == null) return new List<SalaryPay>();

			using (var db = new DbContext(_connectionString))
			{
				var sql = user.Role == Role.Manager ? Resources.SalaryPayForManager
					: Resources.SalaryPayForProjectManager;

				var res = db.Query<SalaryPay>(sql,
					new DataParameter("@userId", user.Id),
					new DataParameter("@year", year),
					new DataParameter("@month", month))
					.ToList();
				return res;
			}
		}
		public void SaveSalaryPay(SalaryPayDto salaryPayDto)
		{
			using (var db = new DbContext(_connectionString))
			{
				db.Update(salaryPayDto);
			}
		}

		public List<WorkedDayDto> GetWorkedDays(int salaryPayId, int year, int month)
		{
			using (var db = new DbContext(_connectionString))
			{
				var res = db.GetTable<WorkedDayDto>()
						.Where(p => p.SalaryPayId == salaryPayId)
						.Where(p => p.Date.Year == year)
						.Where(p => p.Date.Month == month)
						.ToList();

				return res;
			}
		}
		public void SaveWorkedDays(params WorkedDay[] workedDay)
		{
			using (var db = new DbContext(_connectionString))
			{
				var noWork = workedDay.Where(t => !t.IsWorked).ToArray();
				foreach (var day in noWork)
				{
					db.Execute(Resources.DeleteWorkDay,
						new DataParameter("@payId", day.SalaryPayId),
						new DataParameter("@day", day.Date));
				}

				var work = workedDay.Where(t => t.IsWorked).ToArray();
				foreach (var day in work)
				{
					db.Execute(Resources.InsertWorkDay,
						new DataParameter("@payId", day.SalaryPayId),
						new DataParameter("@day", day.Date));
				}
			}
		}

		public void Insert<T>(T obj)
		{
			using (var db = new DbContext(_connectionString))
			{
				db.Insert(obj);
			}
		}
	}
}
