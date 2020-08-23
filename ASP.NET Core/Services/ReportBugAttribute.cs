using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Furmanov.Services
{
	public class ReportBugAttribute : Attribute, IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			var request = context.HttpContext.Request;
			var ex = context.Exception;
			ExceptionService.AddException(request, ex);
		}
	}
}
