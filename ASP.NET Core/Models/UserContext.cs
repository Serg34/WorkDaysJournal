using Furmanov.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
	}
}
