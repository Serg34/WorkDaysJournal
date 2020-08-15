using System.Security.Claims;
using Furmanov.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Furmanov.Models
{
	public class UserContext : DbContext
	{
		public DbSet<UserDto> User { get; set; }
		public UserContext(DbContextOptions<UserContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		public async Task<UserDto> GetUserAsync(ClaimsPrincipal user)
		{
			var login = user.Identity.Name;
			var userDto = await User.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Login == login);

			return userDto;
		}
		public async Task<UserDto> GetUserAsync(string login, string password)
		{
			var user = await User.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);

			return user;
		}
	}
}
