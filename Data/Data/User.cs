using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Data.Data
{
	public enum Role { ProjectManager = 4, Manager = 5 }

	[Table(Name = "User")]
	public class UserDto : Dto
	{
		[Display(Name = "Логин")]
		[Required(ErrorMessage = "Логин не может быть пустым")]
		[Column] public string Login { get; set; }

		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Пароль не может быть пустым")]
		[Column] public string Password { get; set; }

		[Display(Name = "Почта")]
		[Required(ErrorMessage = "Почта не может быть пустой")]
		[Column] public string Email { get; set; }
		[Column] public int RoleId { get; set; }
	}

	public class User : UserDto
	{
		public Role Role => (Role)RoleId;

		[Display(Name = "Роль")]
		public string RoleName { get; set; }
	}
}
