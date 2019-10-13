using System;
using System.Data;

namespace Furmanov.Dal.Dto
{
	[Flags]
	public enum SelectionResourceMode
	{
		Staff = 1,
		Freelance = 1 << 1,
		All = Staff | Freelance
	}
	public class Employee
	{
		public int Id { get; set; }
		public int? Object_Id { get; set; }
		public int? Manager_Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Description { get; set; }
		public decimal? OfficialSalary { get; set; }
		public DateTime dtc { get; set; }
		public string Card { get; set; }
	}
}
