using Furmanov.MVP.Login;
using Furmanov.MVP.MainView.UndoRedoCommands;
using Furmanov.Services.UndoRedo;

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
				_view.UpdateLogin(user);
			};
			_model.Updated += (sender, modelView) =>
			{
				_view.UpdateMonth(sender, modelView);
				_view.UpdateSalaries(sender, modelView);
				_view.UpdateUndoRedo(_undoService.UndoItems, _undoService.RedoItems);
			};
			_model.SelectedSalaryPay += _view.UpdateDays;
			_model.Progress += _view.Progress;
			_model.ReportingBug += _view.ReportBug;

			_view.Logging += (sender, args) => ShowLoginView(false);
			_view.Logout += (sender, args) =>
			{
				_model.UpdateLogin(null);
				_undoService.Reset();
				ShowLoginView(false);
			};

			_view.RefillingDataBase += (sender, e) => _model.RefillDataBase();
			_view.ChangedMonth += (sender, e) =>
			{
				var cmd = new MonthCmd(_model, e);
				_undoService.Execute(cmd);
			};

			_view.WorkDaysOnlyClick += (sender, pay) =>
			{
				var days = _model.GenWorkedDays(pay.Id,false, true);
				var prevDays = _model.GetWorkedDays(pay.Id);
				var cmd = new WorkedDaysCmd(_model, days, prevDays);
				_undoService.Execute(cmd);
			};
			_view.AllDaysClick += (sender, pay) =>
			{
				var days = _model.GenWorkedDays(pay.Id, true, true);
				var prevDays = _model.GetWorkedDays(pay.Id);
				var cmd = new WorkedDaysCmd(_model, days, prevDays);
				_undoService.Execute(cmd);
			};
			_view.DeletingAllDays += (sender, pay) =>
			{
				var days = _model.GenWorkedDays(pay.Id, true, false);
				var prevDays = _model.GetWorkedDays(pay.Id);
				var cmd = new WorkedDaysCmd(_model, days, prevDays);
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
			_view.ReportingBug += _model.ReportBug;

			_model.Update();

			_view.Show();
			ShowLoginView(true);
		}

		private void ShowLoginView(bool isStartApp)
		{
			var model = _model.LoginModel;
			var view = _view.LoginView;
			model.SqlConnectingError += (sender, args) => _view.ShowSqlError();

			if (_loginPresenter == null)
			{
				_loginPresenter = new LoginPresenter(model, view);
			}
			_loginPresenter.ShowView(isStartApp);
		}
	}
}
