using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Furmanov.Data.Data
{
	public enum ObjType { Project, Object, SalaryPay, Summary }

	[Table(Name = "SalaryPay")]
	public class SalaryPayDto : Dto
	{
		[Column] public int Year { get; set; }
		[Column] public int Month { get; set; }
		[Column] public int Employee_Id { get; set; }
		[Column] public int Object_Id { get; set; }

		[Display(Name = "Аванс")]
		[DisplayFormat(DataFormatString = "c2")]
		[Column] public decimal? Advance { get; set; }

		[Display(Name = "Штрафы")]
		[DisplayFormat(DataFormatString = "c2")]
		[Column] public decimal? Penalty { get; set; }

		[Display(Name = "Премии")]
		[DisplayFormat(DataFormatString = "c2")]
		[Column] public decimal? Premium { get; set; }

		[Display(Name = "Комментарий")]
		[Column] public string Comment { get; set; }

		[Display(Name = "Норма")]
		[Column] public int RateDays { get; set; }

		[Display(Name = "Факт")]
		[Editable(false)]
		[Column] public int? FactDays { get; set; }

		[Display(Name = "К выплате")]
		[Editable(false)]
		[DisplayFormat(DataFormatString = "c2")]
		[Column] public decimal? SalaryToPay { get; set; }
	}

	[DebuggerDisplay("{ViewModelId}")]
	public class SalaryPay : SalaryPayDto, IViewModel
	{
		[Column] public string ViewModelId { get; set; }
		[Column] public ObjType Type { get; set; }
		[Column] public string ParentId { get; set; }
		public bool HasChildren { get; set; } // for treelist
		public bool IsExpanded { get; set; } // for treelist

		[Display(Name = "ФИО / Наименование")]
		[Editable(false)]
		[Column] public string Name { get; set; }

		[Display(Name = "Должность / Адрес")]
		[Editable(false)]
		[Column] public string Position { get; set; }

		[Display(Name = "Телефон")]
		[Editable(false)]
		[Column] public string Phone { get; set; }

		[Display(Name = "Оклад")]
		[Editable(false)]
		[DisplayFormat(DataFormatString = "c2")]
		[Column] public decimal Salary { get; set; }

		public SalaryPay Clone() => (SalaryPay)MemberwiseClone();
	}
}
