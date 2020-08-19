using Furmanov.Data.Data;
using Furmanov.Models;
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
					await Authenticate(user, vm.IsRemember); // аутентификация

					return RedirectToAction(nameof(HomeController.Index), HomeController.Name);
				}
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(vm);
		}

		private async Task Authenticate(UserDto user, bool isRemember)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user?.Login),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user?.Role_Id.ToString()),
			};

			const string nameType = ClaimsIdentity.DefaultNameClaimType;
			const string roleType = ClaimsIdentity.DefaultRoleClaimType;
			var id = new ClaimsIdentity(claims,"ApplicationCookie",nameType, roleType);

			const string scheme = CookieAuthenticationDefaults.AuthenticationScheme;
			var properties = new AuthenticationProperties
			{
				IsPersistent = isRemember
			};
			await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(id), properties);
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