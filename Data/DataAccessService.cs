using Furmanov.Dal;
using Furmanov.Data.Data;
using Furmanov.Data.Properties;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Furmanov.Data
{
	#region IDataAccessService
	public interface IDataAccessService
	{
		DbContext GetDbContext();
		List<LoginPassword> GetAutoLoginPassword();
		void SaveAutoLoginPassword(LoginPassword loginPassword);
		void DeleteAutoLogin(string login);
		User GetUser(string login, string password);

		SalaryPay[] GetSalaryPays(User user, int year, int month);
		SalaryPay GetSalaryPay(int payId);
		void SaveSalaryPay(SalaryPayDto salaryPayDto);

		WorkedDayDto[] GetWorkedDays(int salaryPayId, int year, int month);
		void SaveWorkedDays(params WorkedDay[] workedDayDb);
		void Insert<T>(T obj);
		T[] GetTable<T>() where T : Dto;
	}
	#endregion

	public class DataAccessService : IDataAccessService
	{
		private readonly string _connectionString;
		private readonly IRepository<List<LoginPassword>> _loginPasswordRepository;

		public DataAccessService(string connectionString,
			IRepository<List<LoginPassword>> loginPasswordRepository)
		{
			_connectionString = connectionString;
			_loginPasswordRepository = loginPasswordRepository;
		}
		public DbContext GetDbContext() => new DbContext(_connectionString);
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
		public List<LoginPassword> GetAutoLoginPassword()
		{
			var res = _loginPasswordRepository.Load();
#if DEBUG
			if (res == null) res = new List<LoginPassword>();
			void Add(string login)
			{
				if (res.Any(l => l.Login == login)) return;
				res.Add(new LoginPassword(login, "1"));
			}
			Add("Admin");
			Add("ProjectManager");
			Add("Manager");
#endif
			return res;
		}
		public void SaveAutoLoginPassword(LoginPassword loginPassword)
		{
			var loginPasswords = new List<LoginPassword> { loginPassword }; //Текущий логин должен быть первым
			loginPasswords.AddRange(_loginPasswordRepository.Load()
					.Where(u => u.Login != loginPassword.Login));
			_loginPasswordRepository.Save(loginPasswords);
		}
		public void DeleteAutoLogin(string login)
		{
			var users = _loginPasswordRepository.Load()
				.Where(u => !u.Login.Equals(login, StringComparison.CurrentCultureIgnoreCase))
				.ToList();
			_loginPasswordRepository.Save(users);
		}

		public SalaryPay[] GetSalaryPays(User user, int year, int month)
		{
			if (user == null) return new SalaryPay[0];
			var userId = user.Role_Id == Role.Admin || user.Role_Id == Role.Director ? 0 : user.Id;

			using (var db = new DbContext(_connectionString))
			{
				var sql = user.Role_Id == Role.Manager ? Resources.SalaryPaysForManager
					: Resources.SalaryPaysForProjectManager;

				var res = db.Query<SalaryPay>(sql,
					new DataParameter("@userId", userId),
					new DataParameter("@year", year),
					new DataParameter("@month", month))
					.ToArray();
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

		public WorkedDayDto[] GetWorkedDays(int salaryPayId, int year, int month)
		{
			using (var db = new DbContext(_connectionString))
			{
				var res = db.GetTable<WorkedDayDto>()
						.Where(p => p.SalaryPay_Id == salaryPayId)
						.Where(p => p.Date.Year == year)
						.Where(p => p.Date.Month == month)
						.ToArray();

				return res;
			}
		}
		public void SaveWorkedDays(params WorkedDay[] workedDay)
		{
			using (var db = new DbContext(_connectionString))
			{
				var noWorkDays = workedDay.Where(d => !d.IsWorked)
					.Select(d => d.Date.Day)
					.ToArray();
				if (noWorkDays.Any())
				{
					var payId = workedDay[0].SalaryPay_Id;
					var year = workedDay[0].Date.Year;
					var month = workedDay[0].Date.Month;

					var deleteSql = Resources.DeleteWorkDay;
					deleteSql = deleteSql.Replace("@day", string.Join(",", noWorkDays));
					db.Execute(deleteSql,
						new DataParameter("@payId", payId),
						new DataParameter("@year", year),
						new DataParameter("@month", month));
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

		public SalaryPay GetSalaryPay(int payId)
		{
			using (var db = new DbContext(_connectionString))
			{
				var res = db.Query<SalaryPay>(Resources.SalaryPay,
						new DataParameter("@payId", payId))
						.FirstOrDefault();

				return res;
			}
		}
	}
}
