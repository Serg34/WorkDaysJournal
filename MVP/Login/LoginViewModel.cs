using Furmanov.Data.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.MVP.Login
{
	public class LoginViewModel
	{
		[Display(Name = "Введите логин")]
		[Required(ErrorMessage = "Логин не может быть пустым")]
		public string Login { get; set; }
		
		[Display(Name = "Введите пароль")]
		[Required(ErrorMessage = "Пароль не может быть пустым")]
		public string Password { get; set; }

		public bool IsRememberLogin { get; set; }
		public bool IsRememberPassword { get; set; }
		public List<LoginPassword> AutoLogins { get; set; }
		public bool CanLogin { get; set; } //запрет автовхода при выходе из учётки
	}
}
