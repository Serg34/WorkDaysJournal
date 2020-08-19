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
		event EventHandler<UserDto> LoginChanged;
		event EventHandler<MainViewModel> Updated;
		event EventHandler<List<WorkedDay>> SelectedSalaryPay;
		event EventHandler<ProgressEventArgs> Progress;
		event EventHandler<BugEventArgs> ReportingBug;

		ILoginModel LoginModel { get; }
		void UpdateLogin(UserDto user);

		UserDto User { get; }
		int Year { get; }
		int Month { get; }
		string CurrentEmployeeName { get; }

		void Update();
		void RefillDataBase();
		void ChangeMonth(int year, int month);
		void SaveSalaryPay(SalaryPay viewModel);
		void SelectSalaryPay(SalaryPay vm);

		void SaveWorkedDays(params WorkedDay[] days);
		WorkedDay[] GetWorkedDays(int payId);
		WorkedDay[] GenWorkedDays(int payId, bool allDays, bool isExist);

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
			LoginModel.Logged += (sender, user) => UpdateLogin(user);
		}

		#region Events
		public event EventHandler<UserDto> LoginChanged;
		public event EventHandler<MainViewModel> Updated;
		public event EventHandler<List<WorkedDay>> SelectedSalaryPay;
		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler<BugEventArgs> ReportingBug;
		#endregion

		#region Login
		public ILoginModel LoginModel { get; }
		public void UpdateLogin(UserDto user)
		{
			User = user;
			Update();
			LoginChanged?.Invoke(this, User); //после обновления восстанавливаем состояние контролов
		}
		#endregion

		#region Properties
		public UserDto User { get; private set; }
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
				var vm = GetMainVieModels();
				Updated?.Invoke(this, vm);
			}
			catch (Exception ex)
			{
				ReportBug(this, new BugEventArgs(ex, User));
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
				CurrentEmployeeName = pay.Type == ObjType.SalaryPay ? pay.Name : null;

				var days = _db.GetWorkedDays(pay.Id);
				var currentDaysInMonth = DateService.AllDaysInMonth(Year, Month)
					.Select(date => new WorkedDay
					{
						SalaryPay_Id = pay.Id,
						Date = date,
						IsWorked = days.Any(t => t.Date.Date == date.Date)
					}).ToList();

				SelectedSalaryPay?.Invoke(this,
					pay.Type == ObjType.SalaryPay ? currentDaysInMonth : new List<WorkedDay>());
			}
			catch (Exception ex)
			{
				ReportBug(this, new BugEventArgs(ex, User));
			}
		}
		#endregion

		#region WorkedDays
		public void SaveWorkedDays(params WorkedDay[] days)
		{
			Year = days[0].Date.Year;
			Month = days[0].Date.Month;

			var pay = _db.GetSalaryPay(days[0].SalaryPay_Id);
			_db.SaveWorkedDays(days);
			CalculateAndSaveSalaryPay(pay);
			Update();
		}
		public WorkedDay[] GetWorkedDays(int payId)
		{
			var pay = _db.GetSalaryPay(payId);
			if (pay == null) return new WorkedDay[0];

			Year = pay.Year;
			Month = pay.Month;

			var workedDays = _db.GetWorkedDays(payId);
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
			var pay = _db.GetSalaryPay(payId);
			if (pay == null) return new WorkedDay[0];

			Year = pay.Year;
			Month = pay.Month;

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
		#endregion

		#region CalculateAndSaveSalaryPay
		private void CalculateAndSaveSalaryPay(SalaryPay pay)
		{
			var workDays = _db.GetWorkedDays(pay.Id);
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
				Year = Year,
				Month = Month,

				SalaryPays = _db.GetSalaryPays(User, Year, Month),
			};
			return vm;
		}
		#endregion

		public void ReportBug(object sender, BugEventArgs e)
		{
			using (var dbContext = _db.GetDbContext())
			{
				var bug = BugReporter.Report(dbContext, e.Exception, e.User, e.InfoForDeveloper);
				if (bug != null)
				{
					e.Bug = bug;
					ReportingBug?.Invoke(this, e);
				}
			}
		}
	}
}
