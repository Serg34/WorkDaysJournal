using System;
using System.Runtime.Serialization;

namespace Furmanov.Data.Data
{
	[DataContract]
	[Serializable]
	public class LoginPassword
	{
		public LoginPassword() { }

		public LoginPassword(string login, string password)
		{
			Login = login;
			Password = password;
		}
		[DataMember]
		public string Login { get; set; }

		[DataMember]
		public string Password { get; set; }
	}
}
