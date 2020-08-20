using System.Runtime.Serialization;

namespace Furmanov.Models
{
	[DataContract]
	public class SaveWorkedDaysViewModel
	{
		[DataMember] public bool AllDays { get; set; }
		[DataMember] public bool IsExist { get; set; }
		[DataMember] public string[] ExpandList { get; set; }
		[DataMember] public string SelectedRow { get; set; }
	}
}
