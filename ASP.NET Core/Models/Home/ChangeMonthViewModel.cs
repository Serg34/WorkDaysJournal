using System.Runtime.Serialization;

namespace Furmanov.Models.Home
{
	[DataContract]
	public class ChangeMonthViewModel
	{
		[DataMember] public string Month { get; set; }
		[DataMember] public string[] ExpandList { get; set; }
		[DataMember] public string SelectedRow { get; set; }
	}
}
