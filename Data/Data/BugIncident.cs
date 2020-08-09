using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace Furmanov.Data.Data
{
	[Table("BugIncident")]
	public class BugIncidentDto
	{
		[PrimaryKey, Identity] public int Id { get; set; }
		[Column] public int Bug_Id { get; set; }
		[Column] public string User { get; set; }

		[Column(SkipOnInsert = true, SkipOnUpdate = true)]
		[DisplayFormat(DataFormatString = "dd.MM.yy HH:mm")]
		public DateTime DateTime { get; set; }
	}
}
