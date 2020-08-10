using Furmanov.Data.Data;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView
{
	public class MainViewModel
	{
		public User User { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public SalaryPay[] SalaryPays { get; set; }
	}
}
