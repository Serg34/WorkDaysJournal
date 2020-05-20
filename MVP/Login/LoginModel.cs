﻿using Furmanov.Data;
using Furmanov.Data.Data;
using System;

namespace Furmanov.MVP.Login
{
	public interface ILoginModel
	{
		event EventHandler<string> Error;
		event EventHandler<LoginViewModel> Updated;
		event EventHandler Logged;
		bool LoginChecked { get; }

		void Update(bool isStartApp);
		void Login(object sender, LoginViewModel viewModel);
	}
	public class LoginModel : ILoginModel
	{
		private readonly IDataAccessService _db;
		private LoginViewModel _viewModel;

		public bool LoginChecked { get; private set; }

		public LoginModel(IDataAccessService dataAccessService)
		{
			_db = dataAccessService;
		}

		public event EventHandler<LoginViewModel> Updated;
		public event EventHandler Logged;
		public event EventHandler<string> Error;

		public void Update(bool isStartApp)
		{
			_viewModel = new LoginViewModel { CanLogin = isStartApp };
			var loginPass = _db.GetAutoLoginPassword();
			if (loginPass != null)
			{
				_viewModel.Login = loginPass.Login;
				_viewModel.Password = loginPass.Password;
				_viewModel.IsRemember = !string.IsNullOrEmpty(loginPass.Login);
				_viewModel.IsAutoLoginOnStart = !string.IsNullOrEmpty(loginPass.Password);
				if (_viewModel.IsRemember && _viewModel.IsAutoLoginOnStart)
				{
					Login(this, _viewModel);
				}
			}
			Updated?.Invoke(this, _viewModel);
		}

		public void Login(object sender, LoginViewModel viewModel)
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
				ApplicationUser.User = user;
				var loginPass = new LoginPassword();
				if (_viewModel.IsRemember)
				{
					loginPass.Login = _viewModel.Login;
					if (_viewModel.IsAutoLoginOnStart) loginPass.Password = _viewModel.Password;
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
				ApplicationUser.User = null;
				LoginChecked = false;

				Error?.Invoke(this, "Неверный логин или пароль");
			}
		}
	}
}
