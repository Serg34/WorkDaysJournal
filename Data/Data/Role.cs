using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Data.Data
{
	public enum Role
	{
		[Display(Name = "Администратор")] Admin = 1,
		[Display(Name = "Руководитель проекта")] ProjectManager = 4,
		[Display(Name = "Менеджер")] Manager = 5,
		[Display(Name = "Директор")] Director = 6,
	}

	[Table(Name = "Role")]
	public class RoleDto : Dto
	{
		[Column] public string Name { get; set; }
		public override string ToString() => Name;
	}
}
