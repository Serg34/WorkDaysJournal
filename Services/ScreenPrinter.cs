using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace UltraZoom.Services
{
	public static class ScreenPrinter
	{
		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		private static readonly int _width;
		private static readonly int _height;

		private const int DESKTOPHORZRES = 118;
		private const int DESKTOPVERTRES = 117;

		static ScreenPrinter()
		{
			IntPtr hdc = GetDC(IntPtr.Zero);
			_width = GetDeviceCaps(hdc, DESKTOPHORZRES);
			_height = GetDeviceCaps(hdc, DESKTOPVERTRES);
		}

		public static Image Print()
		{
			using (var bmp = new Bitmap(_width, _height))
			using (var gr = Graphics.FromImage(bmp))
			{
				gr.CopyFromScreen(0, 0, 0, 0, new Size(_width, _height));
				return Image.FromHbitmap(bmp.GetHbitmap());
			}
		}
	}
}
