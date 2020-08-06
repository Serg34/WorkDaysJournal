using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Furmanov.Data.Data
{
	public enum ObjType { Project, Object, Salary }

	[Table(Name = "SalaryPay")]
	public class SalaryPayDto : Dto
	{
		[Column] public int Year { get; set; }
		[Column] public int Month { get; set; }
		[Column] public int EmployeeId { get; set; }
		[Column] public int ObjectId { get; set; }
		[Column] public int PositionId { get; set; }

		[Display(Name = "Аванс")]
		[Column] public decimal? Advance { get; set; }

		[Display(Name = "Штрафы")]
		[Column] public decimal? Penalty { get; set; }

		[Display(Name = "Премии")]
		[Column] public decimal? Premium { get; set; }

		[Display(Name = "Комментарий")]
		[Column] public string Comment { get; set; }

		[Display(Name = "Норма")]
		[Column] public int RateDays { get; set; }

		[Display(Name = "Факт")]
		[Editable(false)]
		[Column] public int? FactDays { get; set; }

		[Display(Name = "Зарплата")]
		[Editable(false)]
		[Column] public decimal? SalaryPay { get; set; }
	}

	[DebuggerDisplay("{ViewModelId}")]
	public class SalaryPay : SalaryPayDto, IViewModel
	{
		[Column] public string ViewModelId { get; set; }
		[Column] public ObjType Type { get; set; }
		[Column] public string ParentId { get; set; }

		[Display(Name = "ФИО")]
		[Editable(false)]
		[Column] public string Name { get; set; }

		[Display(Name = "Телефон")]
		[Editable(false)]
		[Column] public string Phone { get; set; }

		[Display(Name = "Должность")]
		[Editable(false)]
		[Column] public string PositionName { get; set; }

		[Display(Name = "Оклад")]
		[Editable(false)]
		[Column] public decimal Salary { get; set; }

		public SalaryPay Clone() => (SalaryPay)MemberwiseClone();
	}
}
