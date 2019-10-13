using Furmanov.Dal.Dto;
using System;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView.ViewModels
{
	public class MainViewModel
	{
		public User User { get; set; }
		public DateTime Month { get; set; }
		public List<ResOPViewModel> ResOps { get; set; }
		public SelectionResOpViewModel SelectionResOp { get; set; }
	}
}
