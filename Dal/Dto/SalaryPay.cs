using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

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
		[Range(0, 999999)]
		[Display(Name = "Аванс")]
		public decimal? Advance { get; set; }

		[Column(Name = "Penalty")]
		[Range(0, 999999)]
		[Display(Name = "Штрафы")]
		public decimal? Penalty { get; set; }

		[Column(Name = "Premium")]
		[Range(0, 999999)]
		[Display(Name = "Премии")]
		public decimal? Premium { get; set; }

		[Column(Name = "Comment")]
		[Display(Name = "Комментарий")]
		public string Comment { get; set; }

		[Column(Name = "RateDays")]
		[Range(0, 31)]
		[Display(Name = "Норма")]
		public int RateDays { get; set; }

		[Column(Name = "FactDays")]
		[Display(Name = "Факт")]
		public int? FactDays { get; set; }

		[Column(Name = "SalaryPay")]
		[Display(Name = "ЗП")]
		public decimal? SalaryPay { get; set; }
	}
	public enum ObjType { Project, Object, Salary }
	public class SalaryPayVisual : SalaryPayDb, IViewModel
	{
		public ObjType Type { get; set; }
		public string ViewModelId { get; set; }
		public string ParentId { get; set; }

		[Display(Name = "ФИО")]
		public string Name { get; set; }

		[Display(Name = "Телефон")]
		public string Phone { get; set; }

		[Display(Name = "Должность")]
		public string PositionName { get; set; }

		[Display(Name = "Оклад")]
		public decimal Salary { get; set; }
	}
}
