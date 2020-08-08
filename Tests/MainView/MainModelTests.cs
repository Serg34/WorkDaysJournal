using Furmanov.Data;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Moq;
using NUnit.Framework;

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
			var month = _model.Month - 1;
			_model.ChangeMonth(2019, month);
			Assert.AreEqual(month, _model.Month);
		}
	}
}