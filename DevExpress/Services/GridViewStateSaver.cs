using System;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace Furmanov.UI.Services
{
	public class GridViewStateSaver : IDisposable
	{
		private readonly GridView _gridView;
		private readonly int _row;
		private readonly GridColumn _col;
		private int _top;

		public GridViewStateSaver(GridView gridView)
		{
			_gridView = gridView;
			_row = _gridView.FocusedRowHandle;
			_col = _gridView.FocusedColumn;
			_top = _gridView.TopRowIndex;
		}

		public void Dispose()
		{
			if (_row < _gridView.RowCount - 1)
			{
				_gridView.FocusedRowHandle = _row;
			}

			if (_gridView.Columns.Contains(_col))
			{
				_gridView.FocusedColumn = _col;
			}

			if (_top < _gridView.RowCount - 1)
			{
				_gridView.TopRowIndex = _top;
			}
		}
	}
}
