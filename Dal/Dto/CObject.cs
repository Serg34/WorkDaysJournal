using System.Data;

namespace Furmanov.Dal.Dto
{
	public class CObject
	{
		public int Id { get; set; }
		public int? ManagerId { get; set; }
		public int ProjectId { get; set; }
		public string Address { get; set; }
	}
}
