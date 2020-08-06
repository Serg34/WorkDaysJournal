using System;
using System.Windows.Forms;

namespace Furmanov.MVP
{
	public interface IView : IWin32Window
	{
		void Show();
		DialogResult ShowDialog();
		void Hide();
		void Close();

		void ShowError(Exception ex);
	}
}
