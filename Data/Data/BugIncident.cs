using LinqToDB.Mapping;
using System;

namespace Furmanov.Data.Data
{
	[Table("BugIncident")]
	public class CBugIncidentDto
	{
		[PrimaryKey, Identity] public int ID { get; set; }
		[Column] public int Bug_ID { get; set; }
		[Column] public string User { get; set; }
		[Column] public DateTime DateTime { get; set; }
	}
}
