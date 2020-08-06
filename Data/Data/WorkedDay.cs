using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Data.Data
{
	[Table(Name = "WorkedDay")]
	public class WorkedDayDto : Dto
	{
		[Column] public int SalaryPayId { get; set; }

		[Display(Name = "Дата")]
		[Editable(false)]
		[Column] public DateTime Date { get; set; }
	}

	public class WorkedDay : WorkedDayDto
	{
		[Display(Name = "Выход")]
		public bool IsWorked { get; set; }
		public int EmployeeId { get; set; }
		public int ObjectId { get; set; }
	}
}
