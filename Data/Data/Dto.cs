using LinqToDB.Mapping;
using System;
using System.Runtime.Serialization;

namespace Furmanov.Data.Data
{
	[DataContract]
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
