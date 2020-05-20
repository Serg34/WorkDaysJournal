using LinqToDB;
using SC.Common.Dal;
using SC.Common.Model;
using System;
using System.Reflection;
using System.Windows.Forms;
using UltraZoom.Services;

namespace SC.Common.Services
{
	public static class BugReporter
	{
		public static void Report(Exception ex)
		{
			using (var db = new FeedbackDbDataContext())
			{
				var message = ex.ToString();
				var bug = db.FirstOrDefault<CBugDto>(b => b.Message == message);
				if (bug == null)
				{
					var assambly = Assembly.GetEntryAssembly();
					var printScreen = ScreenPrinter.Print();
					bug = new CBugDto
					{
						Solution = assambly.GetName().Name,
						Project = Application.ProductName,
						Message = message,
						User = Environment.UserName,
						PrintScreen = printScreen.ToByteArray()
					};
					bug.ID = db.InsertWithInt32Identity(bug);
					MessageService.ShowError($"Новая ошибка: {ex.Message}\n\n" +
											 "Все необходимые сведения уже отправлены разработчикам");
				}
				else
				{
					MessageService.ShowError($"Известная ошибка: {ex.Message}\n\n" +
											 "Все необходимые сведения уже отправлены разработчикам\n\n" +
											 (bug.DateSolved != null ? $"Планируемая дата решения: {bug.DateSolved:d}\n\n" : "") +
											 bug.InfoToUser);
				}
				var incident = new CBugIncidentDto
				{
					Bug_ID = bug.ID,
					DateTime = DateTime.Now,
					User = Environment.UserName
				};
				db.Insert(incident);
			}
		}
	}
}
