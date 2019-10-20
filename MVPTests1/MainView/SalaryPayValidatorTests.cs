using FluentValidation.TestHelper;
using Furmanov.MVP.MainView;
using NUnit.Framework;

namespace Furmanov.Tests
{
	[TestFixture]
	public class SalaryPayValidatorTests
	{
		private SalaryPayValidator _validator;

		[SetUp]
		public void Setup()
		{
			_validator = new SalaryPayValidator();
		}
		[Test]
		public void SalaryPayValidator_AdvanceTest()
		{
			_validator.ShouldNotHaveValidationErrorFor(s => s.Advance, null as decimal?);
			_validator.ShouldNotHaveValidationErrorFor(s => s.Advance, 0);
			_validator.ShouldNotHaveValidationErrorFor(s => s.Advance, 500);
			_validator.ShouldHaveValidationErrorFor(s => s.Advance, -500);
		}
		[Test]
		public void SalaryPayValidator_PenaltyTest()
		{
			_validator.ShouldNotHaveValidationErrorFor(s => s.Penalty, null as decimal?);
			_validator.ShouldNotHaveValidationErrorFor(s => s.Penalty, 0);
			_validator.ShouldNotHaveValidationErrorFor(s => s.Penalty, 500);
			_validator.ShouldHaveValidationErrorFor(s => s.Penalty, -500);
		}
		[Test]
		public void SalaryPayValidator_PremiumTest()
		{
			_validator.ShouldNotHaveValidationErrorFor(s => s.Premium, null as decimal?);
			_validator.ShouldNotHaveValidationErrorFor(s => s.Premium, 0);
			_validator.ShouldNotHaveValidationErrorFor(s => s.Premium, 500);
			_validator.ShouldHaveValidationErrorFor(s => s.Premium, -500);
		}
		[Test]
		public void SalaryPayValidator_RateDaysTest()
		{
			_validator.ShouldNotHaveValidationErrorFor(s => s.RateDays, 0);
			_validator.ShouldNotHaveValidationErrorFor(s => s.RateDays, 30);
			_validator.ShouldHaveValidationErrorFor(s => s.RateDays, -20);
			_validator.ShouldHaveValidationErrorFor(s => s.RateDays, 32);
		}
		[Test]
		public void SalaryPayValidator_SalaryTest()
		{
			_validator.ShouldNotHaveValidationErrorFor(s => s.Salary, 0);
			_validator.ShouldNotHaveValidationErrorFor(s => s.Salary, 500);
			_validator.ShouldHaveValidationErrorFor(s => s.Salary, -500);
		}
	}
}