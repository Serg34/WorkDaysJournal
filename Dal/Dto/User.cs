﻿using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

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

		[Column(Name = "RoleId")]
		public int RoleId { get; set; }
	}

	public class UserViewModel : UserDb
	{
		public Role Role => (Role)RoleId;

		[Display(Name = "Роль")]
		public string RoleName { get; set; }
	}
}
