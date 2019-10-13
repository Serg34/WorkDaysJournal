using System;
using System.Data;

namespace Furmanov.Dal.Dto
{
	public class SalaryPay
	{
		public int Id { get; set; }
		public DateTime? Month { get; set; }
		public int Resource_Id { get; set; }
		public int? Object_Id { get; set; }
		public int Position_Id { get; set; }
		public decimal? Avans { get; set; }
		public decimal? Penalty { get; set; }
		public decimal? Premium { get; set; }
		public string Comment { get; set; }
		public int RateDays { get; set; }
		public int? FactDays { get; set; }
		public decimal? FactSalary { get; set; }
	}
}
