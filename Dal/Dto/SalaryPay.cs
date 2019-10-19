using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Furmanov.Dal.Dto
{
	[Table(Name = "SalaryPay")]
	public class SalaryPayDb
	{
		[PrimaryKey, Identity]
		public int Id { get; set; }

		[Column(Name = "Month")]
		public DateTime Month { get; set; }

		[Column(Name = "EmployeeId")]
		public int EmployeeId { get; set; }

		[Column(Name = "ObjectId")]
		public int ObjectId { get; set; }

		[Column(Name = "PositionId")]
		public int PositionId { get; set; }

		[Column(Name = "Advance")]
		[Display(Name = "Аванс")]
		public decimal? Advance { get; set; }

		[Column(Name = "Penalty")]
		[Display(Name = "Штрафы")]
		public decimal? Penalty { get; set; }

		[Column(Name = "Premium")]
		[Display(Name = "Премии")]
		public decimal? Premium { get; set; }

		[Column(Name = "Comment")]
		[Display(Name = "Комментарий")]
		public string Comment { get; set; }

		[Column(Name = "RateDays")]
		[Display(Name = "Норма")]
		public int RateDays { get; set; }

		[Column(Name = "FactDays")]
		[Display(Name = "Факт")]
		[Editable(false)]
		public int? FactDays { get; set; }

		[Column(Name = "SalaryPay")]
		[Display(Name = "Зарплата")]
		[Editable(false)]
		public decimal? SalaryPay { get; set; }
	}
	public enum ObjType { Project, Object, Salary }

	[DebuggerDisplay("{VisualId}")]
	public class SalaryPayViewModel : SalaryPayDb, IVisual
	{
		[Column(Name = "Type")]
		public ObjType Type { get; set; }

		[Column(Name = "VisualId")]
		public string VisualId { get; set; }

		[Column(Name = "ParentId")]
		public string ParentId { get; set; }

		[Column(Name = "Name")]
		[Display(Name = "ФИО")]
		[Editable(false)]
		public string Name { get; set; }

		[Column(Name = "Phone")]
		[Display(Name = "Телефон")]
		[Editable(false)]
		public string Phone { get; set; }

		[Column(Name = "PositionName")]
		[Display(Name = "Должность")]
		[Editable(false)]
		public string PositionName { get; set; }

		[Column(Name = "Salary")]
		[Display(Name = "Оклад")]
		[Editable(false)]
		public decimal Salary { get; set; }
	}
}
