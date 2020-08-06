using LinqToDB.Mapping;
using System;

namespace Furmanov.Data.Data
{
	[Table("BugIncident")]
	public class BugIncidentDto
	{
		[PrimaryKey, Identity] public int Id { get; set; }
		[Column] public int Bug_Id { get; set; }
		[Column] public string User { get; set; }
		[Column] public DateTime DateTime { get; set; }
	}
}
