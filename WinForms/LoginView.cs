using System;
using System.Linq;
using System.Windows.Forms;
using Furmanov.Data.Data;
using Furmanov.MVP.Login;
using Furmanov.Services;

namespace Furmanov.UI
{
	public partial class LoginView : Form, ILoginView
	{
		private LoginViewModel _vm;
		public LoginView()
		{
			InitializeComponent();
		}
		public event EventHandler<LoginViewModel> Logging;
		public event EventHandler<string> DeletingAutoLogin;

		public void Update(LoginViewModel viewModel = null)
		{
			try
			{
				if (viewModel == null) return;
				_vm = viewModel;
				cbLogins.Items.Clear();
				if (_vm.AutoLogins.NoEmpty())
				{
					cbLogins.Items.AddRange(_vm.AutoLogins.Select(l => l.Login).ToArray());
				}
				var currentLoginPass = _vm.AutoLogins?.FirstOrDefault();
				cbLogins.Text = currentLoginPass?.Login;
				SelectedLoginPasswordChange(currentLoginPass);
				btDeleteLogin.Enabled = _vm.AutoLogins.NoEmpty();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				_vm.Login = cbLogins.Text?.Trim();
				_vm.Password = tbPass.Text?.Trim();
				_vm.CanLogin = true;
				_vm.IsRememberLogin = chbIsRememberLogin.Checked;
				_vm.IsRememberPassword = chbIsRememberPassword.Checked;
				Logging?.Invoke(this, _vm);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void SelectedLoginPasswordChange(LoginPassword loginPass)
		{
			tbPass.Text = loginPass?.Password;
			chbIsRememberLogin.Checked =
			chbIsRememberPassword.Enabled = loginPass?.Login.NoEmpty() ?? false;
			chbIsRememberPassword.Checked = loginPass?.Password.NoEmpty() ?? false;
		}
		private void cbLogins_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				var index = cbLogins.SelectedIndex;
				var loginPass = index >= 0 ? _vm.AutoLogins[index] : null;
				SelectedLoginPasswordChange(loginPass);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		private void btDeleteLogin_Click(object sender, EventArgs e)
		{
			try
			{
				var index = cbLogins.SelectedIndex;
				var login = index >= 0 ? _vm.AutoLogins[index].Login : null;
				var q = $"Удалить логин '{login}' из выпадающего списка?";
				if (MessageService.Question(q) != DialogResult.Yes) return;

				DeletingAutoLogin?.Invoke(this, login);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		private void chbIsRememberLogin_CheckedChanged(object sender, EventArgs e)
		{
			chbIsRememberPassword.Enabled = chbIsRememberLogin.Checked;
			if (!chbIsRememberLogin.Checked) chbIsRememberPassword.Checked = false;
		}
		public void ShowError(Exception ex) => MessageService.Error(ex.ToString());
		public void ShowError(string error) => MessageService.Error(error);
	}
}
