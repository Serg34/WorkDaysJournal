using Furmanov.Data.Data;

namespace Furmanov.MVP.MainView
{
	public class MainViewModel
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public SalaryPay[] SalaryPays { get; set; }
	}
}
