using Furmanov.Data.Data;
using Furmanov.Services;

namespace Furmanov.Views
{
	public partial class FrmPrintScreen : DevExpress.XtraEditors.XtraForm
	{
		public FrmPrintScreen(BugDto bug)
		{
			InitializeComponent();

			picPrintScreen.Image = bug.PrintScreen.ToImage();
		}
	}
}