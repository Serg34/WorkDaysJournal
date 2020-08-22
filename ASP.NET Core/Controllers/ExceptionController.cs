using System.Diagnostics;
using Furmanov.IoC;
using Furmanov.Models;
using Furmanov.MVP;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Furmanov.Controllers
{
	public class ExceptionController : Controller
    {
	    private readonly ILogger<ExceptionController> _logger;
	    private readonly UserContext _db;
	    private readonly ExceptionService _exceptionService;
	    private readonly IMainModel _model;

	    public ExceptionController(IConfiguration config, 
				    ILogger<ExceptionController> logger, 
				    UserContext context, 
				    ExceptionService exceptionService)
	    {
		    _logger = logger;
		    _db = context;
		    _exceptionService = exceptionService;

		    var connectionString = config.GetConnectionString("DefaultConnection");
		    var resolver = IoCBuilder.Build(connectionString);

		    _model = resolver.Resolve<IMainModel>();
	    }

		//[HttpPost]
		public async Task<IActionResult> ReportBug()
		{
			var ex = _exceptionService.Exception;

			var user = await _db.GetUserAsync(User);
			_model.UpdateLogin(user);

			_logger.LogError($"error:{ex.GetType()?.Name}\n{ex}");

			var e = new BugEventArgs("Табель учёта рабочего времени", ex);
			_model.ReportBug(this, e);
			var result = View(e);

			return result;
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel
			{
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			});
		}

		/// <summary>Name of Controller without "Controller"</summary>
		public static string Name => typeof(ExceptionController).Name.Replace("Controller", "");
	}
}