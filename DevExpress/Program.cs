using System;
using System.Windows.Forms;
using Furmanov.MVP.MainView;
using Furmanov.MVP.Services.UndoRedo;
using Furmanov.UI;
using Furmanov.UI.IoC;

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

			var connectionString = "Server =.\\SQLExpress; Database = Furmanov; Trusted_Connection = True;";
			var resolver = IoCContainerBuilder.Build(connectionString);

			var model = resolver.Resolve<IMainModel>();
			var view = new MainView();
			var undoRedoService = resolver.Resolve<IUndoRedoService>();
			new MainPresenter(model, view, undoRedoService);

			Application.Run(view);
		}
	}
}
