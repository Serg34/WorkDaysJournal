using Furmanov.Data;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Moq;
using NUnit.Framework;
using System;

namespace Furmanov.Tests.MainView
{
	[TestFixture]
	public class MainModelTests
	{
		private IMainModel _model;
		[SetUp]
		public void Setup()
		{
			var db = new Mock<IDataAccessService>().Object;
			var loginModel = new Mock<ILoginModel>().Object;
			_model = new MainModel(db, loginModel);
		}

		[Test]
		public void ChangeMonthTest()
		{
			var month = DateTime.Now.AddMonths(-1);
			_model.ChangeMonth(month);
			Assert.AreEqual(month, _model.Month);
		}
	}
}