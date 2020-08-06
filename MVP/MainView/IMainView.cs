using Furmanov.Data.Data;
using Furmanov.MVP.Login;
using Furmanov.Services.UndoRedo;
using System;
using System.Collections.Generic;

namespace Furmanov.MVP.MainView
{
	public interface IMainView : IView
	{
		ILoginView LoginView { get; }
		event EventHandler Logging;
		event EventHandler Logout;

		event EventHandler<MonthEventArgs> ChangedMonth;
		event EventHandler WorkDaysOnlyClick;
		event EventHandler AllDaysClick;
		event EventHandler DeletingAllDays;

		event EventHandler<UndoRedoEventArgs<SalaryPay>> ChangedSalaryPay;
		event EventHandler<SalaryPay> SelectionChangingSalaryPay;

		event EventHandler<WorkedDay> ChangedWorkedDay;

		event EventHandler<int> Undo;
		event EventHandler<int> Redo;

		void UpdateLogin(User user);
		void UpdateMonth(object sender, MonthEventArgs monthEventArgs);
		void UpdateSalaries(object sender, MainViewModel viewModel);
		void UpdateDays(List<WorkedDay> viewModel);
		void UpdateUndoRedo(IEnumerable<string> undoItems, IEnumerable<string> redoItems);
	}
}