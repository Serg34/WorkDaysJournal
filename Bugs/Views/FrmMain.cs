using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Furmanov.Data.Data;
using Furmanov.Models;
using Furmanov.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Furmanov.Views
{
	public partial class FrmMain : DevExpress.XtraEditors.XtraForm
	{
		private readonly MainModel _model = new MainModel();
		public FrmMain()
		{
			try
			{
				InitializeComponent();
				LayoutSaver.Restore(this);

				_model.Updating += (sender, bugs) =>
				{
					Invoke(new Action(() => UpdateData(bugs)));
				};
				Shown += (sender, e) =>
				{
					_model.Update();
					_model.Start();
				};
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void UpdateData(Bug[] bugs)
		{
			try
			{
				using (new GridViewStateSaver(gvBugs))
				{
					gcBugs.DataSource = bugs;
				}
				UpdateTaskBar();
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void UpdateTaskBar()
		{
			try
			{
				if (!(gcBugs.DataSource is Bug[] bugs)) return;
				if (bugs.Any(b => b.IsNew))
				{
					TaskbarProgress.Error(this);
				}
				else if (bugs.Any(b => b.HasNewIncident))
				{
					TaskbarProgress.Pause(this);
				}
				else
				{
					TaskbarProgress.Finish(this);
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
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

				var incidents = _model.GetIncidents(bug.Id);
				gcIncidents.DataSource = incidents;
				gvBugs.LayoutChanged();
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

				_model.UpdateBug(bug);
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
				if (bug.IsNew || bug.HasNewIncident)
				{
					bug.IsNew = false;
					_model.DeleteNewBug(bug);
					bug.HasNewIncident = false;
					_model.DeleteNewIncident(bug.Id);
					gvBugs.LayoutChanged();
				}
				UpdateTaskBar();
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
				if (bug.HasNewIncident)
				{
					e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void FrmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5) _model.Update();
		}
		private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			_model.Dispose();
			LayoutSaver.Save(this);
		}
	}
}
