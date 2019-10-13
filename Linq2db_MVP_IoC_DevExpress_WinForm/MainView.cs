using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace Furmanov
{
	public partial class MainView : DevExpress.XtraBars.Ribbon.RibbonForm
	{
		public MainView()
		{
			InitializeComponent();
		}


		private void MenuUndo_PaintMenuBar(object sender, BarCustomDrawEventArgs e)
		{
			if (sender is PopupMenu menu)
			{
				var focused = menu.ItemLinks.FirstOrDefault(i => i.ScreenBounds.Contains(MousePosition));
				if (focused == null) return;

				var index = menu.ItemLinks.IndexOf(focused);
				for (int i = 0; i < menu.ItemLinks.Count; i++)
				{
					if (i < index)
					{
						using (var brush = new SolidBrush(Color.FromArgb(50, 90, 173, 228)))
						{
							e.Graphics.FillRectangle(brush, menu.ItemLinks[i].Bounds);
						}
					}
				}
			}
		}
	}
}