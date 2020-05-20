using FluentValidation;
using Furmanov.Data.Data;
using System;

namespace Furmanov.MVP.MainView
{
	public class SalaryPayValidator : AbstractValidator<SalaryPay>
	{
		public SalaryPayValidator(DateTime month)
		{
			RuleFor(s => s.Advance)
				.GreaterThanOrEqualTo(0)
				.When(s => s.Advance != null)
				.WithMessage("Аванс не может быть отрицательным");

			RuleFor(s => s.Penalty)
				.GreaterThanOrEqualTo(0)
				.When(s => s.Penalty != null)
				.WithMessage("Штраф не может быть отрицательным");

			RuleFor(s => s.Premium)
				.GreaterThanOrEqualTo(0)
				.When(s => s.Premium != null)
				.WithMessage("Премия не может быть отрицательной");

			var daysMaxCount = DateTime.DaysInMonth(month.Year, month.Month);
			RuleFor(s => s.RateDays)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Норма не может быть отрицательной")
				.LessThanOrEqualTo(daysMaxCount)
				.WithMessage($"Норма не может быть больше {daysMaxCount}");

			RuleFor(s => s.Salary)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Зарплата не может быть отрицательной");
		}
	}
}
