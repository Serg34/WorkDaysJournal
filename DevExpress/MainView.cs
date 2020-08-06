﻿using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using Furmanov.Data;
using Furmanov.Data.Data;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Furmanov.Services.UndoRedo;
using Furmanov.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Furmanov.UI
{
	public partial class MainView : XtraForm, IMainView
	{
		#region Fields
		private SalaryPay _currentPay;
		private SalaryPay _prevPay;

		private readonly string _appUserDataFolder =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WorkDaysJournal");

		private bool _updating;
		#endregion

		public MainView()
		{
			try
			{
				InitializeComponent();

				lblVersion.Caption = "Версия: " + Application.ProductVersion;

				gcWorkedDays.Paint += GcWorkedDays_Paint;
			}
			catch (Exception ex)
			{
				ShowError(ex.ToString());
			}
		}

		#region Events
		public event EventHandler Logging;
		public event EventHandler Logout;

		public event EventHandler<MonthEventArgs> ChangedMonth;
		public event EventHandler WorkDaysOnlyClick;
		public event EventHandler AllDaysClick;

		public event EventHandler<UndoRedoEventArgs<SalaryPay>> ChangedSalaryPay;
		public event EventHandler<SalaryPay> SelectionChangingSalaryPay;

		public event EventHandler<WorkedDay> ChangedWorkedDay;

		public event EventHandler DeletingAllDays;

		public event EventHandler<int> Undo;
		public event EventHandler<int> Redo;
		#endregion

		#region Login
		public void UpdateLogin(User user)
		{
			pnMain.BeginInit();

			//Контролы должны быть видимыми до заполнения
			pnMain.Visible =
			btnVedomostTotal.Enabled =
			btnCreateResource.Enabled =
			btnEditResource.Enabled =
			btnDeleteResource.Enabled =
			deMonth.Enabled =
			btnWorkDaysOnly.Enabled =
			btnAllDays.Enabled =
			btnDeleteAllDays.Enabled = user != null;

			pnMain.EndInit();

			btnLogin.Visibility = user != null ? BarItemVisibility.Never : BarItemVisibility.Always;
			btnLogOut.Visibility = user == null ? BarItemVisibility.Never : BarItemVisibility.Always;

			UpdateUndoRedo(Array.Empty<string>(), Array.Empty<string>());

			if (user != null)
			{
				lblUser.Caption = $"Пользователь: {user.Login} / {user.RoleName}";
				TreeSalary_SelectionChange();
			}
			else
			{
				lblUser.Caption = "Вход не выполнен";
			}
		}

		public ILoginView LoginView => new LoginView { Owner = this };

		private void BtnLogin_ItemClick(object sender, ItemClickEventArgs e)
		{
			Logging?.Invoke(this, EventArgs.Empty);
		}

		private void BtnLogOut_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (MessageService.Question("Вы уверены, что хотите выйти?") == DialogResult.Yes)
			{
				SaveControlsLatoutToXml();
				Logout?.Invoke(this, EventArgs.Empty);
			}
		}
		#endregion

		#region TopMenu
		private void DeMonth_EditValueChanged(object sender, EventArgs e)
		{
			if (_updating) return;
			if (!(deMonth.EditValue is DateTime date)) return;
			ChangedMonth?.Invoke(this, new MonthEventArgs(date.Year, date.Month));
		}
		private void BtnWorkDaysOnly_ItemClick(object sender, ItemClickEventArgs e)
		{
			WorkDaysOnlyClick?.Invoke(this, EventArgs.Empty);
		}
		private void BtnAllDays_ItemClick(object sender, ItemClickEventArgs e)
		{
			AllDaysClick?.Invoke(this, EventArgs.Empty);
		}
		private void BtnDeleteAllDays_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeletingAllDays?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region Update
		public void UpdateMonth(object sender, MonthEventArgs e)
		{
			try
			{
				_updating = true;
				deMonth.EditValue = new DateTime(e.Year, e.Month, 1);
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
		public void UpdateSalaries(object sender, MainViewModel viewModel)
		{
			try
			{
				_updating = true;

				using (new TreeListStateSaver(treeSalary))
				{
					treeSalary.DataSource = viewModel.SalaryPays;
				}

				TreeSalary_SelectionChange();
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
		public void UpdateDays(List<WorkedDay> days)
		{
			using (new GridViewStateSaver(gvWorkedDays))
			{
				gcWorkedDays.DataSource = days;
			}
		}
		public void UpdateUndoRedo(IEnumerable<string> undoItems, IEnumerable<string> redoItems)
		{
			btnUndo.Enabled = undoItems.Any();
			btnUndo.ImageOptions.Image = btnUndo.Enabled ? Resources.undo : Resources.UndoNoEnabled;
			menuUndo.ItemLinks.Clear();
			menuUndo.ItemLinks.AddRange(undoItems.Select(i =>
			{
				var item = new BarButtonItem { Caption = i, Alignment = BarItemLinkAlignment.Left };
				item.ItemClick += (sender, args) =>
					Undo?.Invoke(item, menuUndo.ItemLinks.IndexOf(
						menuUndo.ItemLinks.First(l => l.Item == item)) + 1);
				return item;
			}));

			btnRedo.Enabled = redoItems.Any();
			btnRedo.ImageOptions.Image = btnRedo.Enabled ? Resources.redo : Resources.RedoNoEnabled;
			menuRedo.ItemLinks.Clear();
			menuRedo.ItemLinks.AddRange(redoItems.Select(i =>
			{
				var item = new BarButtonItem { Caption = i, Alignment = BarItemLinkAlignment.Left };
				item.ItemClick += (sender, args) =>
					Redo?.Invoke(item, menuRedo.ItemLinks.IndexOf(
						menuRedo.ItemLinks.First(l => l.Item == item)) + 1);
				return item;
			}));
		}
		#endregion

		#region TreeSalary
		private void TreeSalary_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
			_prevPay = _currentPay.Clone();
		}
		private void TreeSalary_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			ChangedSalaryPay?.Invoke(this,
				new UndoRedoEventArgs<SalaryPay>(_currentPay, _prevPay));
		}
		private void TreeSalary_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			try
			{
				var fieldName = treeSalary.FocusedColumn.FieldName;
				var year = _currentPay.Year;
				var month = _currentPay.Month;
				ValidateService.Validate(e, new SalaryPayValidator(year, month), _prevPay, _currentPay, fieldName);
			}
			catch (Exception ex)
			{
				e.ErrorText = ex.Message;
				e.Valid = false;
			}
		}

		[DebuggerStepThrough]
		private void TreeSalary_ShowingEditor(object sender, CancelEventArgs e)
		{
			if (sender is TreeList view &&
				view.GetFocusedRow() is SalaryPay vm)
			{
				e.Cancel = vm.Type != ObjType.Salary;
			}
		}

		private void TreeSalary_DoubleClick(object sender, EventArgs e)
		{
			if (_currentPay.Type == ObjType.Salary)
			{
				ShowNoImplementedCode(this, null);
			}
		}
		private void DeleteSalaryPay()
		{
			if (_currentPay.Type != ObjType.Salary) return;
			var resName = _currentPay.Name;
			if (_currentPay.FactDays > 0)
			{
				MessageService.Message($"Удаление сотрудника '{resName}' невозможно, так как у него есть отработанные дни.\n" +
										   "Удалите отработанные дни сотрудника и повторите попытку.");
				return;
			}

			if (MessageService.Question($"Будет удален сотрудник '{resName}'.\nПродолжить?") == DialogResult.Yes)
			{
				ShowNoImplementedCode(this, null);
			}
		}

		private void TreeSalary_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			TreeSalary_SelectionChange();
		}
		private void TreeSalary_Enter(object sender, EventArgs e)
		{
			TreeSalary_SelectionChange();
		}
		private void TreeSalary_SelectionChange()
		{
			if (treeSalary.GetFocusedRow() is SalaryPay vm)
			{
				_currentPay = vm;

				btnCreateResource.Enabled
					= btnEditResource.Enabled
					= btnVedomostForObject.Enabled
					= vm.Type == ObjType.Object ||
					  vm.Type == ObjType.Salary;

				btnDeleteResource.Enabled
					= btnAllDays.Enabled
					= btnWorkDaysOnly.Enabled
					= btnDeleteAllDays.Enabled
					= vm.Type == ObjType.Salary;

				if (!_updating)
				{
					SelectionChangingSalaryPay?.Invoke(this, vm);
				}
			}
		}

		[DebuggerStepThrough]
		private void TreeSalary_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (sender is TreeList view &&
				view.GetRow(e.Node.Id) is SalaryPay vm)
			{
				if (vm.Type == ObjType.Project)
				{
					e.Appearance.BackColor = Color.FromArgb(150, 255, 190, 64);
					e.Appearance.BorderColor = Color.FromArgb(255, 121, 124, 145);
					e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
				}
				else if (vm.Type == ObjType.Object)
				{
					if (e.Column != colName && e.Column != colPhone)
					{
						e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Underline);
					}

					e.Appearance.BackColor = Color.FromArgb(150, 213, 238, 255);
					e.Appearance.BorderColor = Color.FromArgb(255, 150, 153, 169);
				}
				else if (vm.Type == ObjType.Salary)
				{
				}
			}
		}
		[DebuggerStepThrough]
		private void TreeSalary_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
		{
			if (sender is TreeList view &&
				view.GetRow(e.Node.Id) is SalaryPay vm &&
				vm.Type != ObjType.Salary)
			{
				if (e.Info.Appearance.Options.UseBorderColor)
				{
					var b = e.Bounds;
					b.Inflate(1, 1);
					e.Cache.DrawRectangle(e.Cache.GetPen(Color.FromArgb(25, e.Info.Appearance.BorderColor)), b);
					e.Cache.DrawLine(e.Cache.GetPen(e.Info.Appearance.BorderColor),
						new Point(b.Left, b.Top), new Point(b.Right, b.Top));
					e.Cache.DrawLine(e.Cache.GetPen(e.Info.Appearance.BorderColor),
						new Point(b.Left, b.Bottom - 1), new Point(b.Right, b.Bottom - 1));
				}
			}
		}
		#endregion

		#region GvWorkedDays
		private void GvWorkedDays_CellValueChanging(object sender,
			DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (sender is GridView view && view.GetRow(e.RowHandle) is WorkedDay vm)
			{
				ChangedWorkedDay?.Invoke(this, vm);
			}
		}

		[DebuggerStepThrough]
		private void GvWorkedDays_RowStyle(object sender, RowStyleEventArgs e)
		{
			if (sender is GridView view &&
				view.GetRow(e.RowHandle) is WorkedDay vm &&
				(vm.Date.DayOfWeek == DayOfWeek.Saturday ||
				 vm.Date.DayOfWeek == DayOfWeek.Sunday))
			{
				e.Appearance.BackColor = Color.FromArgb(255, 103, 103);
				e.Appearance.BorderColor = Color.FromArgb(200, 121, 124, 145);
				e.HighPriority = true;
			}
		}
		private void GcWorkedDays_Paint(object sender, PaintEventArgs e)
		{
			if (gvWorkedDays.GetViewInfo() is GridViewInfo viewInfo)
			{
				foreach (var rowInfo in viewInfo.RowsInfo)
				{
					if (rowInfo.Appearance.Options.UseBorderColor)
					{
						using (var pen = new Pen(rowInfo.Appearance.BorderColor))
						{
							var bounds = rowInfo.TotalBounds;
							bounds.Offset(0, -1);
							e.Graphics.DrawRectangle(pen, bounds);
						}
					}
				}
			}
		}
		#endregion

		#region Form service
		private void TreeSalary_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete ||
				e.KeyCode == Keys.OemMinus ||
				e.KeyCode == Keys.Subtract)
			{
				DeleteSalaryPay();
			}
			else if (e.KeyCode == Keys.Insert || e.KeyCode == Keys.Add)
			{
				ShowNoImplementedCode(this, null);
			}
			else if (e.Control && e.Shift && e.KeyCode == Keys.Z) Redo?.Invoke(this, 1);
			else if (e.Control && e.KeyCode == Keys.Z) Undo?.Invoke(this, 1);
		}
		public void ShowError(string error)
		{
			MessageService.Error(error);
		}

		private void ShowNoImplementedCode(object sender, ItemClickEventArgs e)
		{
			MessageService.Message("Раньше здесь был не очень интересный код");
		}
		private void MainView_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveControlsLatoutToXml();
		}
		private void SaveControlsLatoutToXml()
		{
			if (!Directory.Exists(_appUserDataFolder))
			{
				Directory.CreateDirectory(_appUserDataFolder);
			}

			menuMain.SaveLayoutToXml(Path.Combine(_appUserDataFolder, "Ribbon.xml"));

			var user = ApplicationUser.User;
			if (user != null)
			{
				var treeFile = Path.Combine(_appUserDataFolder, $"treeSalaryPay_user({user.Id})");
				treeSalary.SaveLayoutToXml($"{treeFile} DevState.xml");
				new TreeListStateSaver(treeSalary).SaveLayoutToXml($"{treeFile} NodeState.xml");
			}
		}
		private void MenuUndo_PaintMenuBar(object sender, BarCustomDrawEventArgs e)
		{
			if (sender is PopupMenu menu)
			{
				var focused = menu.ItemLinks.FirstOrDefault(i => i.ScreenBounds.Contains(MousePosition));
				if (focused == null) return;

				var index = menu.ItemLinks.IndexOf(focused);
				for (int i = 0; i < menu.ItemLinks.Count; i++)
				{
					if (i < index)
					{
						using (var brush = new SolidBrush(Color.FromArgb(50, 90, 173, 228)))
						{
							e.Graphics.FillRectangle(brush, menu.ItemLinks[i].Bounds);
						}
					}
				}
			}
		}
		#endregion
	}
}