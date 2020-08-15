using Furmanov.Data;
using Furmanov.Data.Data;
using Furmanov.Services;
using System;
using System.Linq;

namespace Furmanov.MVP.Login
{
	public interface ILoginModel
	{
		event EventHandler<LoginViewModel> Updated;
		event EventHandler Logged;
		event EventHandler SqlConnectingError;
		event EventHandler<string> Error;
		bool LoginChecked { get; }
		User User { get; }

		void Update(bool isStartApp);
		void Login(object sender, LoginViewModel viewModel);
		void DeleteAutoLogin(object sender, string login);
	}
	public class LoginModel : ILoginModel
	{
		private readonly IDataAccessService _db;
		private LoginViewModel _viewModel;


		public LoginModel(IDataAccessService dataAccessService)
		{
			_db = dataAccessService;
		}

		public event EventHandler<LoginViewModel> Updated;
		public event EventHandler Logged;
		public event EventHandler SqlConnectingError;
		public event EventHandler<string> Error;
		public bool LoginChecked { get; private set; }
		public User User { get; private set; }
		public void Update(bool isStartApp)
		{
			_viewModel = new LoginViewModel { CanLogin = isStartApp };
			var loginsPass = _db.GetAutoLoginPassword();
			var loginPass = loginsPass.FirstOrDefault();
			if (loginPass != null)
			{
				_viewModel.AutoLogins = loginsPass;
				_viewModel.Login = loginPass.Login;
				_viewModel.Password = loginPass.Password;
				_viewModel.IsRememberLogin = loginPass.Login.NoEmpty();
				_viewModel.IsRememberPassword = loginPass.Password.NoEmpty();
				if (_viewModel.IsRememberLogin && _viewModel.IsRememberPassword)
				{
					Login(this, _viewModel);
				}
			}
			Updated?.Invoke(this, _viewModel);
		}

		public void Login(object sender, LoginViewModel viewModel)
		{
			try
			{
				_viewModel = viewModel;
				var validateResult = new LoginValidator().Validate(_viewModel);
				if (!validateResult.IsValid)
				{
					Error?.Invoke(this, string.Join("\n", validateResult.Errors));
					return;
				}

				var user = _db.GetUser(_viewModel.Login, _viewModel.Password);
				if (user != null)
				{
					User = ApplicationUser.User = user;
					var loginPass = new LoginPassword();
					if (_viewModel.IsRememberLogin)
					{
						loginPass.Login = _viewModel.Login;
						if (_viewModel.IsRememberPassword) loginPass.Password = _viewModel.Password;
					}

					_db.SaveAutoLoginPassword(loginPass);
					LoginChecked = true;

					if (_viewModel.CanLogin)
					{
						Logged?.Invoke(this, EventArgs.Empty);
					}
				}
				else
				{
					User = ApplicationUser.User = null;
					LoginChecked = false;

					Error?.Invoke(this, "Неверный логин или пароль");
				}
			}
			catch (System.Data.SqlClient.SqlException)
			{
				SqlConnectingError?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				Error?.Invoke(this, ex.ToString());
			}
		}

		public void DeleteAutoLogin(object sender, string login)
		{
			_db.DeleteAutoLogin(login);
			Update(false);
		}
	}
}
