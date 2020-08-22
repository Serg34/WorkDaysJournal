using System.Runtime.Serialization;
using Furmanov.Data.Data;

namespace Furmanov.Models.Home
{
	[DataContract]
	public class SaveWorkedDayViewModel
	{
		[DataMember] public WorkedDay WorkedDay { get; set; }
		[DataMember] public string[] ExpandList { get; set; }
		[DataMember] public string SelectedRow { get; set; }
	}
}
