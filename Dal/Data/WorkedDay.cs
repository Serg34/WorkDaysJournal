using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Dal.Data
{
	[Table(Name = "WorkedDay")]
	public class WorkedDayDto
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

	public class WorkedDay : WorkedDayDto
	{
		[Display(Name = "Выход")]
		public bool IsWorked { get; set; }
		public int EmployeeId { get; set; }
		public int ObjectId { get; set; }
	}
}
