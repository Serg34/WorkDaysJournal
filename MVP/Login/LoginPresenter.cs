﻿namespace Furmanov.MVP.Login
{
	public class LoginPresenter
	{
		private readonly ILoginModel _model;
		private readonly ILoginView _view;

		public LoginPresenter(ILoginModel model, ILoginView view)
		{
			_model = model;
			_view = view;

			_model.Updated += (sender, viewModel) => _view.Update(viewModel);
			_model.SqlConnectingError += (sender, args) => _view.Hide();
			_model.Error += (sender, error) => _view.ShowError(error);
			_model.Logged += (sender, user) => _view.Hide();

			_view.Logging += (sender, args) => _model.Login(args);
			_view.DeletingAutoLogin += (sender, login) => _model.DeleteAutoLogin(login);
		}

		public void ShowView(bool isStartApp)
		{
			_model.Update(isStartApp);

			if (!isStartApp || !_model.LoginChecked)
			{
				_view.ShowDialog();
			}
		}
	}
}
