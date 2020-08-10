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
		public static Bug Report(DbContext db, Exception ex, string infoForDeveloper)
		{
//#if DEBUG
//			MessageService.Error(ex.ToString());
//			return null;
//#endif
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

			var bugDto = db.FirstOrDefault<BugDto>(b => b.Message == message);
			var isNew = bugDto == null;
			if (isNew)
			{
				bugDto = new BugDto
				{
					Project = Application.ProductName,
					Message = message,
					InfoForDeveloper = infoForDeveloper,
					User = Environment.UserName,
					PrintScreen = ScreenPrinter.Print().ToByteArray()
				};
				bugDto.Id = db.InsertWithInt32Identity(bugDto);
			}
			else if (bugDto.PrintScreen == null) //если разработчик удалил снимок экрана
			{
				bugDto.PrintScreen = ScreenPrinter.Print().ToByteArray();
				db.Update(bugDto);
			}

			var incident = new BugIncidentDto
			{
				Bug_Id = bugDto.Id,
				User = Environment.UserName
			};
			db.Insert(incident);

			var bug = MapperService.Map<Bug>(bugDto);
			bug.IsNew = isNew;
			return bug;
		}
	}
}
