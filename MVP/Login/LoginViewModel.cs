namespace Furmanov.MVP.Login
{
	public class LoginViewModel
	{
		public string Login { get; set; }

		public string Password { get; set; }

		public bool IsRemember { get; set; }
		public bool IsAutoLoginOnStart { get; set; }
		public bool CanLogin { get; set; } //запрет автовхода при выходе из учётки
	}
}
