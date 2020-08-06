using Furmanov.Data.Data;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView
{
	public class MainViewModel
	{
		public User User { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public List<SalaryPay> SalaryPays { get; set; }
		public List<WorkedDay> WorkedDays { get; set; }
	}
}
