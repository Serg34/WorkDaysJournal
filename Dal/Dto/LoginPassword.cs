using System;
using System.Runtime.Serialization;

namespace Furmanov.Dal.Dto
{
	[DataContract]
	[Serializable]
	public class LoginPassword
	{
		[DataMember]
		public string Login { get; set; }

		[DataMember]
		public string Password { get; set; }
	}
}
