using AutoMapper;
using Furmanov.Data.Data;

namespace Furmanov.Services
{
	public class MapperService
	{
		private static readonly MapperConfiguration Config;
		static MapperService()
		{
			Config = new MapperConfiguration(
				cfg =>
				{
					cfg.CreateMap<UserDto, User>();
					cfg.CreateMap<User, UserDto>();
					cfg.CreateMap<BugDto, Bug>();
				});
		}
		public static T Map<T>(object obj) => Config.CreateMapper().Map<T>(obj);
	}
}
