using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Furmanov.Data.Data
{
	[Table(Name = "WorkedDay")]
	[DataContract]
	public class WorkedDayDto : Dto
	{
		[DataMember]
		[Column] public int SalaryPay_Id { get; set; }

		[Display(Name = "Дата")]
		[Editable(false)]
		[Column] public DateTime Date { get; set; }

		[DataMember]
		public string DateJson { get; set; }
	}

	public class WorkedDay : WorkedDayDto
	{
		[Display(Name = "Выход")]
		[DataMember]
		public bool IsWorked { get; set; }

		public WorkedDay Clone()
		{
			return (WorkedDay)MemberwiseClone();
		}
	}
}
