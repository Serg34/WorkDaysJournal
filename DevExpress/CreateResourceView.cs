using DevExpress.XtraEditors;
using Furmanov.MVP;
using Furmanov.MVP.CreateResource;
using Furmanov.MVP.MainView.ViewModels;
using Furmanov.MVP.Services.UI;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Furmanov.UI
{
	public partial class CreateResourceView : XtraForm, ICreateResourceView
	{
		private readonly CreateResourceViewModel _viewModel = new CreateResourceViewModel { IsStaff = true };
		public CreateResourceView(ResOPViewModel vm)
		{
			InitializeComponent();
			G.OnError += error => ShowError($"Ошибка в базе данных:\n{error}");
			lblObject.Text = $"Объект: {vm.ObjectNameForResOp}";
		}

		public event EventHandler<CreateResourceViewModel> Creating;
		private void BtOk_Click(object sender, EventArgs e)
		{
			Creating?.Invoke(this, _viewModel);
		}

		private void TbName_EditValueChanged(object sender, EventArgs e)
		{
			_viewModel.Name = tbName.Text;
		}
		private void TbName_KeyPress(object sender, KeyPressEventArgs e)
		{
			var length = tbName.Text.Length;
			tbName.Text = string.Concat(tbName.Text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)));
			if (tbName.Text.Length < length)
			{
				tbName.SelectionStart = tbName.Text.Length + 1;
			}
		}
		private void TbName_KeyUp(object sender, KeyEventArgs e)
		{
			var length = tbName.Text.Length;
			tbName.Text = string.Concat(tbName.Text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)));
			if (tbName.Text.Length < length)
			{
				tbName.SelectionStart = tbName.Text.Length + 1;
			}
		}
		private void TbPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{
			_viewModel.Phone = tbPhone.Text;
		}
		private void TbCard_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{
			_viewModel.Card = tbCard.Text;
		}
		private void TbOfficialSalary_EditValueChanged(object sender, EventArgs e)
		{
			_viewModel.OfficialSalary = tbOfficialSalary.Text;
		}

		private void CheckFreeLance_CheckedChanged(object sender, EventArgs e)
		{
			_viewModel.IsStaff = !checkFreeLance.Checked;
		}

		private void BtCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		public void ShowError(string error)
		{
			MessageService.ShowError(error);
		}
	}
}