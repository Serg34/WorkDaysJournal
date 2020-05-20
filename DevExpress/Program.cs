using Furmanov.UI.IoC;
using System;
using System.Threading.Tasks;
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

			new Task(KillAboutDevExpressForm).Start();

			var connectionString = "Server =.\\SQLExpress; Database = Furmanov; Trusted_Connection = True;";
			var resolver = IoCBuilder.Build(connectionString);

			var model = resolver.Resolve<IMainModel>();
			var view = new MainView();
			var undoRedoService = resolver.Resolve<IUndoRedoService>();
			new MainPresenter(model, view, undoRedoService);

			Application.Run(view);
		}
		private static void KillAboutDevExpressForm()
		{
			var start = DateTime.Now;
			while ((DateTime.Now - start).TotalSeconds < 10)
			{
				System.Threading.Thread.Sleep(500);
				var activeWindows = Application.OpenForms;
				foreach (Form form in activeWindows)
				{
					if (form.Name == "AboutForm12")
					{
						form.Invoke(new Action(() => form.Close()));
						return;
					}
				}
			}
		}
	}
}
