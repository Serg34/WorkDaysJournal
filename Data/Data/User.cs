using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Data.Data
{
	[Table(Name = "User")]
	public class UserDto : Dto
	{
		[Display(Name = "Логин")]
		[Required(ErrorMessage = "Логин не может быть пустым")]
		[Column] public string Login { get; set; }

		[Display(Name = "ФИО")]
		[Required(ErrorMessage = "ФИО не может быть пустым")]
		[Column] public string Name { get; set; }

		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Пароль не может быть пустым")]
		[Column] public string Password { get; set; }

		[Display(Name = "Почта")]
		[Required(ErrorMessage = "Почта не может быть пустой")]
		[Column] public string Email { get; set; }
		[Column] public Role Role_Id { get; set; } //Для Entity Framework нужно соответсвие названия свойства и столбца
	}

	public class User : UserDto
	{
		[Display(Name = "Роль")]
		[Column] public string RoleName { get; set; }
	}
}
