using LinqToDB.Mapping;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Data.Data
{
	[Table("Bug")]
	public class BugDto
	{
		[PrimaryKey, Identity] public int Id { get; set; }

		[DisplayName("Проект")]
		[Column] public string Project { get; set; }

		[DisplayName("Сообщение")]
		[Column] public string Message { get; set; }
		[Column] public string InfoForDeveloper { get; set; }

		[DisplayName("Снимок экрана")]
		[Column] public byte[] PrintScreen { get; set; }
		[Column] public string User { get; set; }


		[DisplayName("Дата исправления")]
		[Column] public DateTime? DateSolved { get; set; }


		[DisplayName("Сообщение пользователю")]
		[Column] public string InfoToUser { get; set; }


		[Column(SkipOnInsert = true, SkipOnUpdate = true)]
		[DisplayFormat(DataFormatString = "dd.MM.yy hh:mm")]
		public DateTime dtc { get; set; }
	}
}
