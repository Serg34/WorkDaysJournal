using Furmanov.Data.Data;
using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.MVP.MainView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Furmanov.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IMainModel _model;
		private MainViewModel _mainViewModel;
		private List<WorkedDay> _workedDays;

		public HomeController(IConfiguration config,
			//UserManager<AspNetApplicationUser> userManager, 
			ILogger<HomeController> logger)
		{
			_logger = logger;

			var connectionString = config.GetConnectionString("DefaultConnection");
			var resolver = IoCBuilder.Build(connectionString);

			_model = resolver.Resolve<IMainModel>();

			var loginModel = _model.LoginModel;
			//loginModel.Updated
			//loginModel.Update(true);

			_model.Updated += (sender, vm) => _mainViewModel = vm;
			_model.SelectedSalaryPay += (sender, days) => _workedDays = days;
		}

		private void ShowLoginView(bool isStartApp)
		{
			var model = _model.LoginModel;
			//model.SqlConnectingError += (sender, e) => _view.ShowSqlError();
		}

		public IActionResult Index(int year = 2019, int month = 1)
		{
			var user = Content(User.Identity.Name);


			Response.Cookies.Append("Test", "Test value",
				new CookieOptions
				{
					Expires = DateTimeOffset.Now.AddMonths(1)
				});

			_model.ChangeMonth(year, month);
			return View(_mainViewModel);
		}

		public IActionResult Privacy()
		{
			var cookie = Request.Cookies["Test"];
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
