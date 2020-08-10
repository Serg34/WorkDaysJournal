using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.UserSkins;
using Furmanov.Views;

namespace Furmanov
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			new Task(KillAboutDevExpressForm).Start();
			
			BonusSkins.Register();
			Application.Run(new FrmMain());
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
