using System.Runtime.Remoting.Channels;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView.UndoRedoCommands;
using Furmanov.Services.UndoRedo;
using LinqToDB.SqlQuery;
using SwissClean.Services.UndoRedo.Commands;

namespace Furmanov.MVP.MainView
{
	public class MainPresenter
	{
		private readonly IMainModel _model;
		private readonly IMainView _view;
		private readonly IUndoRedoService _undoService;
		private LoginPresenter _loginPresenter;

		public MainPresenter(IMainModel model, IMainView view, IUndoRedoService undoService)
		{
			_model = model;
			_view = view;
			_undoService = undoService;

			_model.LoginChanged += (sender, user) =>
			{
				_undoService.Reset();
				_view.UpdateLogin();
			};
			_model.Updated += (sender, modelView) =>
			{
				_view.UpdateMonth(sender, new MonthEventArgs(_model.Year, _model.Month));
				_view.UpdateSalaries(sender, modelView);
				_view.UpdateUndoRedo(_undoService.UndoItems, _undoService.RedoItems);
			};
			_model.SelectedSalaryPay += (sender, modelView) => _view.UpdateDays(modelView);
			_model.Progress += _view.Progress;
			_model.Error += (sender, ex) => _view.ShowError(ex);

			_view.Logging += (sender, args) => ShowLoginView(false);
			_view.Logout += (sender, args) =>
			{
				_model.Logout();
				_undoService.Reset();
				ShowLoginView(false);
			};

			_view.RefillingDataBase += (sender, e) => _model.RefillDataBase();
			_view.ChangedMonth += (sender, e) =>
			{
				var cmd = new MonthCmd(_model, e);
				_undoService.Execute(cmd);
			};

			_view.WorkDaysOnlyClick += (sender, args) =>
			{
				var days = _model.GenWorkedDays(false, true);
				var cmd = new WorkedDaysCmd(_model, days);
				_undoService.Execute(cmd);
			};
			_view.AllDaysClick += (sender, args) =>
			{
				var days = _model.GenWorkedDays(true, true);
				var cmd = new WorkedDaysCmd(_model, days);
				_undoService.Execute(cmd);
			};
			_view.DeletingAllDays += (sender, args) =>
			{
				var days = _model.GenWorkedDays(true, false);
				var cmd = new WorkedDaysCmd(_model, days);
				_undoService.Execute(cmd);
			};

			_view.ChangedSalaryPay += (sender, args) =>
			{
				var cmd = new SalaryPayCmd(_model, args);
				_undoService.Execute(cmd);
			};
			_view.SelectionChangingSalaryPay += (sender, viewModel) => _model.SelectSalaryPay(viewModel);
			_view.ChangedWorkedDay += (sender, viewModel) =>
			{
				var cmd = new WorkedDayCmd(_model, viewModel);
				_undoService.Execute(cmd);
			};

			_view.Undo += (sender, count) => _undoService.Undo(count);
			_view.Redo += (sender, count) => _undoService.Redo(count);

			_model.Update();

			_view.Show();
			ShowLoginView(true);
		}

		private void ShowLoginView(bool isStartApp)
		{
			var model = _model.LoginModel;
			var view = _view.LoginView;
			model.Exception += (sender, ex) =>
			{
				if (ex is System.Data.SqlClient.SqlException) _view.ShowSqlError();
				else { _view.ShowError(ex);}
			};

			if (_loginPresenter == null)
			{
				_loginPresenter = new LoginPresenter(model, view);
			}
			_loginPresenter.ShowView(isStartApp);
		}
	}
}
