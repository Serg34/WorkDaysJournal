using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Furmanov.UI.Services
{
	public static class DataGridViewService
	{
		public static void CellValueNeeded<T>(object sender, DataGridViewCellValueEventArgs e, IEnumerable<T> src)
		{
			var dataSource = src as T[] ?? src.ToArray();
			if(e.RowIndex > dataSource.Length - 1) return;

			var prop = GetProperty<T>(sender, e);
			if (prop == null) return;

			var row = dataSource.ElementAt(e.RowIndex);
			var value = prop.GetValue(row);

			if (value is DateTime dt) e.Value = dt.ToString("d");
			else e.Value = value;
		}

		public static void CellValuePushed<T>(object sender, DataGridViewCellValueEventArgs e, IEnumerable<T> src)
		{
			var dataSource = src as T[] ?? src.ToArray();
			if (e.RowIndex > dataSource.Length - 1) return;

			var prop = GetProperty<T>(sender, e);
			if (prop == null) return;

			var row = dataSource[e.RowIndex];
			prop.SetValue(row, e.Value);
		}

		private static PropertyInfo GetProperty<T>(object sender, DataGridViewCellValueEventArgs e)
		{
			if (!(sender is DataGridView dgv)) return null;
			var fieldName = dgv.Columns[e.ColumnIndex].DataPropertyName;
			var prop = typeof(T).GetProperty(fieldName);
			return prop;
		}
	}
}
