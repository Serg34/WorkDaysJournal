using Furmanov.Dal;
using Furmanov.Dal.Dto;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView.ViewModels;
using Services;
using Services.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Furmanov.MVP.MainView
{
	#region IMainModel
	public interface IMainModel
	{
		event EventHandler<UserViewModel> LoginChanged;
		event EventHandler<MainViewModel> SalaryPayUpdated;
		event EventHandler<List<WorkedDayViewModel>> SelectedSalaryPay;
		event EventHandler<string> Error;

		DateTime Month { get; }
		void Reload();
		void ChangeMonth(DateTime month);
		void SaveSalaryPay(SalaryPayViewModel viewModel);
		void SelectSalaryPay(SalaryPayViewModel vm);

		string CurrentEmployeeName { get; }
		SalaryPayViewModel CurrentPay { get; set; }
		List<WorkedDayViewModel> CurrentTables { get; }
		void WorkDaysOnly();
		void AllDays();
		void DeleteAllDays();
		void CreateVedomost(int objectId);
		List<WorkedDayViewModel> GenWorkedDays(bool allDays, bool isExist);
		void SaveWorkDay(WorkedDayViewModel day);
		void SaveWorkedDays(List<WorkedDayViewModel> days);

		ILoginModel LoginModel { get; }

		void Logout();
	}
	#endregion

	public class MainModel : IMainModel
	{
		#region Fields
		private readonly IDataAccessService _db;
		private UserViewModel _user;

		private List<SalaryPayViewModel> _allPays;
		public SalaryPayViewModel CurrentPay { get; set; }
		#endregion

		public MainModel(IDataAccessService dataAccessService, ILoginModel loginModel)
		{
			_db = dataAccessService;
			LoginModel = loginModel;
			LoginModel.Logged += (sender, args) => UpdateLogin();
		}

		#region Events
		public event EventHandler<UserViewModel> LoginChanged;
		public event EventHandler<MainViewModel> SalaryPayUpdated;
		public event EventHandler<List<WorkedDayViewModel>> SelectedSalaryPay;
		public event EventHandler<string> Error;
		#endregion

		#region Login
		public ILoginModel LoginModel { get; }
		public void Logout()
		{
			ApplicationUser.Instance.User = null;
			UpdateLogin();
		}
		private void UpdateLogin()
		{
			_user = ApplicationUser.Instance.User;
			Reload();
			LoginChanged?.Invoke(this, _user); //после обновления восстанавливаем состояние контролов
		}
		#endregion

		#region TopMenu
		public DateTime Month { get; private set; } = DateTime.Now;

		public void ChangeMonth(DateTime month)
		{
			Month = month;
			Reload();
		}
		public void WorkDaysOnly() => GenWorkedDays(false, true);
		public void AllDays() => GenWorkedDays(true, true);
		public void DeleteAllDays() => GenWorkedDays(true, false);
		public void CreateVedomost(int objectId)
		{
			try
			{
				var dt = _db.Report(_user, objectId);
				new Excel().CreateVedomost(dt);
			}
			catch (Exception ex) { Error?.Invoke(this, ex.ToString()); }
		}
		#endregion

		public void Reload()
		{
			try
			{
				_user = ApplicationUser.Instance.User;

				_allPays = _db.GetSalaryPays(_user, Month);

				SalaryPayUpdated?.Invoke(this, GetMainVieModels());
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex.ToString());
			}
		}

		#region SalaryPays
		public void SaveSalaryPay(SalaryPayViewModel pay)
		{
			CurrentPay = pay;
			CalculateAndSaveSalaryPay(CurrentPay);
			Reload();
		}

		public void SelectSalaryPay(SalaryPayViewModel pay)
		{
			try
			{
				CurrentPay = pay.Type != ObjType.Salary ? null
					: _allPays.FirstOrDefault(p => p.Id == pay.Id);

				CurrentEmployeeName = CurrentPay?.Name;

				var days = _db.GetWorkedDays(pay.Id, Month);
				CurrentTables = Month.AllDaysInMonth()
					.Select(date => new WorkedDayViewModel
					{
						SalaryPayId = CurrentPay?.Id ?? -1,
						Date = date,
						IsWorked = days.Any(t => t.Date.Date == date.Date)
					}).ToList();

				SelectedSalaryPay?.Invoke(this,
					pay.Type == ObjType.Salary ? CurrentTables : new List<WorkedDayViewModel>());
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex.ToString());
			}
		}
		#endregion
		public string CurrentEmployeeName { get; private set; }

		#region WorkedDays
		public List<WorkedDayViewModel> CurrentTables { get; private set; }


		public void SaveWorkDay(WorkedDayViewModel day)
		{
			day.IsWorked = !day.IsWorked; // данные приходят до изменений, поэтому обратное значение

			if (day.IsWorked && !_allPays.Any(pay => pay.EmployeeId == day.EmployeeId))
			{
				CreateSalaryPayBeforeCreatingWorkedDay(day);
				day.SalaryPayId = CurrentPay?.Id ?? -1;
			}

			_db.SaveWorkedDays(day);
			CurrentPay = _allPays.First(p => p.Id == day.SalaryPayId);
			CalculateAndSaveSalaryPay(CurrentPay);
			Reload();
		}
		private void CreateSalaryPayBeforeCreatingWorkedDay(WorkedDayViewModel day = null)
		{
			if (day != null) CurrentPay = new SalaryPayViewModel
			{
				Month = Month,
				ObjectId = day.ObjectId,
				EmployeeId = day.EmployeeId,
			};

			_db.SaveSalaryPay(CurrentPay);
			Reload();
		}

		public List<WorkedDayViewModel> GenWorkedDays(bool allDays, bool isExist)
		{
			if (!_allPays.Any(res => res.EmployeeId == CurrentPay.EmployeeId))
			{
				CreateSalaryPayBeforeCreatingWorkedDay();
			}

			var tabels = Month.AllDaysInMonth()
				.Select(date => new WorkedDayViewModel
				{
					SalaryPayId = CurrentPay.Id,
					Date = date,
					IsWorked = isExist && (allDays ||
							 date.DayOfWeek != DayOfWeek.Saturday &&
							 date.DayOfWeek != DayOfWeek.Sunday)
				}).ToList();
			return tabels;
		}
		public void SaveWorkedDays(List<WorkedDayViewModel> days)
		{
			_db.SaveWorkedDays(days.ToArray());
			CalculateAndSaveSalaryPay(CurrentPay);
			Reload();
		}
		#endregion

		private void CalculateAndSaveSalaryPay(SalaryPayViewModel salaryPay)
		{
			var workDays = _db.GetWorkedDays(salaryPay.Id, Month);
			CurrentTables = Month.AllDaysInMonth()
				.Select(date => new WorkedDayViewModel
				{
					SalaryPayId = CurrentPay.Id,
					Date = date,
					IsWorked = workDays.Any(t => t.Date.Date == date.Date)
				}).ToList();

			salaryPay.FactDays = Month.AllDaysInMonth()
				.Count(date => CurrentTables
					.Any(t => t.SalaryPayId == salaryPay.Id &&
							  t.Date.Date == date.Date &&
							  t.IsWorked));

			if (salaryPay.FactDays != null && salaryPay.RateDays > 0)
			{
				//ЗП = Оклад / Норма * Факт - Аванс - Штрафы + Премии;

				salaryPay.SalaryPay =
					salaryPay.Salary / salaryPay.RateDays * salaryPay.FactDays
					- (salaryPay.Advance ?? 0)
					- (salaryPay.Penalty ?? 0)
					+ (salaryPay.Premium ?? 0);
			}
			else salaryPay.SalaryPay = 0;

			_db.SaveSalaryPay(salaryPay);
		}

		#region Get ViewModels
		private MainViewModel GetMainVieModels()
		{
			var vm = new MainViewModel
			{
				User = _user,
				Month = Month,

				SalaryPays = _db.GetSalaryPays(_user, Month),
				WorkedDays = CurrentTables,
			};
			return vm;
		}
		#endregion
	}
}
