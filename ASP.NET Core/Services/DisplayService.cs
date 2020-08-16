using Furmanov.Data.Data;
using System.Text;

namespace Furmanov.Services
{
	public class DisplayService
	{
		public static string ExpandIcon(string id)
		{
			var res ="<td class=\"show-hide-icon no-cell\">" +
				$"<a id = \"{id}_icon\" onclick=\"ShowHideRow('{id}')\">" +
				"<i class=\"fa fa-caret-right\" aria-hidden=\"true\"></i></a></td>";

			return res;
		}

		public static string Row(SalaryPay pay)
		{
			var res = new StringBuilder();
			var cls = pay.Type == ObjType.Project ? "class=\"project-cell\""
				: pay.Type == ObjType.Object ? "class=\"object-cell\""
				: pay.Type == ObjType.Summary ? "class=\"summary-cell\""
				: "class=\"pay-cell\"";

			if (pay.Type == ObjType.Project || pay.Type == ObjType.Summary)
			{
				res.Append($"<td {cls} colspan=\"3\">{pay.Name}</td>");
			}
			if (pay.Type == ObjType.Object) res.Append($"<td {cls} colspan=\"2\">{pay.Name}</td>");
			if (pay.Type == ObjType.Salary) res.Append($"<td {cls}>{pay.Name}</td>");

			void Add(object value) => res.Append($"<td {cls}>{value}</td>");
			void AddDecimal(decimal? value) => res.Append($"<td {cls}>{value:c2}</td>");
			
			Add(pay.Position);
			Add(pay.Phone);
			AddDecimal(pay.Salary);
			Add(pay.RateDays);
			Add(pay.FactDays);
			AddDecimal(pay.Advance);
			AddDecimal(pay.Penalty);
			AddDecimal(pay.Premium);
			AddDecimal(pay.SalaryToPay);
			Add(pay.Comment);

			return res.ToString();
		}
	}
}
