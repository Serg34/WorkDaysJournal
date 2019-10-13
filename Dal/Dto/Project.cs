using System.Data;

namespace Furmanov.Dal.Dto
{
	public class CProject
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public static CProject Factory(DataRow r) => new CProject
		{
			Id = G._I(r["Id"]),
			Name = G._S(r["Name"]),
		};
	}
}
