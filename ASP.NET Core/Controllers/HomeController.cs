using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.Models.Home;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Furmanov.Controllers
{
	[Authorize]
	[ReportBug]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserContext _db;
		private readonly IMainModel _model;
		private MainViewModel _mainViewModel;

		public HomeController(IConfiguration config,
			ILogger<HomeController> logger,
			UserContext context)
		{
			_logger = logger;
			_db = context;

			var connectionString = config.GetConnectionString("DefaultConnection");
			var resolver = IoCBuilder.Build(connectionString);

			_model = resolver.Resolve<IMainModel>();
			_model.Updated += (sender, vm) => _mainViewModel = vm;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string message = null)
		{
			await Authorize();
			_model.Update();
			ViewBag.Message = message;
			return View(_mainViewModel);
		}

		public IActionResult RefillDataBase()
		{
			//_model.RefillDataBase();
			return RedirectToAction(nameof(Index), new { message = "Данные успешно сгенерированы" });
		}
		[HttpGet]
		public IActionResult ReportBugTest()
		{
			var rnd = new Random();
			var r = rnd.Next(1000);
			if (r < 100) throw new ArgumentException("Test");
			if (r < 200) throw new AggregateException("Test");
			if (r < 300) throw new ApplicationException("Test");
			if (r < 400) throw new ArgumentOutOfRangeException("Test");
			if (r < 500) throw new COMException("Test");
			if (r < 600) throw new BadImageFormatException("Test");
			if (r < 700) throw new DataException("Test");
			if (r < 800) throw new DuplicateWaitObjectException("Test");
			if (r < 900) throw new DivideByZeroException("Test");
			throw new Exception("Test");
		}
		[HttpPost]
		public async Task<IActionResult> ChangeMonth(string json)
		{
			await Authorize();

			var vm = JsonService.FromJson<ChangeMonthViewModel>(json);
			var month = DateTime.Parse(vm.Month);
			_model.ChangeMonth(month.Year, month.Month);
			foreach (var p in _mainViewModel.SalaryPays)
			{
				p.IsExpanded = vm.ExpandList.Contains(p.ViewModelId.ToId());
			}
			ViewBag.SelectedRow = vm.SelectedRow;

			return PartialView("_SalaryPays", _mainViewModel);
		}
		[HttpGet]
		public IActionResult _WorkedDays(int payId)
		{
			var workedDays = _model.GetWorkedDays(payId);
			return PartialView(workedDays);
		}
		[HttpPost]
		public async Task<IActionResult> SaveWorkedDay(string json)
		{
			await Authorize();
			var vm = JsonService.FromJson<SaveWorkedDayViewModel>(json);
			vm.WorkedDay.Date = DateTime.Parse(vm.WorkedDay.DateJson);
			_model.SaveWorkedDays(vm.WorkedDay);
			foreach (var p in _mainViewModel.SalaryPays)
			{
				p.IsExpanded = vm.ExpandList.Contains(p.ViewModelId.ToId());
			}
			ViewBag.SelectedRow = vm.SelectedRow;

			return PartialView("_SalaryPays", _mainViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> SaveWorkedDays(string json)
		{
			await Authorize();
			var vm = JsonService.FromJson<SaveWorkedDaysViewModel>(json);
			var payIdTxt = vm.SelectedRow.Replace("Salary_", "");
			var payId = int.Parse(payIdTxt);
			var days = _model.GenWorkedDays(payId, vm.AllDays, vm.IsExist);
			_model.SaveWorkedDays(days);
			foreach (var p in _mainViewModel.SalaryPays)
			{
				p.IsExpanded = vm.ExpandList.Contains(p.ViewModelId.ToId());
			}
			ViewBag.SelectedRow = vm.SelectedRow;

			return PartialView("_SalaryPays", _mainViewModel);
		}


		private async Task Authorize()
		{
			var user = await _db.GetUserAsync(User);
			_model.UpdateLogin(user);
		}

		/// <summary>Name of Controller without "Controller"</summary>
		public static string Name => typeof(HomeController).Name.Replace("Controller", "");
	}
}
