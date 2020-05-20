using Furmanov.Data.Data;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Furmanov.Services
{
	public static class BugReporter
	{
		public static void Report(Exception ex, IWin32Window owner, string infoToDeveloper = null)
		{
#if DEBUG
			MessageService.Error(ex.ToString());
			return;
#endif
			var message = ex.ToString();
			if (message.Contains("Время ожидания выполнения истекло") ||
				message.Contains("Сервер не найден или недоступен") ||
				message.Contains("Ошибка на транспортном уровне при получении результатов с сервера"))
			{
				MessageService.Error("Произошла ошибка подключения к серверу SQL.\n" +
									 "Обратитесь к системному администратору.\n\n" +
									 $"{ex.Message}");
				return;
			}

			using (var form = new FrmReportBug(ex, infoToDeveloper))
			{
				if (owner != null) form.ShowDialog(owner);
				else form.ShowDialog();
			}
		}

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
					MessageService.Error($"Новая ошибка: {ex.Message}\n\n" +
											 "Все необходимые сведения уже отправлены разработчикам");
				}
				else
				{
					MessageService.Error($"Известная ошибка: {ex.Message}\n\n" +
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
