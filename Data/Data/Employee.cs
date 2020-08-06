using LinqToDB.Mapping;

namespace Furmanov.Data.Data
{
	[Table(Name = "Employee")]
	public class EmployeeDto : Dto
	{
		[Column] public string Name { get; set; }
		[Column] public string Position { get; set; }
		[Column] public string Phone { get; set; }
		[Column] public decimal Salary { get; set; }
		[Column] public string Card { get; set; }
		[Column] public bool IsDeleted { get; set; }
	}
}
