using System.ComponentModel.DataAnnotations;

namespace Furmanov.Models
{
	public class LoginViewModel
	{
		[Display(Name = "Введите логин")]
		[Required(ErrorMessage = "Логин не может быть пустым")]
		public string Login { get; set; }

		[Display(Name = "Введите пароль")]
		[Required(ErrorMessage = "Пароль не может быть пустым")]
		public string Password { get; set; }

		public bool IsRemember { get; set; }
	}
}
