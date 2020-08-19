using System.Runtime.Serialization;
using Furmanov.Data.Data;

namespace Furmanov.Models
{
	[DataContract]
	public class SaveWorkedDaysViewModel
	{
		[DataMember] public int PayId { get; set; }
		[DataMember] public bool AllDays { get; set; }
		[DataMember] public bool IsExist { get; set; }
		[DataMember] public string[] ExpandList { get; set; }
		[DataMember] public string SelectedRow { get; set; }
	}
}
