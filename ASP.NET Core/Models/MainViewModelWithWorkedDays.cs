using Furmanov.Data.Data;
using Furmanov.MVP.MainView;

namespace Furmanov.Models
{
	public class MainViewModelWithWorkedDays : MainViewModel
	{
		public MainViewModelWithWorkedDays(MainViewModel model)
		{
			Year = model.Year;
			Month = model.Month;
			SalaryPays = model.SalaryPays;
		}
		public WorkedDay[] WorkedDays { get; set; } = new WorkedDay[0];
	}
}
