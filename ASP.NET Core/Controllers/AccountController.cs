using Furmanov.Data;
using Furmanov.Data.Data;
using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.MVP.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Furmanov.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserContext _db;
		private readonly ILoginModel _model;

		public AccountController(IConfiguration config, UserContext context)
		{
			_db = context;

			var connectionString = config.GetConnectionString("DefaultConnection");
			var resolver = IoCBuilder.Build(connectionString);

			_model = resolver.Resolve<ILoginModel>();
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
				//_model.Login(this, vm);
				//var user = ApplicationUser.User;

				var user = await _db.User.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Login == vm.Login && u.Password == vm.Password);
				if (user != null)
				{
					await Authenticate(vm.Login); // аутентификация

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(vm);
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _db.User.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Login == model.Login);
				if (user == null)
				{
					// добавляем пользователя в бд
					_db.User.Add(new UserDto { Login = model.Login, Password = model.Password });
					await _db.SaveChangesAsync();

					await Authenticate(model.Login); // аутентификация

					return RedirectToAction("Index", "Home");
				}
				else
					ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(model);
		}

		private async Task Authenticate(string userName)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, 
				"ApplicationCookie", 
				ClaimsIdentity.DefaultNameClaimType, 
				ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
				new ClaimsPrincipal(id));
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}