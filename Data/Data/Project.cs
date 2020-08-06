using LinqToDB.Mapping;

namespace Furmanov.Data.Data
{
	[Table("Project")]
	public class ProjectDto : Dto, IHasName
	{
		[Column] public string Name { get; set; }
		[Column] public int? Manager_Id { get; set; }
		[Column] public bool IsLoadTo1C { get; set; }
		[Column] public bool IsDeleted { get; set; }
		public override string ToString() => Name;
	}
}
