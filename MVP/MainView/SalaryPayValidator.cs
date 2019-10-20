﻿using System;
using FluentValidation;
using Furmanov.Dal.Dto;

namespace Furmanov.MVP.MainView
{
	public class SalaryPayValidator : AbstractValidator<SalaryPayViewModel>
	{
		public SalaryPayValidator()
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

			RuleFor(s => s.RateDays)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Норма не может быть отрицательной")
				.LessThanOrEqualTo(31)
				.WithMessage("Норма не может быть больше 31 дня");

			RuleFor(s => s.Salary)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Зарплата не может быть отрицательной");
		}
	}
}
