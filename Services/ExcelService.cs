using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Xls = Microsoft.Office.Interop.Excel;

namespace SC.Common.Services
{
	public class ExcelService : IDisposable
	{
		private readonly IWin32Window _form;
		private Xls.Workbook _wbk;
		private string _fileName;

		public ExcelService(IWin32Window form)
		{
			_form = form;
		}
		public ExcelService GridToExcel(Action<string> action)
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = "Экспортировать таблицу в файл";
				dialog.DefaultExt = "*.xlsx";
				dialog.AddExtension = true;
				dialog.OverwritePrompt = true;
				dialog.CheckPathExists = true;
				dialog.Filter = "Файлы Excel|*.xlsx";

				if (dialog.ShowDialog(_form) == DialogResult.OK)
				{
					_fileName = dialog.FileName;
					action?.Invoke(_fileName);
				}
			}
			return this;
		}

		public ExcelService SetAutoFit()
		{
			if (_fileName.IsEmpty()) return this;
			if (_wbk == null)
			{
				var app = new Xls.Application();
				_wbk = app.Workbooks.Open(_fileName);
			}
			foreach (Xls.Worksheet sheet in _wbk.Sheets)
			{
				sheet.Columns.AutoFit();
			}
			return this;
		}
		public ExcelService SetMonthOnTopRow()
		{
			if (_fileName.IsEmpty()) return this;
			if (_wbk == null)
			{
				var app = new Xls.Application();
				_wbk = app.Workbooks.Open(_fileName);
			}
			_wbk.Sheets[1].Rows[1].NumberFormat = "ММММ гггг";

			return this;
		}
		public void Dispose()
		{
			if (_fileName.IsEmpty()) return;
			if (_wbk != null)
			{
				_wbk.Save();
				_wbk.Close(false);
			}
			var question = $"Открыть файл '{Path.GetFileNameWithoutExtension(_fileName)}'?";
			if (MessageService.ShowQuestion(question) == DialogResult.Yes)
			{
				Process.Start(_fileName);
			}
		}
	}
}
