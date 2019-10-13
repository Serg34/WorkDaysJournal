using System;
using System.Data;

namespace Furmanov.Dal.Dto
{
	public class Position
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime dtc { get; set; }

		public decimal Salary { get; set; } // из базы данных Oklad

		public override string ToString() => Name;

		public static Position Factory(DataRow r) => new Position
		{
			Id = G._I(r["Id"]),
			Name = G._S(r["Name"]),
			dtc = G._D(r["dtc"]),
			Salary = G._Dec(r["Salary"]),
		};
	}
}
