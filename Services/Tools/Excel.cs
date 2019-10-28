using System;
using System.IO;
using DataTable = System.Data.DataTable;
using Xls = Microsoft.Office.Interop.Excel;

namespace Furmanov.Services.Tools
{
	public class Excel
	{
		public void CreateVedomost(DataTable dt)
		{
			var app = new Xls.Application();
			var exlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "Ведомость.xlt");
			var wbk = app.Workbooks.Add(exlFile);
			try
			{
				//var sht = (Xls.Worksheet)wbk.Sheets[1];
				//decimal sum = 0;

				//for (var i = 0; i < dt.Rows.Count; i++)
				//{
				//	var salary = G._Dec(dt.Rows[i]["FactSalary"]);

				//	((Xls.Range)sht.Cells[34 + i, 1]).Value2 = i + 1;
				//	((Xls.Range)sht.Cells[34 + i, 3]).Value2 = dt.Rows[i]["oName"];
				//	((Xls.Range)sht.Cells[34 + i, 4]).Value2 = dt.Rows[i]["rName"];
				//	((Xls.Range)sht.Cells[34 + i, 8]).Value2 = salary;
				//	((Xls.Range)sht.Cells[34 + i, 10]).Value2 = 0;
				//	((Xls.Range)sht.Cells[34 + i, 11]).Value2 = salary;

				//	sum += salary;
				//}

				//sht.Range["SumText"].Value2 = MoneyToString.RurToWord(sum);
				//sht.Range["SumNum"].Value2 = $"( {Math.Floor(sum):N0} руб. {sum % 1 * 100:##} коп. )";

				//app.Visible = true;
			}
			catch
			{
				wbk.Close(false);
				app.Quit();
				GC.Collect();
				throw;
			}
		}
	}
}
