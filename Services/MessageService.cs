using System.Windows.Forms;

namespace Furmanov.Services
{
	public static class MessageService
	{
		public static void Message(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1);
		}

		public static void Exclamation(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Exclamation,
				MessageBoxDefaultButton.Button1);
		}

		public static void Error(string message)
		{
			MessageBox.Show(message,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1);
		}

		public static DialogResult Question(string message, MessageBoxButtons buttons = MessageBoxButtons.YesNo)
		{
			return MessageBox.Show(message,
				Application.ProductName,
				buttons,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1);
		}
	}
}
