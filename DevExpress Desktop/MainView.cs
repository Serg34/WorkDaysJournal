using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using Furmanov.Data.Data;
using Furmanov.MVP;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Furmanov.Services.UndoRedo;
using Furmanov.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Furmanov.UI.Services;

namespace Furmanov.UI
{
	public partial class MainView : DevExpress.XtraBars.Ribbon.RibbonForm, IMainView
	{
		#region Fields
		private SalaryPay _currentPay;
		private SalaryPay _prevPay;
		private bool _updating;
		#endregion

		public MainView()
		{
			try
			{
				InitializeComponent();
				lblVersion.Caption = $"Версия: {Application.ProductVersion}";

				LayoutSaver.Restore(this);

				gcWorkedDays.Paint += GcWorkedDays_Paint;
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		#region Events
		public event EventHandler Logging;
		public event EventHandler Logout;

		public event EventHandler RefillingDataBase;
		public event EventHandler<MonthEventArgs> ChangedMonth;
		public event EventHandler<SalaryPay> WorkDaysOnlyClick;
		public event EventHandler<SalaryPay> AllDaysClick;

		public event EventHandler<UndoRedoEventArgs<SalaryPay>> ChangedSalaryPay;
		public event EventHandler<SalaryPay> SelectionChangingSalaryPay;

		public event EventHandler<WorkedDay> ChangedWorkedDay;

		public event EventHandler<SalaryPay> DeletingAllDays;

		public event EventHandler<int> Undo;
		public event EventHandler<int> Redo;

		public event EventHandler<BugEventArgs> ReportingBug;
		#endregion

		#region Login
		public void UpdateLogin(UserDto user)
		{
			try
			{
				pnMain.BeginInit();

				//Контролы должны быть видимыми до заполнения
				pnMain.Visible =
				btReportTotal.Enabled =
				btCreateResource.Enabled =
				btEditResource.Enabled =
				btDeleteResource.Enabled =
				deMonth.Enabled =
				btWorkDaysOnly.Enabled =
				btAllDays.Enabled =
				btDeleteAllDays.Enabled = user != null;

				pnMain.EndInit();

				btLogin.Visibility = user != null ? BarItemVisibility.Never : BarItemVisibility.Always;
				btLogOut.Visibility = user == null ? BarItemVisibility.Never : BarItemVisibility.Always;

				UpdateUndoRedo(new string[0], new string[0]);

				if (user != null)
				{
					lblUser.Caption = $"Пользователь: {user.Login} | {user.Name} | {user.Role_Id.DisplayName()}";
					TreeSalary_SelectionChange();
				}
				else
				{
					lblUser.Caption = "Вход не выполнен";
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		public ILoginView LoginView => new LoginView { Owner = this };

		private void BtLogin_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				Logging?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		private void BtLogOut_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (MessageService.Question("Вы уверены, что хотите выйти?") == DialogResult.Yes)
				{
					Logout?.Invoke(this, EventArgs.Empty);
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		#endregion

		#region TopMenu
		private void btRefillDataBase_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				var q = "Сгенерировать новые данные в базе данных?\n\n" +
						"Все текущие записи будут удалены.\n\n" +
						"Для отладки будут доступны три учётки:\n-'Admin';\n-'ProjectManager';\n-'Manager'\n" +
						"с соответствующими ролями.\n\n" +
						"Пароль для любой учётки '1'";

				if (MessageService.Question(q) != DialogResult.Yes) return;

				TaskbarProgress.Start(this);
				RefillingDataBase?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
			finally
			{
				TaskbarProgress.Finish(this);
			}
		}

		private void btBugDebug_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				var rnd = new Random();
				var r = rnd.Next(1000);
				if (r < 100) throw new ArgumentException("Test");
				if (r < 200) throw new AggregateException("Test");
				if (r < 300) throw new ApplicationException("Test");
				if (r < 400) throw new ArgumentOutOfRangeException("Test");
				if (r < 500) throw new COMException("Test");
				if (r < 600) throw new BadImageFormatException("Test");
				if (r < 700) throw new DataException("Test");
				if (r < 800) throw new DuplicateWaitObjectException("Test");
				if (r < 900) throw new DivideByZeroException("Test");
				throw new Exception("Test");
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void DeMonth_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (_updating) return;
				if (!(deMonth.EditValue is DateTime date)) return;
				ChangedMonth?.Invoke(this, new MonthEventArgs(date.Year, date.Month));
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void BtWorkDaysOnly_ItemClick(object sender, ItemClickEventArgs e)
		{
			WorkDaysOnlyClick?.Invoke(this, _currentPay);
		}
		private void BtAllDays_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				AllDaysClick?.Invoke(this, _currentPay);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void BtDeleteAllDays_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeletingAllDays?.Invoke(this, _currentPay);
		}
		#endregion

		#region Update
		public void UpdateMonth(object sender, MainViewModel vm)
		{
			try
			{
				_updating = true;
				deMonth.EditValue = new DateTime(vm.Year, vm.Month, 1);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
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
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
			finally
			{
				_updating = false;
			}
		}
		public void UpdateDays(object sender, List<WorkedDay> days)
		{
			try
			{
				using (new GridViewStateSaver(gvWorkedDays))
				{
					gcWorkedDays.DataSource = days;
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		public void UpdateUndoRedo(string[] undoItems, string[] redoItems)
		{
			try
			{
				btUndo.Enabled = undoItems.Any();
				btUndo.ImageOptions.Image = btUndo.Enabled ? Resources.undo : Resources.UndoNoEnabled;
				menuUndo.ItemLinks.Clear();
				menuUndo.ItemLinks.AddRange(undoItems.Select(i =>
				{
					var item = new BarButtonItem { Caption = i, Alignment = BarItemLinkAlignment.Left };
					item.ItemClick += (sender, args) =>
						Undo?.Invoke(item, menuUndo.ItemLinks.IndexOf(
							menuUndo.ItemLinks.First(l => l.Item == item)) + 1);
					return item;
				}));

				btRedo.Enabled = redoItems.Any();
				btRedo.ImageOptions.Image = btRedo.Enabled ? Resources.redo : Resources.RedoNoEnabled;
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
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		#endregion

		#region TreeSalary
		private void TreeSalary_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
			try
			{
				_prevPay = _currentPay.Clone();
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void TreeSalary_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			try
			{
				ChangedSalaryPay?.Invoke(this,
					new UndoRedoEventArgs<SalaryPay>(_currentPay.Clone(), _prevPay));
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void TreeSalary_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			try
			{
				var fieldName = treeSalary.FocusedColumn.FieldName;
				var year = _currentPay.Year;
				var month = _currentPay.Month;
				var vmToValidate = _prevPay.Clone();
				ValidateService.Validate(e, new SalaryPayValidator(year, month), vmToValidate, _currentPay, fieldName);
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
			try
			{
				if (sender is TreeList view &&
				view.GetFocusedRow() is SalaryPay vm)
				{
					e.Cancel = vm.Type != ObjType.SalaryPay;
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		private void TreeSalary_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (!(sender is TreeList treeList)) return;
				var pt = treeList.PointToClient(MousePosition);
				var hitInfo = treeList.CalcHitInfo(pt);
				if (!hitInfo.InRow) return;

				if (_currentPay.Type == ObjType.SalaryPay)
				{
					ShowNoImplementedCode(this, null);
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void DeleteSalaryPay()
		{
			try
			{
				if (_currentPay.Type != ObjType.SalaryPay) return;
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
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
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
			try
			{
				if (!(treeSalary.GetFocusedRow() is SalaryPay pay)) return;

				_currentPay = pay;

				btCreateResource.Enabled =
				btEditResource.Enabled =
				btReportForObject.Enabled = pay.Type == ObjType.Object ||
											pay.Type == ObjType.SalaryPay;

				btDeleteResource.Enabled =
				btAllDays.Enabled =
				btWorkDaysOnly.Enabled =
				btDeleteAllDays.Enabled = pay.Type == ObjType.SalaryPay;

				SelectionChangingSalaryPay?.Invoke(this, pay);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		[DebuggerStepThrough]
		private void TreeSalary_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			try
			{
				if (!(sender is TreeList view) || !(view.GetRow(e.Node.Id) is SalaryPay vm)) return;
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
				else if (vm.Type == ObjType.SalaryPay)
				{
				}
				else if (vm.Type == ObjType.Summary)
				{
					e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
					e.Appearance.BackColor = Color.FromArgb(75, 128, 128, 128);
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		[DebuggerStepThrough]
		private void TreeSalary_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
		{
			try
			{
				if (sender is TreeList view &&
					view.GetRow(e.Node.Id) is SalaryPay vm &&
					vm.Type != ObjType.SalaryPay)
				{
					TreeListService.CustomDrawNodeCell(e);
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		#endregion

		#region GvWorkedDays
		private void GvWorkedDays_CellValueChanging(object sender,
			DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			try
			{
				if (!(sender is GridView view) || !(view.GetRow(e.RowHandle) is WorkedDay vm)) return;
				if (e.Column == colIsWorked)
				{
					vm.IsWorked = !vm.IsWorked;
				}
				ChangedWorkedDay?.Invoke(this, vm);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		[DebuggerStepThrough]
		private void GvWorkedDays_RowStyle(object sender, RowStyleEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void GcWorkedDays_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				if (!(gvWorkedDays.GetViewInfo() is GridViewInfo viewInfo)) return;
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
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		#endregion

		#region Form service
		private void TreeSalary_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Delete ||
					e.KeyCode == Keys.OemMinus ||
					e.KeyCode == Keys.Subtract)
				{
					DeleteSalaryPay();
				}
				else if (e.KeyCode == Keys.Add)
				{
					ShowNoImplementedCode(this, null);
				}
				else if (e.Control && e.Shift && e.KeyCode == Keys.Z) Redo?.Invoke(this, 1);
				else if (e.Control && e.KeyCode == Keys.Z) Undo?.Invoke(this, 1);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		public void Progress(object sender, ProgressEventArgs e)
		{
			try
			{
				TaskbarProgress.SetValue(e.Value, e.Max, this);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		private void ShowNoImplementedCode(object sender, ItemClickEventArgs e)
		{
			MessageService.Message("В реальном приложении нам этой кнопке не очень интересный код.\n\n" +
                   "Для демонстрации доступны следующие функции:\n" +
                   "-Заполнение базы данных новыми записями;\n" +
                   "-Тест отчёта о баге (желательно при запущенном приложении 'Bugs');\n" +
                   "-Изменение месяца в пределах 2019г;\n" +
				   "-Изменение дней выхода сотрудника на работу;\n" +
                   "-Изменение данных по выплате зарплаты: штраф, премия и т.д");
		}
		private void MainView_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				LayoutSaver.Save(this);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void btResetSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				var question = "Настройки экрана будут сброшены.\nПродолжить?";
				if (MessageService.Question(question) == DialogResult.Yes)
				{
					LayoutSaver.Reset();
					MessageService.Message("Настройки экрана сброшены.\nИзменения вступят после перезапуска приложения.");
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void MenuUndo_PaintMenuBar(object sender, BarCustomDrawEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		private void btOpenDbStartup_Click(object sender, EventArgs e)
		{
			try
			{
				var psi = new ProcessStartInfo
				{
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Normal,
					FileName = "explorer",
					Arguments = @"/n, /select, Database startup.bak"
				};
				using (var prFolder = new Process { StartInfo = psi })
				{
					prFolder.Start();
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void btOpenConfigFile_Click(object sender, EventArgs e)
		{
			try
			{
				var psi = new ProcessStartInfo
				{
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Normal,
					FileName = "explorer",
					Arguments = @"/n, /select, WorkDaysJournal.exe.config"
				};
				using (var prFolder = new Process { StartInfo = psi })
				{
					prFolder.Start();
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		public void ShowSqlError()
		{
			try
			{
				var connectionString = Settings.Default.ConnectionString;
				lbNoConnectSqlConnectionStringValue.Text = $"- текущее значение 'ConnectionString': {connectionString}";
				btRefillDataBase.Enabled = false;
				pnNoConnectSql.Visible = true;
				pnNoConnectSql.BringToFront();
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		public void ReportBug(object sender, BugEventArgs e)
		{
			try
			{
				using (var form = new FrmReportBug(e))
				{
					form.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}
		#endregion
	}
}