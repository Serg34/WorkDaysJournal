using System.Runtime.Serialization;

namespace Furmanov.Models
{
	[DataContract]
	public class ExceptionModel
	{
		[DataMember] public string TypeEx { get; set; }
		[DataMember] public string MessageEx { get; set; }
		[DataMember] public string MessageFull { get; set; }
	}
}
