using LinqToDB.Mapping;

namespace Furmanov.Data.Data
{
	[Table("Object")]
	public class ObjectDto : Dto, IHasName
	{
		[Column] public string Name { get; set; }
		[Column] public string Address { get; set; }
		[Column] public int Project_Id { get; set; }
		[Column] public int? Manager_Id { get; set; }
		[Column] public bool IsDeleted { get; set; }
	}
}
