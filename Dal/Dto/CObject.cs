using System.Data;

namespace Furmanov.Dal.Dto
{
	public class CObject
	{
		public int Id { get; set; }
		public int? Manager_Id { get; set; }
		public int Project_Id { get; set; }
		public string Address { get; set; }
	}
}
