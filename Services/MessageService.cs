using System.Windows.Forms;

namespace SC.Common.Services
{
	public static class MessageService
	{
		public static void ShowMessage(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1);
		}

		public static void ShowExclamation(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Exclamation,
				MessageBoxDefaultButton.Button1);
		}

		public static void ShowError(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1);
		}

		public static DialogResult ShowQuestion(string message, MessageBoxButtons buttons = MessageBoxButtons.YesNo)
		{
			return MessageBox.Show(message,
				Application.ProductName,
				buttons,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1);
		}
	}
}
