using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.MVP;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Furmanov.Controllers
{
	public class ExceptionController : Controller
	{
		private readonly ILogger<ExceptionController> _logger;
		private readonly UserContext _db;
		private readonly IMainModel _model;

		public ExceptionController(IConfiguration config,
					ILogger<ExceptionController> logger,
					UserContext context)
		{
			_logger = logger;
			_db = context;

			var connectionString = config.GetConnectionString("DefaultConnection");
			var resolver = IoCBuilder.Build(connectionString);

			_model = resolver.Resolve<IMainModel>();
		}

		public async Task<IActionResult> ReportBug(string path, string status, string errorMsg)
		{
			ViewBag.Path = path;
			ViewBag.Status = status;
			ViewBag.ErrorMsg = errorMsg;

			BugEventArgs args = null;
			var ex = ExceptionService.GetLastException(Request);
			if (ex != null)
			{
				var user = await _db.GetUserAsync(User);
				_logger.LogError($"error:{ex.GetType()?.Name}\n{ex}\n" +
								 $"user:{user.Login}\n");
				_model.UpdateLogin(user);
				args = new BugEventArgs("Табель учёта рабочего времени", ex);
				_model.ReportBug(this, args);
			}
			var result = View(args);

			return result;
		}

		/// <summary>Name of Controller without "Controller"</summary>
		public static string Name => typeof(ExceptionController).Name.Replace("Controller", "");
	}
}