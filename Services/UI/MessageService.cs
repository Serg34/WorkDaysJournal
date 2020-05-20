using System.Windows.Forms;

namespace Furmanov.Services.UI
{
	public static class MessageService
	{
		private static readonly bool isRightToLeft = Application.CurrentCulture.TextInfo.IsRightToLeft;
		private const string PRODUCT_NAME = "Табель учёта рабочего времени";
		public static void Message(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}

		public static void Exclamation(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Exclamation,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}

		public static void Error(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}

		public static DialogResult Question(string message)
		{
			return MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}
	}
}
