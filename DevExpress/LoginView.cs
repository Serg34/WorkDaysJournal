using DevExpress.XtraEditors;
using Furmanov.MVP.Login;
using Services.UI;
using System;

namespace Furmanov.UI
{
	public partial class LoginView : XtraForm, ILoginView
	{
		private bool _updating;
		private LoginViewModel _vm;
		public LoginView()
		{
			InitializeComponent();
		}
		public event EventHandler<LoginViewModel> Logging;

		public void Update(LoginViewModel viewModel = null)
		{
			try
			{
				_updating = true;
				if (viewModel != null) _vm = viewModel;
				tbLogin.Text = _vm.Login;
				tbPass.Text = _vm.Password;
				checkRemember.Checked = checkAutoLoginOnStart.Enabled = _vm.IsRemember;
				checkAutoLoginOnStart.Checked = _vm.IsAutoLoginOnStart;
			}
			catch (Exception ex)
			{
				ShowError(ex.ToString());
			}
			finally
			{
				_updating = false;
			}
		}
		private void btnOk_Click(object sender, EventArgs e)
		{
			_vm.CanLogin = true;
			Logging?.Invoke(this, _vm);
		}

		private void checkRemember_CheckedChanged(object sender, EventArgs e)
		{
			if (_updating) return;
			_vm.IsRemember = checkRemember.Checked;
			Update();
		}
		private void CheckAutoLoginOnStart_CheckedChanged(object sender, EventArgs e)
		{
			if (_updating) return;
			_vm.IsAutoLoginOnStart = checkAutoLoginOnStart.Checked;
			Update();
		}

		private void TbLogin_EditValueChanged(object sender, EventArgs e)
		{
			if (_updating) return;
			_vm.Login = tbLogin.Text?.Trim();
			Update();
		}

		private void TbPass_EditValueChanged(object sender, EventArgs e)
		{
			if (_updating) return;
			_vm.Password = tbPass.Text?.Trim();
			Update();
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		public void ShowError(string error)
		{
			MessageService.ShowError(error);
		}
	}
}