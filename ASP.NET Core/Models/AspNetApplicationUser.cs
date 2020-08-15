using Furmanov.Data.Data;
using Furmanov.Services;
using Microsoft.AspNetCore.Identity;

namespace Furmanov.Models
{
	public class AspNetApplicationUser : IdentityUser<int>
	{
		public string Login { get; set; }

		public string Name { get; set; }

		public string Password { get; set; }

		public Role Role { get; set; }

		public static implicit operator User(AspNetApplicationUser user)
		{
			var res = MapperService.Map<User>(user);
			return res;
		}
	}
}
