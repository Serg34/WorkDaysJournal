﻿using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Furmanov.Data.Data;

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

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			await Authorize();
			_model.Update();
			return View(_mainViewModel);
		}
		[HttpGet]
		public async Task<IActionResult> ChangeMonth(int year, int month)
		{
			await Authorize();
			_model.ChangeMonth(year, month);
			return View(nameof(Index), _mainViewModel);
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
			var workDay = vm.WorkedDay;
			workDay.Date = DateTime.Parse(workDay.DateJson);
			_model.SaveWorkDay(workDay);
			_mainViewModel.SalaryPays.ForEach(p => p.IsExpanded = vm.ExpandList.Contains(p.ViewModelId.ToId()));

			ViewBag.SelectedRow = vm.SelectedRow;

			return PartialView("_SalaryPays", _mainViewModel);
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
