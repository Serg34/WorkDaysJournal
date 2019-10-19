using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Dal.Dto
{
	[Table(Name = "WorkedDay")]
	public class WorkedDayDb
	{
		[PrimaryKey, Identity]
		public int Id { get; set; }

		[Column(Name = "SalaryPayId")]
		public int SalaryPayId { get; set; }

		[Column(Name = "Date")]
		[Display(Name = "Дата")]
		[Editable(false)]
		public DateTime Date { get; set; }
	}

	public class WorkedDayViewModel : WorkedDayDb
	{
		[Display(Name = "Выход")]
		public bool IsWorked { get; set; }
		public int EmployeeId { get; set; }
		public int ObjectId { get; set; }
	}
}
