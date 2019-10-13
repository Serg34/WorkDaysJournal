using DevExpress.XtraEditors;
using Furmanov.Dal;
using Furmanov.Dal.Dto;
using Furmanov.MVP;
using Furmanov.MVP.MainView.ViewModels;
using Furmanov.MVP.Services.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Furmanov.MVP.EditResource;

namespace Furmanov.UI
{
	public partial class EditResourceView : XtraForm, IEditResourceView
	{
		private readonly IdataAccessService _db;
		private readonly int _objectId;
		private readonly DateTime _month;
		private List<Employee> _resources;
		private readonly List<Position> _positions;

		public EditResourceView(ResOPViewModel vm)
		{
			InitializeComponent();

			_db = new DataAccessService();
			G.OnError += error => ShowError($"Ошибка в базе данных:\n{error}");

			_objectId = vm.Object_Id;
			lblObject.Text = $"Объект: {vm.ObjectNameForResOp}";
			_month = vm.Month;

			_resources = _db.GetResources(SelectionResourceMode.Staff, _objectId);
			_positions = _db.GetPosition(_objectId);

			cbResource.Properties.Items
				.AddRange(_resources?.Select(r => r.Name).ToArray());
			cbPosition.Properties.Items
				.AddRange(_positions?.Select(r => r.Name).ToArray());
		}

		public SalaryPay ResOp { get; private set; }
		public string ResourceName { get; private set; }

		private void BtnSave_Click(object sender, EventArgs e)
		{
			if (cbResource.SelectedIndex >= 0 && cbPosition.SelectedIndex >= 0)
			{
				var resource = _resources[cbResource.SelectedIndex];
				var position = _positions[cbPosition.SelectedIndex];
				ResOp = new SalaryPay
				{
					Month = _month,
					Object_Id = _objectId,
					Resource_Id = resource.Id,
					Position_Id = position.Id,
				};
				ResourceName = resource.Name;

				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				lblErrorCreate.Visible = true;
			}
		}

		private void CbResource_SelectedIndexChanged(object sender, EventArgs e)
		{
			var resource = _resources[cbResource.SelectedIndex];
			tbPhone.Text = resource.Phone;
			tbCard.Text = resource.Card;
			tbOfficialSalary.Text = resource.OfficialSalary?.ToString(CultureInfo.CurrentCulture);
		}

		private void CbPosition_SelectedIndexChanged(object sender, EventArgs e)
		{
			var position = _positions[cbPosition.SelectedIndex];
			tbSalary.Text = position.Salary.ToString(CultureInfo.CurrentCulture);
		}

		private void CheckFreelance_CheckedChanged(object sender, EventArgs e)
		{
			_resources = checkFreeLance.Checked
				? _db.GetResources(SelectionResourceMode.Freelance, _objectId)
				: _db.GetResources(SelectionResourceMode.Staff, _objectId);

			cbResource.Properties.Items.Clear();
			cbResource.SelectedIndex = -1;

			tbPhone.ResetText();
			tbCard.ResetText();

			if (_resources.Count > 0)
			{
				cbResource.Properties.Items
					.AddRange(_resources?.Select(r => r.Name).ToArray());
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		public void ShowError(string error)
		{
			MessageService.ShowError(error);
		}
	}
}