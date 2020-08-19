using System.Runtime.Serialization;
using Furmanov.Data.Data;

namespace Furmanov.Models
{
	[DataContract]
	public class SaveWorkedDayViewModel
	{
		[DataMember]
		public string[] ExpandList { get; set; }
		[DataMember]
		public string SelectedRow { get; set; }
		[DataMember]
		public WorkedDay WorkedDay { get; set; }
	}
}
