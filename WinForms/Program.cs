using Furmanov.MVP.MainView;
using Furmanov.Services.UndoRedo;
using Furmanov.UI.IoC;
using System;
using System.Windows.Forms;

namespace Furmanov.UI
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

			var connectionString = Properties.Settings.Default.ConnectionString;
			var resolver = IoCBuilder.Build(connectionString);

			var model = resolver.Resolve<IMainModel>();
			var view = new MainView();
			var undoRedoService = resolver.Resolve<IUndoRedoService>();
			new MainPresenter(model, view, undoRedoService);

			Application.Run(view);
		}
	}
}
