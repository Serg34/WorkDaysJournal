using Furmanov.Data;
using Furmanov.Data.Data;
using Furmanov.MVP.Login;
using Furmanov.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Furmanov.MVP.MainView
{
	#region IMainModel
	public interface IMainModel
	{
		event EventHandler<User> LoginChanged;
		event EventHandler<MainViewModel> Updated;
		event EventHandler<List<WorkedDay>> SelectedSalaryPay;
		event EventHandler<ProgressEventArgs> Progress;
		event EventHandler<BugEventArgs> ReportingBug;

		ILoginModel LoginModel { get; }
		void Logout();

		int Year { get; }
		int Month { get; }
		string CurrentEmployeeName { get; }

		void Update();
		void RefillDataBase();
		void ChangeMonth(int year, int month);
		void SaveSalaryPay(SalaryPay viewModel);
		void SelectSalaryPay(SalaryPay vm);

		WorkedDay[] GetWorkedDays(int payId);
		WorkedDay[] GenWorkedDays(int payId, bool allDays, bool isExist);
		void SaveWorkDay(WorkedDay day);
		void SaveWorkedDays(WorkedDay[] days);

		void ReportBug(object sender, BugEventArgs e);

	}
	#endregion

	public class MainModel : IMainModel
	{
		private readonly IDataAccessService _db;
		public MainModel(IDataAccessService dataAccessService, ILoginModel loginModel)
		{
			_db = dataAccessService;
			LoginModel = loginModel;
			LoginModel.Logged += (sender, args) => UpdateLogin();
		}

		#region Events
		public event EventHandler<User> LoginChanged;
		public event EventHandler<MainViewModel> Updated;
		public event EventHandler<List<WorkedDay>> SelectedSalaryPay;
		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler<BugEventArgs> ReportingBug;
		#endregion

		#region Login
		public ILoginModel LoginModel { get; }
		public void Logout()
		{
			ApplicationUser.User = null;
			UpdateLogin();
		}
		private void UpdateLogin()
		{
			Update();
			LoginChanged?.Invoke(this, ApplicationUser.User); //после обновления восстанавливаем состояние контролов
		}
		#endregion

		#region Properties
		public int Year { get; private set; } = 2019;
		public int Month { get; private set; } = 1;
		public string CurrentEmployeeName { get; private set; }
		#endregion

		#region TopMenu

		public void RefillDataBase()
		{
			using (var dbContext = _db.GetDbContext())
			{
				var generator = new DataGenerator();
				generator.Progress += Progress;
				generator.RefillDataBase(dbContext);
			}
			Update();
		}

		public void ChangeMonth(int year, int month)
		{
			Year = year;
			Month = month;
			Update();
		}
		#endregion

		#region Update
		public void Update()
		{
			try
			{
				Updated?.Invoke(this, GetMainVieModels());
			}
			catch (Exception ex)
			{
				ReportBug(this, new BugEventArgs(ex));
			}
		}
		#endregion

		#region SalaryPays
		public void SaveSalaryPay(SalaryPay pay)
		{
			CalculateAndSaveSalaryPay(pay);
			Update();
		}
		public void SelectSalaryPay(SalaryPay pay)
		{
			try
			{
				CurrentEmployeeName = pay.Type == ObjType.Salary ? pay.Name : null;

				var days = _db.GetWorkedDays(pay.Id, Year, Month);
				var currentDaysInMonth = DateService.AllDaysInMonth(Year, Month)
					.Select(date => new WorkedDay
					{
						SalaryPay_Id = pay.Id,
						Date = date,
						IsWorked = days.Any(t => t.Date.Date == date.Date)
					}).ToList();

				SelectedSalaryPay?.Invoke(this,
					pay.Type == ObjType.Salary ? currentDaysInMonth : new List<WorkedDay>());
			}
			catch (Exception ex)
			{
				ReportBug(this, new BugEventArgs(ex));
			}
		}
		#endregion

		#region WorkedDays
		public void SaveWorkDay(WorkedDay day)
		{
			day.IsWorked = !day.IsWorked; // данные приходят до изменений, поэтому обратное значение
			_db.SaveWorkedDays(day);
			var pay = _db.GetSalaryPay(day.SalaryPay_Id);
			CalculateAndSaveSalaryPay(pay);
			Update();
		}

		public WorkedDay[] GetWorkedDays(int payId)
		{
			var workedDays = _db.GetWorkedDays(payId, Year, Month);
			var days = DateService.AllDaysInMonth(Year, Month)
				.Select(date => new WorkedDay
				{
					SalaryPay_Id = payId,
					Date = date,
					IsWorked = workedDays.Any(d => d.Date.Date == date.Date)
				}).ToArray();
			return days;
		}

		public WorkedDay[] GenWorkedDays(int payId, bool allDays, bool isExist)
		{
			var days = DateService.AllDaysInMonth(Year, Month)
				.Select(date => new WorkedDay
				{
					SalaryPay_Id = payId,
					Date = date,
					IsWorked = isExist && (allDays ||
							 date.DayOfWeek != DayOfWeek.Saturday &&
							 date.DayOfWeek != DayOfWeek.Sunday)
				}).ToArray();
			return days;
		}
		public void SaveWorkedDays(WorkedDay[] days)
		{
			_db.SaveWorkedDays(days);
			var pay = _db.GetSalaryPay(days[0].SalaryPay_Id);
			CalculateAndSaveSalaryPay(pay);
			Update();
		}
		#endregion

		#region CalculateAndSaveSalaryPay
		private void CalculateAndSaveSalaryPay(SalaryPay pay)
		{
			var workDays = _db.GetWorkedDays(pay.Id, Year, Month);
			var currentDaysInMonth = DateService.AllDaysInMonth(Year, Month)
				.Select(date => new WorkedDay
				{
					SalaryPay_Id = pay.Id,
					Date = date,
					IsWorked = workDays.Any(t => t.Date.Date == date.Date)
				}).ToArray();

			pay.FactDays = DateService.AllDaysInMonth(Year, Month)
				.Count(date => currentDaysInMonth
					.Any(t => t.SalaryPay_Id == pay.Id &&
							  t.Date.Date == date.Date &&
							  t.IsWorked));

			var baseSalary = pay.FactDays == null || pay.RateDays == 0 ? 0
				: pay.Salary / pay.RateDays * pay.FactDays;

			//ЗП = Оклад / Норма * Факт - Аванс - Штрафы + Премии;
			pay.SalaryToPay = baseSalary
				- (pay.Advance ?? 0)
				- (pay.Penalty ?? 0)
				+ (pay.Premium ?? 0);

			_db.SaveSalaryPay(pay);
		}
		#endregion

		#region Get ViewModels
		private MainViewModel GetMainVieModels()
		{
			var vm = new MainViewModel
			{
				User = ApplicationUser.User,
				Year = Year,
				Month = Month,

				SalaryPays = _db.GetSalaryPays(ApplicationUser.User, Year, Month),
			};
			return vm;
		}
		#endregion

		public void ReportBug(object sender, BugEventArgs e)
		{
			using (var dbContext = _db.GetDbContext())
			{
				var bug = BugReporter.Report(dbContext, e.Exception, e.InfoForDeveloper);
				if (bug != null)
				{
					e.Bug = bug;
					ReportingBug?.Invoke(this, e);
				}
			}
		}
	}
}
