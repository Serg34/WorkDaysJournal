using Furmanov.Data.Data;
using Furmanov.Services;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Furmanov.MVP;

namespace Furmanov.UI
{
	public partial class FrmReportBug : Form
	{
		public FrmReportBug(BugEventArgs e)
		{
			try
			{
				InitializeComponent();
				FormBorderStyle = FormBorderStyle.None;
				Padding = new Padding(2, HeaderHeight, 2, 2);

				SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
				LoadBug(e);
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		private void LoadBug(BugEventArgs e)
		{
			try
			{
				var bug = e.Bug;
				if (bug.IsExist)
				{
					lbTitle.Text = "Возникла известная проблема в работе программы";
					lbTitle.ForeColor = Color.FromArgb(100, 200, 100);
					lbDescr.Text = $"Тип: {e.Exception.GetType()}\n" +
		               $"{e.Exception.Message}\n\n" +
		               $"Сборка: {bug.Project}\n" +
		               $"Номер ошибки: {bug.Id}\n" +
		               "Решение проблемы уже в работе.\n" +
		               (bug.SolvedDate != null ? $"Планируемая дата решения: {bug.SolvedDate:d}\n\n" : "") +
		               bug.InfoToUser;
				}
				else
				{
					lbTitle.Text = "Возникла неизвестная проблема в работе программы";
					lbTitle.ForeColor = Color.FromArgb(255, 100, 100);
					lbDescr.Text = $"Тип: {e.Exception.GetType()}\n" +
		               $"{e.Exception.Message}\n\n" +
		               "Все необходимые сведения уже отправлены разработчикам\n" +
		               $"Сборка: {bug.Project}\n" +
		               $"Номер ошибки: {bug.Id}";
				}
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}

		#region Move
		private const int HeaderHeight = 30;
		private bool _allowMoving = false;

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		protected override void OnMouseDown(MouseEventArgs e)
		{
			var rclose = GetCloseButtonRectangle();
			if (RectangleToScreen(rclose).Contains(MousePosition)) Close();

			_allowMoving = true;
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			_allowMoving = false;
			base.OnMouseUp(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			_allowMoving = false;
			base.OnMouseLeave(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_allowMoving && e.Button == MouseButtons.Left)
				if (e.Y < HeaderHeight)
				{
					ReleaseCapture();
					SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
					return;
				}

			base.OnMouseMove(e);
			Invalidate();
		}
		#endregion
		#region Paint
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			//if (ShowIcon)
			//	e.Graphics.DrawIcon(Icon, new Rectangle(1, 1, headerHeight, headerHeight));

			using (var pen = new Pen(SystemColors.ActiveBorder))
				e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

			DrawCloseButton(e.Graphics);
		}
		private void DrawCloseButton(Graphics g)
		{
			var rclose = GetCloseButtonRectangle();
			if (RectangleToScreen(rclose).Contains(MousePosition))
				using (Brush brclose = new SolidBrush(Color.DimGray))
					g.FillRectangle(brclose, rclose);

			var centr = new Point((rclose.Left + rclose.Right) / 2, (rclose.Top + rclose.Bottom) / 2);
			using (var pclose = new Pen(SystemColors.ActiveBorder))
			{
				g.DrawLine(pclose, centr.X - 2, centr.Y - 3, centr.X + 4, centr.Y + 3);
				g.DrawLine(pclose, centr.X - 3, centr.Y - 3, centr.X + 3, centr.Y + 3);

				g.DrawLine(pclose, centr.X - 2, centr.Y + 3, centr.X + 4, centr.Y - 3);
				g.DrawLine(pclose, centr.X - 3, centr.Y + 3, centr.X + 3, centr.Y - 3);
			}
		}
		private Rectangle GetCloseButtonRectangle()
		{
			return new Rectangle(Width - 20, 5, 15, 14);
		}
		#endregion

		private void FrmReportBug_Shown(object sender, EventArgs e)
		{
			try
			{
				TaskbarProgress.Error(Owner ?? this);
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}
		private void FrmReportBug_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				TaskbarProgress.Finish(Owner ?? this);
			}
			catch (Exception ex)
			{
				MessageService.Error(ex.ToString());
			}
		}
	}
}