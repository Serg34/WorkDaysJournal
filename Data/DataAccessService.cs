using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Furmanov.Dal;
using Furmanov.Dal.Data;
using Furmanov.Data.Properties;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Furmanov.Data;

namespace Furmanov.Data
{
	#region IDataAccessService
	public interface IDataAccessService
	{
		LoginPassword GetAutoLoginPassword();
		void SaveAutoLoginPassword(LoginPassword loginPassword);
		User GetUser(string login, string password);

		List<SalaryPay> GetSalaryPays(User user, DateTime month);
		void SaveSalaryPay(SalaryPayDto salaryPayDto);

		List<WorkedDayDto> GetWorkedDays(int resOpId, DateTime month);
		void SaveWorkedDays(params WorkedDay[] workedDayDb);
	}
	#endregion

	public class DataAccessService : IDataAccessService
	{
		private readonly string _connectionString;

		public DataAccessService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public User GetUser(string login, string password)
		{
			using (var db = new DbContext(_connectionString))
			{
				var res = db.Query<User>(Resources.UserDb,
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

		public List<SalaryPay> GetSalaryPays(User user, DateTime month)
		{
			if (user == null) return new List<SalaryPay>();

			using (var db = new DbContext(_connectionString))
			{
				var sql = user.Role == Role.Manager ? Resources.SalaryPayForManager
					: Resources.SalaryPayForProjectManager;

				var res = db.Query<SalaryPay>(sql,
					new DataParameter("@userId", user.Id),
					new DataParameter("@month", $"{month:yyyyMMdd}"))
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

		public List<WorkedDayDto> GetWorkedDays(int salaryPayId, DateTime month)
		{
			using (var db = new DbContext(_connectionString))
			{
				var res = db.GetTable<WorkedDayDto>()
						.Where(t => t.SalaryPayId == salaryPayId)
						.Where(t => t.Date.Year == month.Year)
						.Where(t => t.Date.Month == month.Month)
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
						new DataParameter("@day", $"{day.Date:yyyyMMdd}"));
				}

				var work = workedDay.Where(t => t.IsWorked).ToArray();
				foreach (var day in work)
				{
					db.Execute(Resources.InsertWorkDay,
						new DataParameter("@payId", day.SalaryPayId),
						new DataParameter("@day", $"{day.Date:yyyyMMdd}"));
				}
			}
		}
	}
}
