using Furmanov.Data.Data;

namespace Furmanov.Data
{
	public class ApplicationUser
	{
		private static ApplicationUser _instance;
		private static readonly object LockObject = new object();

		private ApplicationUser() { }

		private static ApplicationUser Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (LockObject)
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
