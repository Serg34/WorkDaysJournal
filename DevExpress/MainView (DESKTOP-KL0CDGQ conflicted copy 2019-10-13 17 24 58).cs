using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using Force.DeepCloner;
using Furmanov.Dal;
using Furmanov.Dal.Dto;
using Furmanov.MVP;
using Furmanov.MVP.MainView.ViewModels;
using Furmanov.MVP.Services.UI;
using Furmanov.MVP.Services.UndoRedo;
using Furmanov.UI.Properties;
using Furmanov.UI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Furmanov.MVP.MainView;

namespace Furmanov.UI
{
	public partial class MainView : XtraForm, IMainView
	{
		#region Fields
		private ResOPViewModel _currentResOpViewModel;
		private ResOPViewModel _currentResOpViewModelBeforeChanged;

		private readonly string _appUserDataFolder =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SC.Tabel");

		private bool _updating;
		#endregion

		public MainView()
		{
			try
			{
				InitializeComponent();

				G.OnError += error => ShowError($"Ошибка в базе данных:\n{error}");
#if DEBUG
				WindowState = FormWindowState.Maximized;
#endif
				lblVersion.Caption = "Версия: " + Application.ProductVersion;

				riMonths.Items.Add("Текущий");
				if (DateTime.Today.Day <= 15)
				{
					riMonths.Items.Add("Предыдущий");
				}

				cbMonth.EditValue = "Текущий";

				var f = Path.Combine(_appUserDataFolder, "Ribbon.xml");
				if (File.Exists(f)) menuMain.RestoreLayoutFromXml(f);

				gcTabels.Paint += GcTabels_Paint;
			}
			catch (Exception ex)
			{
				ShowError(ex.ToString());
			}
		}

		#region Events
		public event EventHandler Logging;
		public event EventHandler Logout;

		public event EventHandler<DateTime> ChangedMonth;
		public event EventHandler WorkDaysOnlyClick;
		public event EventHandler AllDaysClick;

		public event EventHandler<UndoRedoEventArgs<ResOPViewModel>> ChangedResOp;
		public event EventHandler<UndoRedoEventArgs<ResOPViewModel>> ReplacingResource;
		public event EventHandler EditingResource;
		public event EventHandler<ResOPViewModel> SelectResource;

		public event EventHandler<TabelViewModel> ChangedTabel;

		public event EventHandler<ResOPViewModel> CreatingResource;
		public event EventHandler<ResOPViewModel> DeletingResOp;
		public event EventHandler<int> VedomostClick;
		public event EventHandler DeletingAllDays;

		public event EventHandler<int> Undo;
		public event EventHandler<int> Redo;
		#endregion

		#region Login
		public void UpdateLogin(object sender, UserView user)
		{
			pnTabelMain.BeginInit();

			pnTabelMain.Visible =
				btnVedomostTotal.Enabled //Контролы должны быть видимыми до заполнения
				= btnCreateResource.Enabled = btnEditResource.Enabled = btnDeleteResource.Enabled
				= cbMonth.Enabled
				= btnWorkDaysOnly.Enabled = btnAllDays.Enabled = btnDeleteAllDays.Enabled
				= user != null;

			pnTabelMain.EndInit();

			btnLogin.Visibility = user != null ? BarItemVisibility.Never : BarItemVisibility.Always;
			btnLogOut.Visibility = user == null ? BarItemVisibility.Never : BarItemVisibility.Always;

			UpdateUndoRedo(Array.Empty<string>(), Array.Empty<string>());

			if (user != null)
			{
				lblUser.Caption = $"Пользователь: {user.Login} / {user.Login} / {user.RoleName}";

				var treeFile = Path.Combine(_appUserDataFolder, $"treeResOp_user({user.Id})");
				if (File.Exists($"{treeFile} DevState.xml"))
				{
					treeResOp.RestoreLayoutFromXml($"{treeFile} DevState.xml");
					using (var saver = new TreeListStateSaver(treeResOp))
					{
						saver.RestoreLayoutFromXml($"{treeFile} NodeState.xml");
					}
				}
				SelectionResOpChange();
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
			if (MessageService.ShowQuestion("Вы уверены, что хотите выйти?") == DialogResult.Yes)
			{
				SaveControlsLatoutToXml();
				Logout?.Invoke(this, EventArgs.Empty);
			}
		}
		#endregion

		#region TopMenu
		public ICreateResourceView CreateResourceView
		{
			get => new CreateResourceView(_currentResOpViewModel) { Owner = this };
		}
		private void BtnCreateResource_ItemClick(object sender, ItemClickEventArgs e)
		{
			CreatingResource?.Invoke(this, _currentResOpViewModel);
		}
		public IEditResourceView EditResourceView
		{
			get => new EditResourceView(_currentResOpViewModel) { Owner = this };
		}
		private void BtnEditResource_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditingResource?.Invoke(this, EventArgs.Empty);
		}
		private void BtnDeleteResource_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteResOp();
		}

		private void CbMonth_EditValueChanged(object sender, EventArgs e)
		{
			if (_updating) return;
			ChangedMonth?.Invoke(this,
				cbMonth.EditValue.Equals("Текущий")
					? DateTime.Now
					: DateTime.Now.AddMonths(-1));
		}

		private void BtnVedomostTotal_ItemClick(object sender, ItemClickEventArgs e)
		{
			VedomostClick?.Invoke(this, 0);
		}
		private void BtnVedomostForObject_ItemClick(object sender, ItemClickEventArgs e)
		{
			VedomostClick?.Invoke(this, _currentResOpViewModel.ObjectId);
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
		public void UpdateAllResOps(object sender, MainViewModel viewModel)
		{
			try
			{
				_updating = true;

				using (new TreeListStateSaver(treeResOp))
				{
					treeResOp.DataSource = viewModel.ResOps;
				}

				SelectionResOpChange();
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
		public void UpdateSelectedResOp(object sender, SelectionResOpViewModel viewModel)
		{
			riPositions.Items.Clear();
			riPositions.Items.AddRange(viewModel.PositionNames ?? Array.Empty<object>());

			riResourceNames.Items.Clear();
			riResourceNames.Items.AddRange(viewModel.ResourceNames ?? Array.Empty<object>());

			using (new GridViewStateSaver(gridTabels))
			{
				gcTabels.DataSource = viewModel.Tabels ?? new TabelViewModel[0];
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

		#region TreeResOp
		private void TreeResOp_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == colName)
			{
				if (_currentResOpViewModel.FactDays > 0)
				{
					MessageService.ShowMessage($"Замена сотрудника '{_currentResOpViewModel.Name}' невозможна, " +
											   "так как у него есть отработанные дни.\n" +
											   "Удалите отработанные дни сотрудника и повторите попытку.");
				}
			}
			_currentResOpViewModelBeforeChanged = _currentResOpViewModel.DeepClone();
		}
		private void TreeResOp_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == colName)
			{
				ReplacingResource?.Invoke(this, ResOpArgs);
			}
			else
			{
				ChangedResOp?.Invoke(this, ResOpArgs);
			}
		}

		[DebuggerStepThrough]
		private void TreeResOp_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (sender is TreeList view &&
				view.GetRow(e.Node.Id) is ResOPViewModel vm)
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
				else if (vm.Type == ObjType.ResOP)
				{
				}
			}
		}

		[DebuggerStepThrough]
		private void TreeResOp_ShowingEditor(object sender, CancelEventArgs e)
		{
			if (sender is TreeList view &&
				view.GetRow(view.Selection.FirstOrDefault()?.Id ?? -1) is ResOPViewModel vm)
				e.Cancel = vm.Type != ObjType.ResOP;
		}

		[DebuggerStepThrough]
		private void TreeResOp_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
		{
			if (sender is TreeList view &&
				view.GetRow(e.Node.Id) is ResOPViewModel vm &&
				vm.Type != ObjType.ResOP)
			{
				if (e.Column == colIsStaff)
				{
					e.Cache.FillRectangle(e.Cache.GetSolidBrush(e.Info.Appearance.BackColor), e.Bounds);
					e.Handled = true;
				}

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

		private void TreeResOp_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			SelectionResOpChange();
		}

		private void TreeResOp_DoubleClick(object sender, EventArgs e)
		{
			if (_currentResOpViewModel.Type == ObjType.ResOP)
			{
				EditingResource?.Invoke(this, EventArgs.Empty);
			}
		}

		private void DeleteResOp()
		{
			if (_currentResOpViewModel.Type != ObjType.ResOP) return;
			var resName = _currentResOpViewModel.Name;
			if (_currentResOpViewModel.FactDays > 0)
			{
				MessageService.ShowMessage($"Удаление сотрудника '{resName}' невозможно, так как у него есть отработанные дни.\n" +
										   "Удалите отработанные дни сотрудника и повторите попытку.");
				return;
			}

			if (MessageService.ShowQuestion($"Будет удален сотрудник '{resName}'.\nПродолжить?") == DialogResult.Yes)
			{
				DeletingResOp?.Invoke(this, _currentResOpViewModel);
			}
		}
		private void TreeResOp_Enter(object sender, EventArgs e)
		{
			SelectionResOpChange();
		}
		private void SelectionResOpChange()
		{
			if (treeResOp.Tag?.Equals(TreeListStateSaver.State.Updating) ?? false) return;
			if (treeResOp.GetRow(treeResOp.FocusedNode?.Id ?? -1) is ResOPViewModel vm)
			{
				_currentResOpViewModel = vm;

				btnCreateResource.Enabled = btnEditResource.Enabled
					= btnVedomostForObject.Enabled
					= vm.Type == ObjType.Object ||
					  vm.Type == ObjType.ResOP;

				btnDeleteResource.Enabled
					= btnAllDays.Enabled = btnWorkDaysOnly.Enabled
					= btnDeleteAllDays.Enabled
					= vm.Type == ObjType.ResOP;

				SelectResource?.Invoke(this, vm);
			}
		}
		#endregion

		#region GridTabels
		private void GridTabels_CellValueChanging(object sender,
			DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (sender is GridView view && view.GetRow(e.RowHandle) is TabelViewModel vm)
			{
				vm.DeepClone();
				ChangedTabel?.Invoke(this, vm);
			}
		}

		[DebuggerStepThrough]
		private void GridTabels_RowStyle(object sender, RowStyleEventArgs e)
		{
			if (sender is GridView view &&
				view.GetRow(e.RowHandle) is TabelViewModel vm &&
				(vm.Date.DayOfWeek == DayOfWeek.Saturday ||
				 vm.Date.DayOfWeek == DayOfWeek.Sunday))
			{
				e.Appearance.BackColor = Color.FromArgb(255, 103, 103);
				e.Appearance.BorderColor = Color.FromArgb(200, 121, 124, 145);
				e.HighPriority = true;
			}
		}
		private void GcTabels_Paint(object sender, PaintEventArgs e)
		{
			if (gridTabels.GetViewInfo() is GridViewInfo viewInfo)
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
		private void TreeResOp_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete ||
				e.KeyCode == Keys.OemMinus ||
				e.KeyCode == Keys.Subtract)
			{
				DeleteResOp();
			}
			else if (e.KeyCode == Keys.Insert || e.KeyCode == Keys.Add)
			{
				CreatingResource?.Invoke(this, _currentResOpViewModel);
			}
			else if (e.Control && e.Shift && e.KeyCode == Keys.Z) Redo?.Invoke(this, 1);
			else if (e.Control && e.KeyCode == Keys.Z) Undo?.Invoke(this, 1);
		}
		public void ShowError(string error)
		{
			MessageService.ShowError(error);
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

			var user = ApplicationUser.Instance.User;
			if (user != null)
			{
				var treeFile = Path.Combine(_appUserDataFolder, $"treeResOp_user({user.Id})");
				treeResOp.SaveLayoutToXml($"{treeFile} DevState.xml");
				new TreeListStateSaver(treeResOp).SaveLayoutToXml($"{treeFile} NodeState.xml");
			}
		}
		private UndoRedoEventArgs<ResOPViewModel> ResOpArgs
		{
			get => new UndoRedoEventArgs<ResOPViewModel>(
						_currentResOpViewModel,
						_currentResOpViewModelBeforeChanged);
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