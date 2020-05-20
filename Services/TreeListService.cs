using DevExpress.XtraTreeList;
using System.Drawing;

namespace SC.Common.Services
{
	public class TreeListService
	{
		public static void CustomDrawNodeCell(CustomDrawNodeCellEventArgs e)
		{
			if (e.Info.Appearance.Options.UseBorderColor)
			{
				var b = e.Bounds;
				b.Inflate(1, 1);
				e.Cache.DrawRectangle(e.Cache.GetPen(Color.FromArgb(25, e.Info.Appearance.BorderColor)), b);
				e.Cache.DrawLine(e.Cache.GetPen(e.Info.Appearance.BorderColor),
					new Point(b.Left, b.Top), new Point(b.Right, b.Top));
				e.Cache.DrawLine(e.Cache.GetPen(e.Info.Appearance.BorderColor),
					new Point(b.Left, b.Bottom - 1), new Point(b.Right, b.Bottom - 1));
			}
		}
	}
}
