using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Services.UI
{
	public static class ValidateService
	{
		public static (bool IsError, string Error) Validate(object viewModel)
		{
			var results = new List<ValidationResult>();
			var context = new ValidationContext(viewModel);
			if (!Validator.TryValidateObject(viewModel, context, results, true))
			{
				return (true, string.Join("\n", results.Select(r => r.ToString())));
			}

			return (false, "");
		}
	}
}
