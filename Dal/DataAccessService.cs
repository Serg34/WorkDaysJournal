using Furmanov.Dal.Dto;
using Furmanov.Dal.Queries;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Furmanov.Dal
{
	#region IdataAccessService
	public interface IDataAccessService
	{
		LoginPassword GetAutoLoginPassword();
		void SaveAutoLoginPassword(LoginPassword loginPassword);
		UserVisual GetUser(string login, string password);

		List<SalaryPayVisual> GetSalaryPays(UserVisual user, DateTime month);
		void SaveSalaryPay(SalaryPayDb salaryPayDb);
		void DeleteSalaryPay(SalaryPayDb salaryPayDb);

		List<WorkedDayDb> GetWorkedDays(int resOpId, DateTime month);
		void SaveWorkedDays(params WorkedDayVisual[] workedDayDb);
		DataTable Report(UserVisual user, int objectId = 0);
	}
	#endregion

	public class DataAccessService : IDataAccessService
	{
		private readonly string _connectionString;

		public DataAccessService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public UserVisual GetUser(string login, string password)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				var sql = SqlFiles.Get("User.sql");
				var res = db.Query<UserVisual>(sql,
					new DataParameter("@login", login),
					new DataParameter("@password", password))
					.FirstOrDefault();
				return res;
			}
		}
		public LoginPassword GetAutoLoginPassword()
		{
			var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"SC.WorkedDaysDb",
				"LoginPassword.xml");

			var res = new XmlRepository<LoginPassword>(file).Load();
			return res;
		}
		public void SaveAutoLoginPassword(LoginPassword loginPassword)
		{
			var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"SC.WorkedDaysDb",
				"LoginPassword.xml");
			new XmlRepository<LoginPassword>(file).Save(loginPassword);
		}

		public List<SalaryPayVisual> GetSalaryPays(UserVisual user, DateTime month)
		{
			if (user == null) return new List<SalaryPayVisual>();

			using (var db = new DbDataContext(_connectionString))
			{
				var fileName = user.Role == Role.Manager ? "SalaryPayViewForManager.sql"
					: "SalaryPayViewForProjectManager.sql";
				var sql = SqlFiles.Get(fileName);

				var res = db.Query<SalaryPayVisual>(sql,
					new DataParameter("@userId", user.Id),
					new DataParameter("@month", month.ToString("yyyyMMdd")))
					.ToList();
				return res;
			}
		}
		public void SaveSalaryPay(SalaryPayDb salaryPayDb)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				db.Update(salaryPayDb);
			}
		}
		public void DeleteSalaryPay(SalaryPayDb salaryPayDb)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				db.Delete(salaryPayDb);
			}
		}

		public List<WorkedDayDb> GetWorkedDays(int salaryPayId, DateTime month)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				var res = db.GetTable<WorkedDayDb>()
						.Where(t => t.SalaryPayId == salaryPayId)
						.Where(t => t.Date.Year == month.Year)
						.Where(t => t.Date.Month == month.Month)
						.ToList();

				return res;
			}
		}
		public void SaveWorkedDays(params WorkedDayVisual[] workedDay)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				var sqlNoWork = SqlFiles.Get("DeleteWorkDay.sql");
				var noWork = workedDay.Where(t => !t.IsWorked).ToArray();
				foreach (var day in noWork)
				{
					db.Execute(sqlNoWork,
						new DataParameter("@payId", day.SalaryPayId),
						new DataParameter("@day", day.Date.ToString("yyyyMMdd")));
				}

				var sqlWork = SqlFiles.Get("InsertWorkDay.sql");
				var work = workedDay.Where(t => t.IsWorked).ToArray();
				foreach (var day in work)
				{
					db.Execute(sqlWork,
						new DataParameter("@payId", day.SalaryPayId),
						new DataParameter("@day", day.Date.ToString("yyyyMMdd")));
				}
			}
		}

		public DataTable Report(UserVisual user, int objectId = 0)
		{
			throw new NotImplementedException();
		}
	}
}
