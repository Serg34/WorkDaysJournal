using Furmanov.Controllers;
using Furmanov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Furmanov.Services
{
	public class ReportBugAttribute : Attribute, IExceptionFilter
	{
		private ExceptionService _exceptionService;

		public void OnException(ExceptionContext context)
		{


			var model = new ExceptionModel
			{
				TypeEx = context.Exception?.GetType()?.Name,
				MessageEx = context.Exception?.Message,
				MessageFull = context.Exception?.ToString()
			};
			var json = model.ToJson();

			//var ex = context.Exception;
			//var e = new BugEventArgs("Табель учёта рабочего времени", ex);
			//var viewData = new ViewDataDictionary<BugEventArgs>()

			//var url = $"~/{ExceptionController.Name}/{nameof(ExceptionController.ReportBug)}";
			//context.Result = new RedirectToActionResult(url, json);

			//context.Result = new ContentResult { Content = json };

			context.Result = new RedirectToActionResult(
				nameof(ExceptionController.ReportBug),
				ExceptionController.Name,
				json);


			context.ExceptionHandled = true;
		}
	}
}
