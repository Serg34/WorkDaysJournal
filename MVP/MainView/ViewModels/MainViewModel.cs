using Furmanov.Dal.Dto;
using System;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView.ViewModels
{
	public class MainViewModel
	{
		public UserViewModel User { get; set; }
		public DateTime Month { get; set; }
		public List<SalaryPayViewModel> SalaryPays { get; set; }
		public List<WorkedDayViewModel> WorkedDays { get; set; }
	}
}
