using LinqToDB.Mapping;
using System;

namespace Furmanov.Data.Data
{
	public abstract class Dto
	{
		[PrimaryKey, Identity]
		public int Id { get; set; }

		[Column(SkipOnInsert = true, SkipOnUpdate = true)]
		public DateTime CreatedDate { get; set; }

		[Column] public DateTime? UpdatedDate { get; set; }
		[Column] public string UpdatedByUser { get; set; }
	}
}
