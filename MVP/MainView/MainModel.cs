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
		event EventHandler<Exception> Error;

		ILoginModel LoginModel { get; }
		void Logout();

		int Year { get; }
		int Month { get; }
		string CurrentEmployeeName { get; }
		SalaryPay CurrentPay { get; set; }
		List<WorkedDay> CurrentDaysInMonth { get; }

		void Update();
		void ChangeMonth(int year, int month);
		void SaveSalaryPay(SalaryPay viewModel);
		void SelectSalaryPay(SalaryPay vm);

		List<WorkedDay> GenWorkedDays(bool allDays, bool isExist);
		void SaveWorkDay(WorkedDay day);
		void SaveWorkedDays(List<WorkedDay> days);
	}
	#endregion

	public class MainModel : IMainModel
	{
		#region Fields
		private readonly IDataAccessService _db;
		private User _user;

		private List<SalaryPay> _allPays;
		#endregion

		public MainModel(IDataAccessService dataAccessService, ILoginModel loginModel)
		{
			_db = dataAccessService;
			LoginModel = loginModel;
			LoginModel.Logged += (sender, args) => UpdateLogin();

			using (var dc = _db.GetDataContext())
			{
				//new DataGenerator().CreateDataBase(dc);
				new DataGenerator().RefillDataBase(dc);
			}
		}

		#region Events
		public event EventHandler<User> LoginChanged;
		public event EventHandler<MainViewModel> Updated;
		public event EventHandler<List<WorkedDay>> SelectedSalaryPay;
		public event EventHandler<Exception> Error;
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
			_user = ApplicationUser.User;
			Update();
			LoginChanged?.Invoke(this, _user); //после обновления восстанавливаем состояние контролов
		}
		#endregion

		#region Properties
		public int Year { get; private set; } = 2019;
		public int Month { get; private set; } = 1;
		public string CurrentEmployeeName { get; private set; }
		public SalaryPay CurrentPay { get; set; }
		public List<WorkedDay> CurrentDaysInMonth { get; private set; }
		#endregion

		#region TopMenu
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
				_user = ApplicationUser.User;

				_allPays = _db.GetSalaryPays(_user, Year, Month);

				Updated?.Invoke(this, GetMainVieModels());
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex);
			}
		}
		#endregion

		#region SalaryPays
		public void SaveSalaryPay(SalaryPay pay)
		{
			CurrentPay = pay;
			CalculateAndSaveSalaryPay(CurrentPay);
			Update();
		}
		public void SelectSalaryPay(SalaryPay pay)
		{
			try
			{
				CurrentPay = pay.Type != ObjType.Salary ? null
					: _allPays.FirstOrDefault(p => p.Id == pay.Id);

				CurrentEmployeeName = CurrentPay?.Name;

				var days = _db.GetWorkedDays(pay.Id, Year, Month);
				CurrentDaysInMonth = DateService.AllDaysInMonth(Year, Month)
					.Select(date => new WorkedDay
					{
						SalaryPay_Id = CurrentPay?.Id ?? -1,
						Date = date,
						IsWorked = days.Any(t => t.Date.Date == date.Date)
					}).ToList();

				SelectedSalaryPay?.Invoke(this,
					pay.Type == ObjType.Salary ? CurrentDaysInMonth : new List<WorkedDay>());
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex);
			}
		}
		#endregion

		#region WorkedDays
		public void SaveWorkDay(WorkedDay day)
		{
			day.IsWorked = !day.IsWorked; // данные приходят до изменений, поэтому обратное значение

			if (day.IsWorked && !_allPays.Any(pay => pay.Employee_Id == day.Employee_Id))
			{
				CreateSalaryPayBeforeCreatingWorkedDay(day);
				day.SalaryPay_Id = CurrentPay?.Id ?? -1;
			}

			_db.SaveWorkedDays(day);
			CurrentPay = _allPays.First(p => p.Id == day.SalaryPay_Id);
			CalculateAndSaveSalaryPay(CurrentPay);
			Update();
		}
		private void CreateSalaryPayBeforeCreatingWorkedDay(WorkedDay day = null)
		{
			if (day != null) CurrentPay = new SalaryPay
			{
				Month = Month,
				Object_Id = day.Object_Id,
				Employee_Id = day.Employee_Id,
			};

			_db.SaveSalaryPay(CurrentPay);
			Update();
		}

		public List<WorkedDay> GenWorkedDays(bool allDays, bool isExist)
		{
			if (!_allPays.Any(res => res.Employee_Id == CurrentPay.Employee_Id))
			{
				CreateSalaryPayBeforeCreatingWorkedDay();
			}

			var days = DateService.AllDaysInMonth(Year, Month)
				.Select(date => new WorkedDay
				{
					SalaryPay_Id = CurrentPay.Id,
					Date = date,
					IsWorked = isExist && (allDays ||
							 date.DayOfWeek != DayOfWeek.Saturday &&
							 date.DayOfWeek != DayOfWeek.Sunday)
				}).ToList();
			return days;
		}
		public void SaveWorkedDays(List<WorkedDay> days)
		{
			_db.SaveWorkedDays(days.ToArray());
			CalculateAndSaveSalaryPay(CurrentPay);
			Update();
		}
		#endregion

		#region CalculateAndSaveSalaryPay
		private void CalculateAndSaveSalaryPay(SalaryPay salaryPay)
		{
			var workDays = _db.GetWorkedDays(salaryPay.Id, Year, Month);
			CurrentDaysInMonth = DateService.AllDaysInMonth(Year, Month)
				.Select(date => new WorkedDay
				{
					SalaryPay_Id = CurrentPay.Id,
					Date = date,
					IsWorked = workDays.Any(t => t.Date.Date == date.Date)
				}).ToList();

			salaryPay.FactDays = DateService.AllDaysInMonth(Year, Month)
				.Count(date => CurrentDaysInMonth
					.Any(t => t.SalaryPay_Id == salaryPay.Id &&
							  t.Date.Date == date.Date &&
							  t.IsWorked));

			if (salaryPay.FactDays != null && salaryPay.RateDays > 0)
			{
				//ЗП = Оклад / Норма * Факт - Аванс - Штрафы + Премии;

				salaryPay.SalaryToPay =
					salaryPay.Salary / salaryPay.RateDays * salaryPay.FactDays
					- (salaryPay.Advance ?? 0)
					- (salaryPay.Penalty ?? 0)
					+ (salaryPay.Premium ?? 0);
			}
			else salaryPay.SalaryToPay = null;

			_db.SaveSalaryPay(salaryPay);
		}
		#endregion

		#region Get ViewModels
		private MainViewModel GetMainVieModels()
		{
			var vm = new MainViewModel
			{
				User = _user,
				Year = Year,
				Month = Month,

				SalaryPays = _db.GetSalaryPays(_user, Year, Month),
				WorkedDays = CurrentDaysInMonth,
			};
			return vm;
		}
		#endregion
	}
}
