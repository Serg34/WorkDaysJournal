using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Data.Data
{
	public enum Role
	{
		[Display(Name = "Администратор")] Admin = 1,
		[Display(Name = "Директор")] Director = 2,
		[Display(Name = "Руководитель проекта")] ProjectManager = 3,
		[Display(Name = "Менеджер")] Manager = 4,
	}

	[Table(Name = "Role")]
	public class RoleDto : Dto
	{
		[Column] public string Name { get; set; }
		public override string ToString() => Name;
	}
}
