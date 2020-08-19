using FluentValidation;
using Furmanov.Data.Data;
using Furmanov.Services;
using System;
using System.Linq.Expressions;

namespace Furmanov.MVP.MainView
{
	public class SalaryPayValidator : AbstractValidator<SalaryPay>
	{
		public SalaryPayValidator(bool checkDaysMaxCount = true)
		{
			new Expression<Func<SalaryPay, decimal?>>[]
			{
				pay => pay.Advance,
				pay => pay.Penalty,
				pay => pay.Premium,
			}.ForEach(expression =>
			{
				RuleFor(expression)
					.GreaterThanOrEqualTo(0)
					.When(pay =>
					{
						var func = expression.Compile();
						var value = func.Invoke(pay);
						return value != null;
					})
					.WithMessage("Это число не может быть отрицательным");
			});

			new Expression<Func<SalaryPay, decimal>>[]
			{
				pay => pay.RateDays,
				pay => pay.Salary,
			}.ForEach(expression =>
			{
				RuleFor(expression)
					.GreaterThanOrEqualTo(0)
					.WithMessage("Это число не может быть отрицательным");
			});

			if (checkDaysMaxCount)
			{
				var daysMaxCount = 31;
				RuleFor(s => s.RateDays)
					.LessThanOrEqualTo(daysMaxCount)
					.WithMessage($"Норма не может быть больше {daysMaxCount}");
			}
		}
		public SalaryPayValidator(int year, int month) : this(false)
		{
			var daysMaxCount = DateTime.DaysInMonth(year, month);
			RuleFor(s => s.RateDays)
				.LessThanOrEqualTo(daysMaxCount)
				.WithMessage($"Норма не может быть больше количества дней в месяце ({daysMaxCount})");
		}
	}
}
