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
		event EventHandler<UserVisual> LoginChanged;
		event EventHandler<MainViewModel> SalaryPayUpdated;
		event EventHandler<List<WorkedDayVisual>> SelectedSalaryPay;
		event EventHandler<string> Error;

		DateTime Month { get; }
		void Reload();
		void ChangeMonth(DateTime month);
		void SaveResOp(SalaryPayVisual viewModel);
		void SelectSalaryPay(SalaryPayVisual vm);

		string CurrentEmployeeName { get; }
		List<WorkedDayVisual> CurrentTables { get; }
		void SaveWorkDays(WorkedDayVisual viewModel);
		void CreateWorkDaysOnlyTabels();
		void CreateAllDaysTabels();
		void DeleteAllDaysTabels();
		void CreateVedomost(int objectId);
		List<WorkedDayVisual> GenWorkedDays(bool allDays, bool isExist);
		void SaveWorkedDays(List<WorkedDayVisual> workedDays);

		ILoginModel LoginModel { get; }

		void Logout();
	}
	#endregion

	public class MainModel : IMainModel
	{
		#region Fields
		private readonly IDataAccessService _db;
		private UserVisual _user;

		private List<SalaryPayVisual> _allResOps;
		private SalaryPayVisual _currentResOp;
		#endregion

		public MainModel(IDataAccessService dataAccessService, ILoginModel loginModel)
		{
			_db = dataAccessService;
			LoginModel = loginModel;
			LoginModel.Logged += (sender, args) => UpdateLogin();
		}

		#region Events
		public event EventHandler<UserVisual> LoginChanged;
		public event EventHandler<MainViewModel> SalaryPayUpdated;
		public event EventHandler<List<WorkedDayVisual>> SelectedSalaryPay;
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
		public void CreateWorkDaysOnlyTabels() => GenWorkedDays(false, true);
		public void CreateAllDaysTabels() => GenWorkedDays(true, true);
		public void DeleteAllDaysTabels() => GenWorkedDays(true, false);
		public void CreateVedomost(int objectId)
		{
			try
			{
				var dt = _db.GetVedomost(_user, objectId);
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

				_allResOps = _db.GetSalaryPayVisuals(_user, Month);

				SalaryPayUpdated?.Invoke(this, GetMainVieModels());
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex.ToString());
			}
		}

		#region SalaryPays
		public void SaveResOp(SalaryPayVisual vm)
		{
			_currentResOp = vm;
			CalculateAndSaveSalaryPay(_currentResOp);
			Reload();
		}

		public void SelectSalaryPay(SalaryPayVisual vm)
		{
			try
			{
				_currentResOp = vm.Type != ObjType.Salary ? null
					: _allResOps.FirstOrDefault(resOp => resOp.Id == vm.Id);

				var tabelsDb = _db.GetTables(vm.Id, Month);
				CurrentTables = Month.AllDaysInMonth()
					.Select(date => new WorkedDayVisual
					{
						SalaryPayId = _currentResOp.Id,
						Date = date,
						IsWorked = tabelsDb.Any(t => t.Date.Date == date.Date)
					}).ToList();

				SelectedSalaryPay?.Invoke(this, CurrentTables);
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex.ToString());
			}
		}
		#endregion

		#region WorkedDays
		public List<WorkedDayVisual> CurrentTables { get; private set; }

		public string CurrentEmployeeName { get; private set; }

		public void SaveWorkDays(WorkedDayVisual day)
		{
			day.IsWorked = !day.IsWorked; // данные приходят до изменений, поэтому обратное значение

			if (day.IsWorked && !_allResOps.Any(resOp => resOp.EmployeeId == day.EmployeeId))
			{
				CreateSalaryPayBeforeCreatingWorkedDay(day);
				day.SalaryPayId = _currentResOp?.Id ?? -1;
			}

			_db.SaveTables(day);
			_currentResOp = _allResOps.First(resOp => resOp.Id == day.SalaryPayId);
			CalculateAndSaveSalaryPay(_currentResOp);
			Reload();
		}
		private void CreateSalaryPayBeforeCreatingWorkedDay(WorkedDayVisual day = null)
		{
			if (day != null) _currentResOp = new SalaryPayVisual
			{
				Month = Month,
				ObjectId = day.ObjectId,
				EmployeeId = day.EmployeeId,
			};

			_db.SaveSalaryPay(_currentResOp);
			Reload();
		}

		public List<WorkedDayVisual> GenWorkedDays(bool allDays, bool isExist)
		{
			if (!_allResOps.Any(res => res.EmployeeId == _currentResOp.EmployeeId))
			{
				CreateSalaryPayBeforeCreatingWorkedDay();
			}

			var tabels = Month.AllDaysInMonth()
				.Select(date => new WorkedDayVisual
				{
					SalaryPayId = _currentResOp.Id,
					Date = date,
					IsWorked = isExist && (allDays ||
							 date.DayOfWeek != DayOfWeek.Saturday &&
							 date.DayOfWeek != DayOfWeek.Sunday)
				}).ToList();
			return tabels;
		}
		public void SaveWorkedDays(List<WorkedDayVisual> workedDays)
		{
			_db.SaveTables(workedDays.ToArray());
			CalculateAndSaveSalaryPay(_currentResOp);
			Reload();
		}
		#endregion

		private void CalculateAndSaveSalaryPay(SalaryPayVisual salaryPay)
		{
			var workDays = _db.GetTables(salaryPay.Id, Month);
			CurrentTables = Month.AllDaysInMonth()
				.Select(date => new WorkedDayVisual
				{
					SalaryPayId = _currentResOp.Id,
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

				SalaryPays = _db.GetSalaryPayVisuals(_user, Month),
				WorkedDays = CurrentTables,
			};
			return vm;
		}
		#endregion
	}
}
