using System.ComponentModel.DataAnnotations;

namespace Furmanov.MVP.Login
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Не указан логин")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Не указан Пароль")]
		public string Password { get; set; }

		public bool IsRemember { get; set; }
		public bool IsAutoLoginOnStart { get; set; }
		public bool CanLogin { get; set; } //запрет автовхода при выходе из учётки
	}
}
