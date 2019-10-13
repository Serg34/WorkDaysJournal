using Furmanov.Dal.Dto;
using Furmanov.MVP.CreateResource;
using Furmanov.MVP.EditResource;
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
		ICreateResourceView CreateResourceView { get; }
		event EventHandler<ResOPViewModel> CreatingResource;
		IEditResourceView EditResourceView { get; }
		event EventHandler EditingResource;
		event EventHandler<ResOPViewModel> DeletingResOp;

		event EventHandler<DateTime> ChangedMonth;
		event EventHandler WorkDaysOnlyClick;
		event EventHandler AllDaysClick;
		event EventHandler DeletingAllDays;
		event EventHandler<int> VedomostClick;

		event EventHandler<UndoRedoEventArgs<ResOPViewModel>> ChangedResOp;
		event EventHandler<UndoRedoEventArgs<ResOPViewModel>> ReplacingResource;
		event EventHandler<ResOPViewModel> SelectResource;

		event EventHandler<TabelViewModel> ChangedTabel;

		event EventHandler<int> Undo;
		event EventHandler<int> Redo;

		void UpdateLogin(object sender, UserView user);
		void UpdateAllResOps(object sender, MainViewModel viewModel);
		void UpdateSelectedResOp(object sender, SelectionResOpViewModel viewModel);
		void UpdateUndoRedo(IEnumerable<string> undoItems, IEnumerable<string> redoItems);
	}
}