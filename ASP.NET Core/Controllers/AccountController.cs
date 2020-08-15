using Furmanov.Data.Data;
using Furmanov.Models;
using Furmanov.MVP.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Furmanov.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserContext _db;

		public AccountController(UserContext context)
		{
			_db = context;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var user = await _db.GetUserAsync(vm.Login, vm.Password);
				if (user != null)
				{
					await Authenticate(user); // аутентификация

					return RedirectToAction(nameof(HomeController.Index),HomeController.Name);
				}
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(vm);
		}

		private async Task Authenticate(UserDto user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user?.Login),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user?.Role_Id.ToString()),
			};

			var id = new ClaimsIdentity(claims,
				"ApplicationCookie",
				ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(id));
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction(nameof(Login));
		}

		public IActionResult AccessDenied() => View();


		/// <summary>Name of Controller without "Controller"</summary>
		public static string Name => typeof(AccountController).Name.Replace("Controller", "");
	}
}