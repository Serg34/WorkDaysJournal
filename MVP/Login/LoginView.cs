using System;

namespace Furmanov.MVP.Login
{
	public interface ILoginView : IView
	{
		event EventHandler<LoginViewModel> Logging;
		void Update(LoginViewModel viewModel = null);
	}
}