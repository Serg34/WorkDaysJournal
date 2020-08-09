using System;
using System.Drawing;
using System.Windows.Forms;

namespace Furmanov.Views
{
	public sealed class PictureEditor : UserControl
	{
		private float _scale = 1;
		private float _moveX = 0;
		private float _moveY = 0;
		private PointF _center;

		private bool _movingView = false;
		private Point _dragStartPoint;
		public PictureEditor()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint
				  | ControlStyles.OptimizedDoubleBuffer
				  | ControlStyles.ResizeRedraw
				  | ControlStyles.SupportsTransparentBackColor
				  | ControlStyles.UserPaint
				  , true);
			AllowDrop = true;

			DragDrop += (s, e) => ResetDragDrop();
			MouseUp += (s, e) => ResetDragDrop();
			DragLeave += (s, e) => ResetDragDrop();

			Resize += (s, e) => Rebuild();

			Reload(true);
		}

		public Image Image { get; set; }
		public bool CanEdit { get; private set; } = true;

		private void Reload(bool isCentering = false)
		{
			if (isCentering)
			{
				_scale = 1;
				_moveX = 0;
				_moveY = 0;

				_center = new PointF(Width / 2, Height / 2);
			}

			if (Image == null) return;

			Rebuild();

			Invalidate();
		}
		private void Rebuild(float scaleCoef = 1, bool isMoving = true)
		{
			if (Image == null) return;

			if (isMoving)
			{
				_moveY = -((_center.X - _moveY) * scaleCoef - _center.X);
				_moveX = -((_center.Y - _moveX) * scaleCoef - _center.Y);
			}

			LimitMove();
			Invalidate();
		}
		private void LimitMove()
		{
			_moveX = Math.Max(_moveX, -0.75F * Width * _scale);
			_moveX = Math.Min(_moveX, 0.75F * Width);
			_moveY = Math.Max(_moveY, -0.75F * Height * _scale);
			_moveY = Math.Min(_moveY, 0.75F * Height);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			if (!CanEdit) return;

			if (e.Button == MouseButtons.Middle)
			{
				Reload(isCentering: true);
			}
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			if (!CanEdit) return;

			var scaleCoef = _scale * e.Delta > 0 ? 1.1F : 1 / 1.1F;
			_scale *= scaleCoef;
			if (_scale > 5)
			{
				_scale = 5;
				return;
			}
			if (_scale < 0.5F)
			{
				_scale = 0.5F;
				return;
			}

			_center = e.Location;
			Rebuild(scaleCoef);
		}

		protected override bool ProcessDialogKey(Keys keyData) => false;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape) ResetDragDrop();

			var dist = 10;
			if (e.KeyCode == Keys.Left)
			{
				_moveX += dist;
				Rebuild();
			}
			else if (e.KeyCode == Keys.Right)
			{
				_moveX -= dist;
				Rebuild();
			}
			else if (e.KeyCode == Keys.Up)
			{
				_moveY += dist;
				Rebuild();
			}
			else if (e.KeyCode == Keys.Down)
			{
				_moveY -= dist;
				Rebuild();
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (!CanEdit) return;

			if (Math.Abs(e.X - _dragStartPoint.X) < 2) return;

			_dragStartPoint = PointToClient(Cursor.Position);
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
			{
				_movingView = true;
				DoDragDrop(this, DragDropEffects.All);
			}
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);

			if (_movingView)
			{
				e.Effect = DragDropEffects.Move;
				var loc = PointToClient(new Point(e.X, e.Y));

				_moveX += loc.X - _dragStartPoint.X;
				_moveY += loc.Y - _dragStartPoint.Y;

				_dragStartPoint = loc;
				Rebuild(isMoving: false);
			}
		}
		private void ResetDragDrop()
		{
			if (!CanEdit) return;
			_movingView = false;
			_dragStartPoint = Point.Empty;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (!IsHandleCreated || DesignMode || !Visible) return;
			base.OnPaint(e);

			if ((float)Image.Width / Image.Height > Width / (float)Height) //Если длинное изображение
			{
				var heigth = Image.Height / ((float)Image.Width / Width);
				var y = _moveY + (Height - heigth) / 2;
				e.Graphics.DrawImage(Image, _moveX, y, Width * _scale, heigth * _scale);
			}
			else //если высокое
			{
				var width = Image.Width / ((float)Image.Height / Height);
				var x = _moveX + (Width - width) / 2;
				e.Graphics.DrawImage(Image, x, _moveY, width * _scale, Height * _scale);
			}
		}
	}
}
