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
		DbContext GetDataContext();
		LoginPassword GetAutoLoginPassword();
		void SaveAutoLoginPassword(LoginPassword loginPassword);
		User GetUser(string login, string password);

		List<SalaryPay> GetSalaryPays(User user, int year, int month);
		void SaveSalaryPay(SalaryPayDto salaryPayDto);

		List<WorkedDayDto> GetWorkedDays(int salaryPayId, int year, int month);
		void SaveWorkedDays(params WorkedDay[] workedDayDb);
		void Insert<T>(T obj);
		T[] GetTable<T>() where T : Dto;
	}
	#endregion

	public class DataAccessService : IDataAccessService
	{
		private readonly string _connectionString;

		public DataAccessService(string connectionString)
		{
			_connectionString = connectionString;
		}
		public DbContext GetDataContext() => new DbContext(_connectionString);
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
			var userId = user.Role == Role.Admin || user.Role == Role.Director ? 0 : user.Id;

			using (var db = new DbContext(_connectionString))
			{
				var sql = user.Role == Role.Manager ? Resources.SalaryPayForManager
					: Resources.SalaryPayForProjectManager;

				var res = db.Query<SalaryPay>(sql,
					new DataParameter("@userId", userId),
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
				salaryPayDto.UpdatedByUser = Environment.UserName;
				salaryPayDto.UpdatedDate = DateTime.Now;
				db.Update(salaryPayDto);
			}
		}

		public List<WorkedDayDto> GetWorkedDays(int salaryPayId, int year, int month)
		{
			using (var db = new DbContext(_connectionString))
			{
				var res = db.GetTable<WorkedDayDto>()
						.Where(p => p.SalaryPay_Id == salaryPayId)
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
						new DataParameter("@payId", day.SalaryPay_Id),
						new DataParameter("@day", day.Date));
				}

				var work = workedDay.Where(d => d.IsWorked).ToArray();
				foreach (var day in work)
				{
					db.Execute(Resources.InsertWorkDay,
						new DataParameter("@payId", day.SalaryPay_Id),
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
		public T[] GetTable<T>() where T : Dto
		{
			using (var db = new DbContext(_connectionString))
			{
				return db.GetTable<T>().ToArray();
			}
		}
	}
}
