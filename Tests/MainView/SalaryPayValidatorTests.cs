using FluentValidation.TestHelper;
using Furmanov.Data.Data;
using Furmanov.MVP.MainView;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Furmanov.Tests.MainView
{
	[TestFixture]
	public class SalaryPayValidatorTests
	{
		private SalaryPayValidator _validator;

		[SetUp]
		public void Setup()
		{
			_validator = new SalaryPayValidator(2019,1);
		}
		[Test]
		public void SalaryPayValidator_AdvanceTest()
		{
			NoError(s => s.Advance, null);
			NoError(s => s.Advance, 0);
			NoError(s => s.Advance, 500);
			IsError(s => s.Advance, -500);
		}
		[Test]
		public void SalaryPayValidator_PenaltyTest()
		{
			NoError(s => s.Penalty, null);
			NoError(s => s.Penalty, 0);
			NoError(s => s.Penalty, 500);
			IsError(s => s.Penalty, -500);
		}
		[Test]
		public void SalaryPayValidator_PremiumTest()
		{
			NoError(s => s.Premium, null);
			NoError(s => s.Premium, 0);
			NoError(s => s.Premium, 500);
			IsError(s => s.Premium, -500);
		}
		[Test]
		public void SalaryPayValidator_RateDaysTest()
		{
			NoError(s => s.RateDays, 0);
			NoError(s => s.RateDays, 30);
			IsError(s => s.RateDays, -20);
			IsError(s => s.RateDays, 32);
		}
		[Test]
		public void SalaryPayValidator_SalaryTest()
		{
			NoError(s => s.Salary, 0);
			NoError(s => s.Salary, 500);
			IsError(s => s.Salary, -500);
		}

		private void IsError<T>(Expression<Func<SalaryPay, T>> func, T val)
		{
			_validator.ShouldHaveValidationErrorFor(func, val);
		}
		private void NoError<T>(Expression<Func<SalaryPay, T>> func, T val)
		{
			_validator.ShouldNotHaveValidationErrorFor(func, val);
		}
	}
}