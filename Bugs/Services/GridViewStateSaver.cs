using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Furmanov.Data.Data;
using System;

namespace Furmanov.Services
{
	public class GridViewStateSaver : IDisposable
	{
		private readonly GridView _gridView;
		private readonly int _focusedId;
		private readonly GridColumn _col;
		private readonly int _top;

		public GridViewStateSaver(GridView gridView)
		{
			_gridView = gridView;
			if (gridView.GetFocusedRow() is Dto vm)
			{
				_focusedId = vm.Id;
			}
			_col = _gridView.FocusedColumn;
			_top = _gridView.TopRowIndex;
		}

		public void Dispose()
		{
			if (_focusedId > 0)
			{
				for (int r = 0; r < _gridView.RowCount; r++)
				{
					if (_gridView.GetRow(r) is Dto vm && vm.Id == _focusedId)
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
