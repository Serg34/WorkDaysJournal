using Furmanov;
using System;
using System.Windows.Forms;

namespace Linq2db_MVP_IoC_DevExpress_WinForm
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainView());
		}
	}
}
