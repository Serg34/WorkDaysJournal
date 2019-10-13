using Furmanov.Dal.Dto;
using System;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView.ViewModels
{
	public class MainViewModel
	{
		public UserVisual User { get; set; }
		public DateTime Month { get; set; }
		public List<SalaryPayVisual> SalaryPays { get; set; }
		public List<WorkedDayVisual> WorkedDays { get; set; }
	}
}
