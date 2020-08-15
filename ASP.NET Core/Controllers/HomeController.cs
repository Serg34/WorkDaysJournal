using Furmanov.Data.Data;
using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.MVP.MainView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Furmanov.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserContext _db;
		private readonly IMainModel _model;
		private MainViewModel _mainViewModel;
		private List<WorkedDay> _workedDays;

		public HomeController(IConfiguration config, UserContext context)
		{
			_db = context;

			var connectionString = config.GetConnectionString("DefaultConnection");
			var resolver = IoCBuilder.Build(connectionString);

			_model = resolver.Resolve<IMainModel>();

			_model.Updated += (sender, vm) => _mainViewModel = vm;
			_model.SelectedSalaryPay += (sender, days) => _workedDays = days;
		}

		public async Task<IActionResult> Index(int year = 2019, int month = 1)
		{
			_model.ChangeMonth(year, month);
			var user = await _db.GetUserAsync(User);
			_mainViewModel.User = user;
			return View(_mainViewModel);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		/// <summary>Name of Controller without "Controller"</summary>
		public static string Name => typeof(HomeController).Name.Replace("Controller", "");
	}
}
