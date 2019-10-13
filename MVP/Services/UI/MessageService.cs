using System.Windows.Forms;

namespace Furmanov.MVP.Services.UI
{
	public static class MessageService
	{
		private static readonly bool isRightToLeft = Application.CurrentCulture.TextInfo.IsRightToLeft;
		private const string PRODUCT_NAME = "Furmanov";
		public static void ShowMessage(string message)
		{
			MessageBox.Show(message,
				PRODUCT_NAME,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}

		public static void ShowExclamation(string message)
		{
			MessageBox.Show(message,
				PRODUCT_NAME,
				MessageBoxButtons.OK,
				MessageBoxIcon.Exclamation,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}

		public static void ShowError(string message)
		{
			MessageBox.Show(message,
				PRODUCT_NAME,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}

		public static DialogResult ShowQuestion(string message)
		{
			return MessageBox.Show(message,
				PRODUCT_NAME,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}
		public static DialogResult ShowQuestionWithCancel(string message)
		{
			return MessageBox.Show(message,
				PRODUCT_NAME,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1,
				isRightToLeft ? MessageBoxOptions.RtlReading : 0);
		}
	}
}
