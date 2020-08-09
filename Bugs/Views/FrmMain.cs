using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Furmanov.Data;
using Furmanov.Data.Data;
using Furmanov.Properties;
using Furmanov.Services;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;

namespace Furmanov.Views
{
	public partial class FrmMain : DevExpress.XtraEditors.XtraForm
	{
		#region Fields
		private readonly string _connectionString = Settings.Default.ConnectionString;
		private DateTime? _lastBugDateTime;
		private DateTime? _lastIncidentDateTime;
		private readonly List<int> _incidentBugIDs = new List<int>();
		private bool _isActive = true; 
		#endregion
		public FrmMain()
		{
			try
			{
				InitializeComponent();
				LayoutSaver.Restore(this);

				UpdateData();

				new Task(CheckBugs).Start();
				new Task(CheckIncidents).Start();
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void UpdateData()
		{
			try
			{
				using (var db = new DbContext(_connectionString))
				{
					var bugs = db.Query<Bug>(Resources.Bugs)
						.ToArray();
					gcBugs.DataSource = bugs;
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void CheckBugs()
		{
			using (var db = new DbContext(_connectionString))
			{
				while (!Disposing && !IsDisposed)
				{
					System.Threading.Thread.Sleep(60_000);
					if (!_isActive) continue;
					var lastBug = db.GetTable<BugDto>()
						.OrderByDescending(b => b.CreatedDate)
						.FirstOrDefault();
					if (_lastBugDateTime == null) _lastBugDateTime = lastBug?.CreatedDate;
					if (lastBug?.CreatedDate > _lastBugDateTime)
					{
						_lastBugDateTime = lastBug.CreatedDate;
						_isActive = false;
						Invoke(new Action(() =>
						{
							TaskbarProgress.Error(this);
							UpdateData();
							MessageService.Message($"Новый баг:\n{lastBug.Message}");
							TaskbarProgress.Finish(this);
							_isActive = true;
						}));
					}
				}
			}
		}
		private void CheckIncidents()
		{
			using (var db = new DbContext(_connectionString))
			{
				while (!Disposing && !IsDisposed)
				{
					System.Threading.Thread.Sleep(60_000);
					//if (!_isActive) continue;
					var last = db.GetTable<BugIncidentDto>()
						.OrderByDescending(i => i.DateTime)
						.FirstOrDefault();
					if (_lastIncidentDateTime == null) _lastIncidentDateTime = last?.DateTime;
					if (last?.DateTime > _lastIncidentDateTime)
					{
						_lastIncidentDateTime = last.DateTime;
						_incidentBugIDs.Add(last.Bug_Id);
						Invoke(new Action(() =>
						{
							TaskbarProgress.SetState(TaskbarProgress.TaskbarStates.Indeterminate, this);
							UpdateData();
						}));
					}
				}
			}
		}

		private void gvBugs_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (!(sender is GridView gv)) return;
				var pt = gv.GridControl.PointToClient(MousePosition);
				var hitInfo = gv.CalcHitInfo(pt);
				if (!hitInfo.InRow) return;

				if (!(gvBugs.GetFocusedRow() is Bug bug)) return;
				if (bug.PrintScreen == null)
				{
					MessageService.Message("Изображение отсутствует");
					return;
				}
				using (var form = new FrmPrintScreen(bug))
				{
					form.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}
		private void gvBugs_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			try
			{
				if (!(gvBugs.GetFocusedRow() is Bug bug)) return;
				using (var db = new DbContext(_connectionString))
				{
					var incidents = db.GetTable<BugIncidentDto>()
						.Where(i => i.Bug_Id == bug.Id)
						.ToArray();

					gcIncidents.DataSource = incidents;
					gvBugs.LayoutChanged();
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}
		private void gvBugs_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			try
			{
				if (!(gvBugs.GetFocusedRow() is Bug bug)) return;
				using (var db = new DbContext(_connectionString))
				{
					var bugDto = MapperService.Map<BugDto>(bug);
					db.Update(bugDto);
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}
		private void gvBugs_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
		{
			if (gvBugs.FocusedColumn == colTotalMessage)
			{
				e.ErrorText = "Текст ошибки доступен только для копирования. Его менять нельзя";
				e.Valid = false;
			}
		}
		private void gvBugs_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			try
			{
				if (!(gvBugs.GetRow(e.RowHandle) is Bug bug)) return;
				if (_incidentBugIDs.Contains(bug.Id))
				{
					_incidentBugIDs.Remove(bug.Id);
					gvBugs.LayoutChanged();
					if (!_incidentBugIDs.Any())
					{
						TaskbarProgress.Start(this);
					}
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void gvBugs_CalcRowHeight(object sender, RowHeightEventArgs e)
		{
			var max = e.RowHandle == gvBugs.FocusedRowHandle ? 300 : 150;
			e.RowHeight = Math.Min(e.RowHeight, max);
		}
		private void gvBugs_RowStyle(object sender, RowStyleEventArgs e)
		{
			try
			{
				if (!(gvBugs.GetRow(e.RowHandle) is Bug bug)) return;
				if (bug.SolvedDate != null || bug.InfoToUser.NoEmpty())
				{
					e.Appearance.BackColor = Color.FromArgb(30, 100, 200, 100);
				}
				else
				{
					//e.Appearance.BackColor = Color.FromArgb(30, 200, 0, 50);
				}
				if (_incidentBugIDs.Contains(bug.Id))
				{
					e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void FrmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5) UpdateData();
		}
		private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			LayoutSaver.Save(this);
		}
	}
}
