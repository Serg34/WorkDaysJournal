using Furmanov.Data.Data;
using System;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView
{
	public class MainViewModel
	{
		public User User { get; set; }
		public DateTime Month { get; set; }
		public List<SalaryPay> SalaryPays { get; set; }
		public List<WorkedDay> WorkedDays { get; set; }
	}
}
