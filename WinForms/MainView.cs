using BrightIdeasSoftware;
using Furmanov.Data.Data;
using Furmanov.MVP;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Furmanov.Services.UndoRedo;
using Furmanov.UI.Properties;
using Furmanov.UI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Furmanov.UI
{
	public partial class MainView : Form, IMainView
	{
		#region Fields
		private SalaryPay[] Pays { get; set; }
		private List<WorkedDay> Days { get; set; }
		private SalaryPay _currentPay;
		private SalaryPay _prevPay;
		private bool _updating;
		#endregion

		#region Init
		public MainView()
		{
			InitializeComponent();

			InitTreeSalary();

			InitUndo();
		}

		private void InitTreeSalary()
		{
			colName.ImageGetter = row =>
			{
				if (row is SalaryPay pay) return (int)pay.Type;
				return -1;
			};
			treeSalary.CanExpandGetter = row =>
			{
				if (!(row is SalaryPay pay)) return false;
				return Pays?.Any(p => p.ParentId == pay.ViewModelId) ?? false;
			};
			treeSalary.ChildrenGetter = row =>
			{
				if (!(row is SalaryPay pay)) return new SalaryPay[0];
				return Pays?.Where(p => p.ParentId == pay.ViewModelId);
			};
		}
		private void InitUndo()
		{
			btUndo.DropDownOpening += (s, e) => btUndo.Tag = false;
			btUndo.DropDownClosed += (s, e) => btUndo.Tag = true;

			btRedo.DropDownOpening += (s, e) => btRedo.Tag = false;
			btRedo.DropDownClosed += (s, e) => btRedo.Tag = true;

			btUndo.Click += (s, e) =>
			{
				if (btUndo.Tag is bool flag && flag)
				{
					Undo?.Invoke(this, 1);
				}
			};
			btRedo.Click += (s, e) =>
			{
				if (btRedo.Tag is bool flag && flag)
				{
					Redo?.Invoke(this, 1);
				}
			};
		}
		#endregion

		#region Events
		public event EventHandler Logging;
		public event EventHandler Logout;
		public event EventHandler RefillingDataBase;
		public event EventHandler<MonthEventArgs> ChangedMonth;
		public event EventHandler<SalaryPay> WorkDaysOnlyClick;
		public event EventHandler<SalaryPay> AllDaysClick;
		public event EventHandler<SalaryPay> DeletingAllDays;
		public event EventHandler<UndoRedoEventArgs<SalaryPay>> ChangedSalaryPay;
		public event EventHandler<SalaryPay> SelectionChangingSalaryPay;
		public event EventHandler<WorkedDay> ChangedWorkedDay;
		public event EventHandler<int> Undo;
		public event EventHandler<int> Redo;
		public event EventHandler<BugEventArgs> ReportingBug;
		#endregion

		#region Login
		public void UpdateLogin(UserDto user)
		{
			try
			{
				btReportTotal.Enabled =
				btAddUser.Enabled =
				btEditUser.Enabled =
				btDeleteUser.Enabled =
				cbMonth.Enabled =
				nudYear.Enabled =
				btWorkDaysOnly.Enabled =
				btAllDays.Enabled =
				btDeleteAllDays.Enabled = user != null;

				btLogin.Visible = user == null;
				btLogOut.Visible = user != null;

				UpdateUndoRedo(new string[0], new string[0]);

				if (user != null)
				{
					lblUser.Text = $"Пользователь: {user.Login} | {user.Name} | {user.Role_Id.DisplayName()}";
				}
				else
				{
					lblUser.Text = "Вход не выполнен";
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		public ILoginView LoginView => new LoginView { Owner = this };
		private void btLogin_Click(object sender, EventArgs e)
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
		private void btLogOut_Click(object sender, EventArgs e)
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

		#region Top Menu
		private void btRefillDataBase_Click(object sender, EventArgs e)
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
		private void btReportBugTest_Click(object sender, EventArgs e)
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

		private void MonthChanged(object sender, EventArgs e)
		{
			try
			{
				if (_updating) return;
				var month = cbMonth.SelectedIndex + 1;
				var year = (int)nudYear.Value;
				ChangedMonth?.Invoke(this, new MonthEventArgs(year, month));
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		private void btWorkDaysOnly_Click(object sender, EventArgs e)
		{
			try
			{
				WorkDaysOnlyClick?.Invoke(this, _currentPay);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void btAllDays_Click(object sender, EventArgs e)
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
		private void btDeleteAllDays_Click(object sender, EventArgs e)
		{
			try
			{
				DeletingAllDays?.Invoke(this, _currentPay);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		#endregion

		#region Update
		public void UpdateMonth(object sender, MainViewModel viewModel)
		{
			try
			{
				_updating = true;
				cbMonth.SelectedIndex = viewModel.Month - 1;
				nudYear.Value = viewModel.Year;
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
				Pays = viewModel.SalaryPays;
				if (!Pays.Any())
				{
					treeSalary.Roots = Pays;
					return;
				}
				var projects = Pays.Where(p => p.Type == ObjType.Project).ToList();
				if (projects.Any())
				{
					projects.Add(Pays.First(p => p.Type == ObjType.Summary));
					using (new TreeListStateSaver(treeSalary))
					{
						treeSalary.Roots = projects;
					}
				}
				else
				{
					var objs = Pays.Where(p => p.Type == ObjType.Object).ToList();
					objs.Add(Pays.First(p => p.Type == ObjType.Summary));
					using (new TreeListStateSaver(treeSalary))
					{
						treeSalary.Roots = objs;
					}
				}
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
		public void UpdateDays(object sender, List<WorkedDay> viewModel)
		{
			try
			{
				Days = viewModel;
				if (gvWorkedDays.RowCount != Days.Count) gvWorkedDays.RowCount = Days.Count;
				gvWorkedDays.Invalidate();
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
				btRedo.Enabled = redoItems.Any();
				RebuildUndoButton(btUndo, undoItems);
				RebuildUndoButton(btRedo, redoItems);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}

		#endregion


		#region TreeSalary
		private void treeSalary_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!(treeSalary.SelectedObject is SalaryPay pay)) return;
				_currentPay = pay;

				btAddUser.Enabled =
				btEditUser.Enabled =
				btReportForObject.Enabled = pay.Type == ObjType.Object ||
													pay.Type == ObjType.SalaryPay;

				btDeleteUser.Enabled =
				btAllDays.Enabled =
				btWorkDaysOnly.Enabled =
				btDeleteAllDays.Enabled = pay.Type == ObjType.SalaryPay;

				SelectionChangingSalaryPay?.Invoke(this, _currentPay);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void treeSalary_FormatRow(object sender, FormatRowEventArgs e)
		{
			try
			{
				if (!(e.Model is SalaryPay pay)) return;
				if (pay.Type == ObjType.Project)
				{
					e.Item.BackColor = Color.FromArgb(255, 217, 143);
					e.Item.Font = new Font(e.Item.Font, FontStyle.Bold);
				}
				else if (pay.Type == ObjType.Object)
				{
					e.Item.BackColor = Color.FromArgb(230, 245, 255);
					e.Item.Font = new Font(e.Item.Font, FontStyle.Underline);
				}
				else if (pay.Type == ObjType.SalaryPay)
				{
				}
				else if (pay.Type == ObjType.Summary)
				{
					e.Item.Font = new Font(e.Item.Font, FontStyle.Bold);
					e.Item.BackColor = Color.FromArgb(218, 218, 218);
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		#endregion

		#region GvWorkedDays
		private void gvWorkedDays_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			try
			{
				DataGridViewService.CellValueNeeded(sender, e, Days);
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void gvWorkedDays_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (!(sender is DataGridView gv)) return;
				var fieldName = gv.Columns[e.ColumnIndex].DataPropertyName;
				if (fieldName == nameof(WorkedDay.IsWorked))
				{
					var day = Days[e.RowIndex];
					day.IsWorked = !day.IsWorked;
					ChangedWorkedDay?.Invoke(this, day);
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void gvWorkedDays_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
			{
				if (e.RowIndex > Days.Count - 1) return;
				var date = Days[e.RowIndex].Date;
				if (date.DayOfWeek == DayOfWeek.Saturday ||
					date.DayOfWeek == DayOfWeek.Sunday)
				{
					e.CellStyle.BackColor = Color.FromArgb(255, 103, 103);
				}
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		#endregion


		#region Form service
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
		public void ShowSqlError()
		{
			try
			{
				var connectionString = Settings.Default.ConnectionString;
				//lbNoConnectSqlConnectionStringValue.Text = $"- текущее значение 'ConnectionString': {connectionString}";
				//btRefillDataBase.Enabled = false;
				//pnNoConnectSql.Visible = true;
				//pnNoConnectSql.BringToFront();
			}
			catch (Exception ex)
			{
				ReportingBug?.Invoke(this, new BugEventArgs(Application.ProductName, ex));
			}
		}
		private void ShowNoImplementedCode(object sender, EventArgs e)
		{
			MessageService.Message("В реальном приложении нам этой кнопке не очень интересный код.\n\n" +
								   "Для демонстрации доступны следующие функции:\n" +
								   "-Заполнение базы данных новыми записями;\n" +
								   "-Тест отчёта о баге (желательно при запущенном приложении 'Bugs');\n" +
								   "-Изменение месяца в пределах 2019г;\n" +
								   "-Изменение дней выхода сотрудника на работу;\n" +
								   "-Изменение данных по выплате зарплаты: штраф, премия и т.д");
		}
		private void RebuildUndoButton(ToolStripSplitButton button, IEnumerable<string> items)
		{
			button.DropDownItems.Clear();

			foreach (var item in items)
			{
				var mi = new ToolStripMenuItem { Text = item, BackColor = SystemColors.Control };
				mi.MouseEnter += (s, e) => SelectUndoItems(button, button.DropDownItems.IndexOf(mi));
				button.DropDownItems.Add(mi);
			}
		}
		private void SelectUndoItems(ToolStripSplitButton button, int index)
		{
			foreach (ToolStripMenuItem item in button.DropDownItems)
			{
				if (button.DropDownItems.IndexOf(item) > index)
					item.BackColor = SystemColors.Control;
				else item.BackColor = Color.FromArgb(213, 234, 255);
			}
		}
		#endregion


	}
}
