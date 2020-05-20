using Furmanov.Dal.Data;

namespace Furmanov.Dal
{
	public class ApplicationUser
	{
		private static ApplicationUser _instance;
		private static readonly object _lockObject = new object();

		private ApplicationUser() { }

		private static ApplicationUser Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						_instance = new ApplicationUser();
					}
				}
				return _instance;
			}
		}

		private User _user;
		public static User User
		{
			get => Instance._user;
			set => Instance._user = value;
		}
	}
}
