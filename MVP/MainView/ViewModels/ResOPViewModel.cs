using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;

namespace Furmanov.MVP.MainView.ViewModels
{
	public enum ObjType { Project, Object, ResOP }

	[DebuggerDisplay("{ViewModelId}")]
	public class ResOPViewModel : IViewModel
	{
		public ObjType Type { get; set; } //нет в базе
		public string ViewModelId { get; set; } //нет в базе
		public string ParentId { get; set; } //нет в базе

		public int? ResOP_Id { get; set; } //в базе ResOP
		public int Object_Id { get; set; } //в базе ResOP
		public string ObjectNameForResOp { get; set; } //в базе CObject
		public int? ManagerId { get; set; } //в базе ResOP

		[DisplayName("В штате")]
		public bool IsStaff { get; set; } //нет в базе
		public int? PositionId { get; set; } //в базе ResOP


		public int Resource_Id { get; set; } //в базе Resource

		[Required(ErrorMessage = "Это поле не может быть пустым")]
		[DisplayName("ФИО")]
		public string Name { get; set; } //в базе Resource

		[DisplayName("Телефон")]
		public string Phone { get; set; } //в базе Resource

		[Required(ErrorMessage = "Это поле не может быть пустым")]
		[DisplayName("Должность")]
		public string PositionName { get; set; } //в базе Position

		[DisplayName("Оклад")]
		public decimal Salary { get; set; } //в базе Oklad

		[DisplayName("Оф.оклад")]
		public decimal? OfficialSalary { get; set; } //в базе Resource


		[Range(0, 31)]
		[DisplayName("Норма")]
		public int? RateDays { get; set; } //в базе ResOP

		[DisplayName("Факт")]
		public int? FactDays { get; set; } //в базе ResOP

		[DisplayName("ЗП")]
		public decimal? FactSalary { get; set; } //в базе ResOP

		[Range(0, 999999)]
		[DisplayName("Аванс")]
		public decimal? Avans { get; set; } //в базе ResOP

		[Range(0, 999999)]
		[DisplayName("Штрафы")]
		public decimal? Penalty { get; set; } //в базе ResOP

		[Range(0, 999999)]
		[DisplayName("Премии")]
		public decimal? Premium { get; set; } //в базе ResOP

		[DisplayName("Комментарий")]
		public string Comment { get; set; } //в базе ResOP

		public DateTime Month { get; set; }

		[DisplayName("Мед.книжка")]
		[Description("Медицинская книжка")]
		public decimal? MedBook { get; set; } //в базе Resource

		[DisplayName("Спецодежда")]
		public decimal? SpecWear { get; set; } //в базе Resource

		public static ResOPViewModel Factory(DataRow r) => new ResOPViewModel
		{
			ViewModelId = G._S(r["ViewModelId"]),
			Type = (ObjType)G._I(r["Type"]),
			ParentId = G._S(r["ParentId"]),
			Month = G._D(r["Month"]),

			Object_Id = G._I(r["Object_Id"]),
			ObjectNameForResOp = G._S(r["ObjectNameForResOp"]),
			ManagerId = G._I(r["Manager_Id"]),
			IsStaff = G._B(r["IsStaff"]),

			Resource_Id = G._I(r["Resource_Id"]),
			Name = G._S(r["Name"]),
			Phone = G._S(r["Phone"]),
			OfficialSalary = G._Dec(r["OfficialSalary"]),
			MedBook = G._I(r["MedBook"]),
			SpecWear = G._I(r["SpecWear"]),

			ResOP_Id = G._I(r["ResOP_Id"]),
			PositionId = G._I(r["PositionId"]),
			RateDays = G._I(r["RateDays"]),
			FactDays = G._I(r["FactDays"]),
			FactSalary = G._Dec(r["FactSalary"]),
			Avans = G._Dec(r["Avans"]),
			Penalty = G._Dec(r["Penalty"]),
			Premium = G._Dec(r["Premium"]),
			Comment = G._S(r["Comment"]),

			Salary = G._Dec(r["Salary"]),
			PositionName = G._S(r["PositionName"]),
		};
	}
}
