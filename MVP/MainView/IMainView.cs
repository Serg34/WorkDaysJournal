using Furmanov.Data.Data;
using Furmanov.MVP.Login;
using Furmanov.Services;
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

		event EventHandler RefillingDataBase;
		event EventHandler<MonthEventArgs> ChangedMonth;
		event EventHandler WorkDaysOnlyClick;
		event EventHandler AllDaysClick;
		event EventHandler DeletingAllDays;

		event EventHandler<UndoRedoEventArgs<SalaryPay>> ChangedSalaryPay;
		event EventHandler<SalaryPay> SelectionChangingSalaryPay;

		event EventHandler<WorkedDay> ChangedWorkedDay;

		event EventHandler<int> Undo;
		event EventHandler<int> Redo;

		event EventHandler<BugEventArgs> ReportingBug;

		void UpdateLogin();
		void UpdateMonth(object sender, MainViewModel viewModel);
		void UpdateSalaries(object sender, MainViewModel viewModel);
		void UpdateDays(object sender, List<WorkedDay> viewModel);
		void UpdateUndoRedo(string[] undoItems, string[] redoItems);
		void Progress(object sender, ProgressEventArgs e);
		void ReportBug(object sender, BugEventArgs bug);
		void ShowSqlError();
	}
}