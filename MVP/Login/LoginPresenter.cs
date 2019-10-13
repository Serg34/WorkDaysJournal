namespace Furmanov.MVP.Login
{
	public class LoginPresenter
	{
		private readonly ILoginModel _model;
		private readonly ILoginView _view;

		public LoginPresenter(ILoginModel model, ILoginView view, bool isStartApp)
		{
			_model = model;
			_view = view;

			_model.Updated += (sender, viewModel) => _view.Update(viewModel);
			_model.Error += (sender, error) => _view.ShowError(error);
			_model.Logged += (sender, user) => _view.Close();
			_model.Update(isStartApp);

			_view.Logging += (sender, args) => _model.Login(sender, args);

			if (!isStartApp || !_model.LoginChecked)
			{
				_view.ShowDialog();
			}
		}
	}
}
