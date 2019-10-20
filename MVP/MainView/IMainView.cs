using Furmanov.Dal.Dto;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView.ViewModels;
using Furmanov.MVP.Services.UndoRedo;
using System;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView
{
	public interface IMainView : IView
	{
		ILoginView LoginView { get; }
		event EventHandler Logging;
		event EventHandler Logout;

		event EventHandler<DateTime> ChangedMonth;
		event EventHandler WorkDaysOnlyClick;
		event EventHandler AllDaysClick;
		event EventHandler DeletingAllDays;
		event EventHandler<int> ReportClick;

		event EventHandler<UndoRedoEventArgs<SalaryPayViewModel>> ChangedSalaryPay;
		event EventHandler<SalaryPayViewModel> SelectionChangingSalaryPay;

		event EventHandler<WorkedDayViewModel> ChangedWorkedDay;

		event EventHandler<int> Undo;
		event EventHandler<int> Redo;

		void UpdateLogin(UserViewModel user);
		void UpdateSalaries(object sender, MainViewModel viewModel);
		void UpdateDays(List<WorkedDayViewModel> viewModel);
		void UpdateUndoRedo(IEnumerable<string> undoItems, IEnumerable<string> redoItems);
	}
}