using System.Collections.Generic;
using Furmanov.Data.Data;

namespace Furmanov.MVP.Login
{
	public class LoginViewModel
	{
		public string Login { get; set; }
		public string Password { get; set; }

		public bool IsRememberLogin { get; set; }
		public bool IsRememberPassword { get; set; }
		public List<LoginPassword> AutoLogins { get; set; }
		public bool CanLogin { get; set; } //запрет автовхода при выходе из учётки
	}
}
