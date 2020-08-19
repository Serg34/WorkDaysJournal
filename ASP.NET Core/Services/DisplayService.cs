using Furmanov.Data.Data;

namespace Furmanov.Services
{
	public class DisplayService
	{
		public static string ExpandIcon(SalaryPay pay)
		{
			if (pay == null) return null;
			if (!pay.HasChildren) return "<td class=\"no-cell\"></td>";
			var id = pay.ViewModelId.ToId();

			var res = $"<td class=\"show-hide-icon no-cell\" onclick=\"ShowHideRow('{id}')\">" +
						  $"<a id = \"{id}_icon\">" +
							$"<i class=\"fa fa-caret-{(pay.IsExpanded ? "down" : "right")}\" aria-hidden=\"true\"></i>" +
						  $"</a>" +
					  $"</td>";

			return res;
		}
	}
}
