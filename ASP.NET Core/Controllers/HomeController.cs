using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Furmanov.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly UserContext _db;
		private readonly IMainModel _model;
		private MainViewModel _mainViewModel;

		public HomeController(IConfiguration config, UserContext context)
		{
			_db = context;

			var connectionString = config.GetConnectionString("DefaultConnection");
			var resolver = IoCBuilder.Build(connectionString);

			_model = resolver.Resolve<IMainModel>();
			_model.Updated += (sender, vm) => _mainViewModel = vm;
		}

		public async Task<IActionResult> Index()
		{
			await Authorize();
			_model.Update();
			return View(_mainViewModel);
		}
		public async Task<IActionResult> ChangeMonth(int year, int month)
		{
			await Authorize();
			_model.ChangeMonth(year, month);
			_model.Update();
			return View(nameof(Index), _mainViewModel);
		}

		public string GetWorkedDays(int payId, int year, int month)
		{
			_model.ChangeMonth(year, month);
			var workedDays = _model.GetWorkedDays(payId);
			workedDays.ForEach(d => d.CreatedDate = DateTime.Now);
			var json = workedDays.ToJson();
			return json;
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
