using Furmanov.Data;
using Furmanov.Data.Data;
using LinqToDB;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Furmanov.Services
{
	public static class BugReporter
	{
		public static BugDto Report(Exception ex, string infoToDeveloper = null)
		{
#if DEBUG
			MessageService.Error(ex.ToString());
			return null;
#endif
			var message = ex.ToString();
			if (message.Contains("Время ожидания выполнения истекло") ||
				message.Contains("Сервер не найден или недоступен") ||
				message.Contains("Ошибка на транспортном уровне при получении результатов с сервера"))
			{
				MessageService.Error("Произошла ошибка подключения к серверу SQL.\n" +
									 "Обратитесь к системному администратору.\n\n" +
									 $"{ex.Message}");
				return null;
			}
			if (ex is SqlException && message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
			{
				MessageService.Error("Ошибка удаления.\nВозможно, запись используется и не может быть удалена");
				return null;
			}

			using (var db = new FeedbackDbContext())
			{
				var bug = db.FirstOrDefault<BugDto>(b => b.Message == message);
				if (bug == null)
				{
					bug = new BugDto
					{
						Solution = "Furmanov",
						Project = Application.ProductName,
						Message = message,
						InfoToDeveloper = infoToDeveloper,
						User = Environment.UserName,
						PrintScreen = ScreenPrinter.Print().ToByteArray()
					};
					bug.ID = db.InsertWithInt32Identity(bug);
				}
				else if (bug.PrintScreen == null) //если разработчик удалил снимок экрана
				{
					bug.PrintScreen = ScreenPrinter.Print().ToByteArray();
					db.Update(bug);
				}

				var incident = new BugIncidentDto
				{
					Bug_ID = bug.ID,
					DateTime = DateTime.Now,
					User = Environment.UserName
				};
				db.Insert(incident);

				return bug;
			}
		}
	}
}
