using System;
using System.Data;

namespace Furmanov.Dal.Dto
{
	public class CTabel
	{
		public int Id { get; set; }
		public int? ResOP_Id { get; set; }
		public DateTime? Date { get; set; }

		public bool IsExit { get; set; } //Вспомогательное свойство

		public static CTabel Factory(DataRow r) => new CTabel
		{
			Id = G._I(r["Id"]),
			ResOP_Id = G._I(r["ResOP_Id"]),
			Date = G._D(r["Date"]),
			IsExit = true,
		};
	}
}
