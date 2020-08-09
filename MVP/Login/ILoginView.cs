using System;

namespace Furmanov.MVP.Login
{
	public interface ILoginView : IView
	{
		event EventHandler<LoginViewModel> Logging;
		event EventHandler<string> DeletingAutoLogin;
		void Update(LoginViewModel viewModel = null);
		void ShowError(string error);
	}
}