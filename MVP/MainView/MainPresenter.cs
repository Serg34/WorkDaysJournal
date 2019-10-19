using Furmanov.MVP.Login;
using Furmanov.MVP.MainView.UndoRedoCommands;
using Furmanov.MVP.Services.UndoRedo;
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
				_view.UpdateLogin(sender, user);
			};
			_model.SalaryPayUpdated += (sender, modelView) =>
			{
				_view.UpdatePays(sender, modelView);
				_view.UpdateUndoRedo(_undoService.UndoItems, _undoService.RedoItems);
			};
			_model.SelectedSalaryPay += (sender, modelView) => _view.UpdateDays(sender, modelView);
			_model.Error += (sender, error) => _view.ShowError(error);

			_view.Logging += (sender, args) => ShowLoginView(false);
			_view.Logout += (sender, args) =>
			{
				_model.Logout();
				_undoService.Reset();
				ShowLoginView(false);
			};

			_view.ChangedMonth += (sender, month) =>
			{
				var cmd = new MonthCmd(_model, month);
				_undoService.Execute(cmd);
			};
			_view.VedomostClick += (sender, objectId) => _model.CreateVedomost(objectId);

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
			_view.SelectSalaryPay += (sender, viewModel) => _model.SelectSalaryPay(viewModel);
			_view.ChangedWorkedDay += (sender, viewModel) =>
			{
				var cmd = new WorkedDayCmd(_model, viewModel);
				_undoService.Execute(cmd);
			};

			_view.Undo += (sender, count) => _undoService.Undo(count);
			_view.Redo += (sender, count) => _undoService.Redo(count);

			_model.Reload();

			_view.Show();
			ShowLoginView(true);
		}

		private void ShowLoginView(bool isStartApp)
		{
			var model = _model.LoginModel;
			var view = _view.LoginView;
			if (_loginPresenter == null)
			{
				_loginPresenter = new LoginPresenter(model, view);
			}
			_loginPresenter.ShowView(isStartApp);
		}
	}
}
