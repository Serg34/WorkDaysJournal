using Furmanov.Dal.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Furmanov.MVP.MainView.ViewModels
{
	public class TabelViewModel
	{
		public int Id { get; set; } //в базе Tabel
		public int Object_Id { get; set; } //в базе Resource
		public int Resource_Id { get; set; } //в базе Resource
		public int? ResOP_Id { get; set; } //в базе Tabel

		[DisplayName("Дата")]
		public DateTime Date { get; set; } //в базе Tabel

		[DisplayName("Выход")]
		public bool IsExit { get; set; } //нет в базе


		public static TabelViewModel[] Factory(SalaryPay resOp, DateTime month, List<CTabel> tabels)
		{
			if (resOp == null) return Array.Empty<TabelViewModel>();

			var start = new DateTime(month.Year, month.Month, 1);
			var count = DateTime.DaysInMonth(month.Year, month.Month);

			var res = new TabelViewModel[count];
			for (int i = 0; i < count; i++)
			{
				var date = start.AddDays(i);
				var tabel = tabels?.FirstOrDefault(t => t.Date?.Date == date.Date);
				res[i] = new TabelViewModel
				{
					Id = tabel?.Id ?? -1,
					Object_Id = resOp.Object_Id ?? -1,
					Resource_Id = resOp.Resource_Id,
					ResOP_Id = resOp.Id,
					Date = date,
					IsExit = tabel?.IsExit ?? false,
				};
			}

			return res;
		}
	}
}
