using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Dal.Dto
{
	public class SalaryPayDb
	{
		public int Id { get; set; }
		public DateTime? Month { get; set; }
		public int Resource_Id { get; set; }
		public int? Object_Id { get; set; }
		public int Position_Id { get; set; }

		[Range(0, 999999)]
		[DisplayName("Аванс")]
		public decimal? Avans { get; set; }

		[Range(0, 999999)]
		[DisplayName("Штрафы")]
		public decimal? Penalty { get; set; }

		[Range(0, 999999)]
		[DisplayName("Премии")]
		public decimal? Premium { get; set; }

		[DisplayName("Комментарий")]
		public string Comment { get; set; }

		[Range(0, 31)]
		[DisplayName("Норма")]
		public int RateDays { get; set; }

		[DisplayName("Факт")]
		public int? FactDays { get; set; }

		[DisplayName("ЗП")]
		public decimal? SalaryPaid { get; set; }
	}
	public enum ObjType { Project, Object, ResOP }
	public class SalaryPayView : SalaryPayDb, IViewModel
	{
		public ObjType Type { get; set; }
		public string ViewModelId { get; set; }
		public string ParentId { get; set; }

		[DisplayName("В штате")]
		public bool IsStaff { get; set; }

		[Required(ErrorMessage = "Это поле не может быть пустым")]
		[DisplayName("ФИО")]
		public string Name { get; set; }

		[DisplayName("Телефон")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Это поле не может быть пустым")]
		[DisplayName("Должность")]
		public string PositionName { get; set; }

		[DisplayName("Оклад")]
		public decimal Salary { get; set; }
		

		[DisplayName("Мед.книжка")]
		[Description("Медицинская книжка")]
		public decimal? MedBook { get; set; } //в базе Resource

		[DisplayName("Спецодежда")]
		public decimal? SpecWear { get; set; } //в базе Resource
	}
}
