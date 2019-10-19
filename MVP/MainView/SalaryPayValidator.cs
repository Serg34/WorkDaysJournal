using FluentValidation;
using Furmanov.Dal.Dto;

namespace Furmanov.MVP.MainView
{
	public class SalaryPayValidator : AbstractValidator<SalaryPayViewModel>
	{
		public SalaryPayValidator()
		{
			RuleFor(s => s.Advance)
				.GreaterThan(0)
				.When(s => s.Advance != null)
				.WithMessage("Аванс не может быть отрицательным");

			RuleFor(s => s.Penalty)
				.GreaterThan(0)
				.When(s => s.Penalty != null)
				.WithMessage("Штраф не может быть отрицательным");

			RuleFor(s => s.Premium)
				.GreaterThan(0)
				.When(s => s.Premium != null)
				.WithMessage("Премия не может быть отрицательной");

			RuleFor(s => s.RateDays)
				.GreaterThan(0)
				.WithMessage("Норма не может быть отрицательной");

			RuleFor(s => s.Salary)
				.GreaterThan(0)
				.WithMessage("Зарплата не может быть отрицательной");
		}
	}
}
