using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using LinqToDB.Mapping;

namespace Furmanov.Dal.Dto
{
	public enum Role { ProjectManager = 4, Manager = 5 }

	[Table(Name = "User")]
	public class UserDb
	{
		[PrimaryKey, Identity]
		public int Id { get; set; }

		[Column(Name = "Login")]
		[Display(Name = "Логин")]
		[Required(ErrorMessage = "Логин не может быть пустым")]
		public string Login { get; set; }

		[Column(Name = "Password")]
		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Пароль не может быть пустым")]
		public string Password { get; set; }

		[Column(Name = "Role_ID")]
		public int Role_ID { get; set; }
	}

	public class UserView : UserDb
	{
		public Role Role => (Role)Role_ID;

		[Display(Name = "Роль")]
		public string RoleName { get; set; }

		[Display(Name = "Подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
		public string PasswordConfirm { get; set; }
	}
}
