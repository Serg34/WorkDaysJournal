using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;

namespace SC.Common.Services
{
	public class GridViewStateSaver : IDisposable
	{
		private readonly GridView _gridView;
		private readonly int _focusedID;
		private readonly GridColumn _col;
		private readonly int _top;

		public GridViewStateSaver(GridView gridView)
		{
			_gridView = gridView;
			if (gridView.GetFocusedRow() is IHasID vm)
			{
				_focusedID = vm.ID;
			}
			_col = _gridView.FocusedColumn;
			_top = _gridView.TopRowIndex;
		}

		public void Dispose()
		{
			if (_focusedID > 0)
			{
				for (int r = 0; r < _gridView.RowCount; r++)
				{
					if (_gridView.GetRow(r) is IHasID vm && vm.ID == _focusedID)
					{
						_gridView.FocusedRowHandle = r;
						break;
					}
				}
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
