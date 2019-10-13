using Furmanov.Dal;
using Furmanov.Dal.Dto;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView.ViewModels;
using Furmanov.MVP.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Furmanov.MVP.MainView
{
	#region IMainModel
	public interface IMainModel
	{
		event EventHandler<User> LoginChanged;
		event EventHandler<MainViewModel> ResOpsUpdated;
		event EventHandler<SelectionResOpViewModel> SelectedResOp;
		event EventHandler<string> Error;

		DateTime Month { get; }
		void Reload();
		void ChangeMonth(DateTime month);
		void SaveResOp(ResOPViewModel viewModel);
		void ReplaceResource(ResOPViewModel vm);
		void SelectResOp(ResOPViewModel vm);
		List<CTabel> CurrentTabels { get; }
		void SaveTabel(TabelViewModel viewModel);
		void CreateWorkDaysOnlyTabels();
		void CreateAllDaysTabels();
		void DeleteAllDaysTabels();
		void CreateVedomost(int objectId);
		List<CTabel> GenTabels(bool allDays, bool isExist);
		void SaveTabels(List<CTabel> tabels);

		ICreateResourceModel GetCreateResourceModel(ResOPViewModel viewModel);
		void DeleteResOp();

		ILoginModel LoginModel { get; }
		string CurrentResourceName { get; }

		void Logout();
	}
	#endregion

	public class MainModel : IMainModel
	{
		#region Fields
		private readonly IdataAccessService _db;
		private User _user;

		private List<Position> _objPositions;
		private List<Employee> _objResources;
		private List<SalaryPayDb> _allResOps;
		private SalaryPayDb _currentResOp;
		#endregion

		public MainModel(IdataAccessService dataAccessService, ILoginModel loginModel)
		{
			_db = dataAccessService;
			LoginModel = loginModel;
			LoginModel.Logged += (sender, args) => UpdateLogin();
		}

		#region Events
		public event EventHandler<User> LoginChanged;
		public event EventHandler<MainViewModel> ResOpsUpdated;
		public event EventHandler<SelectionResOpViewModel> SelectedResOp;
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
		public ICreateResourceModel GetCreateResourceModel(ResOPViewModel viewModel)
		{
			var model = new CreateResourceModel(_db, viewModel);
			model.Changed += (sender, vm) => Reload();
			return model;
		}
		public void DeleteResOp()
		{
			_db.DeleteResOp(_currentResOp.Id);
			Reload();
		}
		public void ChangeMonth(DateTime month)
		{
			Month = month;
			Reload();
		}
		public void CreateWorkDaysOnlyTabels() => GenTabels(false, true);
		public void CreateAllDaysTabels() => GenTabels(true, true);
		public void DeleteAllDaysTabels() => GenTabels(true, false);
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

				var objects = _db.GetObjects(_user);
				var objectsId = objects?.Select(o => o.Id).ToArray();

				_allResOps = _db.GetResOps(objectsId, Month);

				ResOpsUpdated?.Invoke(this, GetMainVieModels());
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex.ToString());
			}
		}

		#region ResOps
		public void SaveResOp(ResOPViewModel vm)
		{
			_currentResOp = new SalaryPayDb
			{
				Id = vm.ResOPId ?? -1,
				Avans = vm.Avans,
				Comment = vm.Comment,
				FactDays = vm.FactDays,
				FactSalary = vm.FactSalary,
				Month = vm.Month,
				ObjectId = vm.ObjectId,
				Penalty = vm.Penalty,
				PositionId = _objPositions.FirstOrDefault(p => p.Name == vm.PositionName)?.Id ?? -1,
				Premium = vm.Premium,
				EmployeeId = vm.ResourceId,
				RateDays = vm.RateDays ?? 0,
			};
			CalculateSalaryAndSaveResOp(_currentResOp);
			Reload();
		}
		public void ReplaceResource(ResOPViewModel vm)
		{
			_currentResOp.EmployeeId = _objResources.First(r => r.Name == vm.Name).Id;
			CalculateSalaryAndSaveResOp(_currentResOp);
			Reload();
		}
		public void SelectResOp(ResOPViewModel vm)
		{
			try
			{
				_currentResOp = vm.Type != ObjType.ResOP ? null
					: _allResOps.FirstOrDefault(resOp => resOp.Id == vm.ResOPId);

				_objPositions = _db.GetPosition(vm.ObjectId);
				_objResources = _db.GetResources(SelectionResourceMode.All, vm.ObjectId);

				var tabelsDb = _db.GetTabels(vm.ResOPId, Month);
				CurrentTabels = Month.AllDaysInMonth()
					.Select(date => new CTabel
					{
						ResOPId = _currentResOp?.Id,
						Date = date,
						IsExit = tabelsDb.Any(t => t.Date?.Date == date.Date)
					}).ToList();

				SelectedResOp?.Invoke(this, GetSelectionResOpViewModel());
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex.ToString());
			}
		}
		#endregion

		#region Tabels
		public List<CTabel> CurrentTabels { get; private set; }
		public string CurrentResourceName
		{
			get => _objResources.FirstOrDefault(r => r.Id == _currentResOp?.EmployeeId)?.Name;
		}
		public void SaveTabel(TabelViewModel vm)
		{
			var tabel = new CTabel
			{
				Id = vm.Id,
				ResOPId = vm.ResOPId,
				Date = vm.Date,
				IsExit = !vm.IsExit, // данные приходят до изменений, поэтому обратное значение
			};

			if (tabel.IsExit && !_allResOps.Any(resOp => resOp.EmployeeId == vm.ResourceId))
			{
				CreateResOpBeforeCreatingTabel(vm);
				tabel.ResOPId = _currentResOp?.Id ?? -1;
			}

			_db.SaveTabels(tabel);
			_currentResOp = _allResOps.First(resOp => resOp.Id == tabel.ResOPId);
			CalculateSalaryAndSaveResOp(_currentResOp);
			Reload();
		}
		private void CreateResOpBeforeCreatingTabel(TabelViewModel vm = null)
		{
			if (vm != null) _currentResOp = new SalaryPayDb
			{
				Month = Month,
				ObjectId = vm.ObjectId,
				EmployeeId = vm.ResourceId,
			};

			if (_db.SaveSalaryPay(_currentResOp))
			{
				Reload();
			}
		}

		public List<CTabel> GenTabels(bool allDays, bool isExist)
		{
			if (!_allResOps.Any(res => res.EmployeeId == _currentResOp.EmployeeId))
			{
				CreateResOpBeforeCreatingTabel();
			}

			var tabels = Month.AllDaysInMonth()
				.Select(date => new CTabel
				{
					ResOPId = _currentResOp?.Id,
					Date = date,
					IsExit = isExist && (allDays ||
							 date.DayOfWeek != DayOfWeek.Saturday &&
							 date.DayOfWeek != DayOfWeek.Sunday)
				}).ToList();
			return tabels;
		}
		public void SaveTabels(List<CTabel> tabels)
		{
			_db.SaveTabels(tabels.ToArray());
			CalculateSalaryAndSaveResOp(_currentResOp);
			Reload();
		}
		#endregion

		private void CalculateSalaryAndSaveResOp(SalaryPayDb resOp)
		{
			var tabelsDb = _db.GetTabels(resOp.Id, Month);
			CurrentTabels = Month.AllDaysInMonth()
				.Select(date => new CTabel
				{
					ResOPId = _currentResOp.Id,
					Date = date,
					IsExit = tabelsDb.Any(t => t.Date?.Date == date.Date)
				}).ToList();

			resOp.FactDays = Month.AllDaysInMonth()
				.Count(date => CurrentTabels
					.Any(t => t.ResOPId == resOp.Id &&
							  t.Date?.Date == date.Date &&
							  t.IsExit));

			var resource = _objResources.FirstOrDefault(r => r.Id == resOp.EmployeeId);
			var position = _objPositions.FirstOrDefault(p => p.Id == resOp.PositionId);

			if (resource != null &&
				position?.Salary != null &&
				resOp.FactDays != null && resOp.RateDays > 0)
			{
				//ЗП = Оклад / Норма * Факт - Аванс - Штрафы + Премии - оф.оклад;
				resOp.FactSalary = position.Salary / resOp.RateDays * resOp.FactDays
					- (resOp.Avans ?? 0) - (resOp.Penalty ?? 0)
					+ (resOp.Premium ?? 0)
					- (resource.OfficialSalary ?? 0);
			}
			else resOp.FactSalary = 0;

			_db.SaveSalaryPay(resOp);
		}

		#region Get ViewModels
		private MainViewModel GetMainVieModels()
		{
			var vm = new MainViewModel
			{
				User = _user,
				Month = Month,

				ResOps = _db.GetResOPViewModels(_user, Month) ?? new List<ResOPViewModel>(new List<ResOPViewModel>()),

				SelectionResOp = GetSelectionResOpViewModel(),
			};
			return vm;
		}
		private SelectionResOpViewModel GetSelectionResOpViewModel() => new SelectionResOpViewModel
		{
			ResourceNames = _objResources?
				.Where(r => !_allResOps.Any(resOp => resOp.EmployeeId == r.Id))
				.Select(r => r.Name)
				.ToArray(),
			PositionNames = _objPositions?.Select(p => p.Name).ToArray(),
			Tabels = TabelViewModel.Factory(_currentResOp, Month, CurrentTabels),
		};
		#endregion
	}
}
