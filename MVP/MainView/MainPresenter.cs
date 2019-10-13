using Furmanov.MVP.EditResource.UndoRedoCommands;
using Furmanov.MVP.MainView.UndoRedoCommands;
using SwissClean.Services.UndoRedo.Commands;
using System.Windows.Forms;

namespace Furmanov.MVP.MainView
{
	public class MainPresenter
	{
		private readonly IMainModel _model;
		private readonly IMainView _view;
		private readonly IUndoRedoService _undoService;

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
			_model.ResOpsUpdated += (sender, modelView) =>
			{
				_view.UpdateAllResOps(sender, modelView);
				_view.UpdateUndoRedo(_undoService.UndoItems, _undoService.RedoItems);
			};
			_model.SelectedResOp += (sender, modelView) => _view.UpdateSelectedResOp(sender, modelView);
			_model.Error += (sender, error) => _view.ShowError(error);

			_view.Logging += (sender, args) => ShowLoginView(false);
			_view.Logout += (sender, args) =>
			{
				_model.Logout();
				_undoService.Reset();
				ShowLoginView(false);
			};

			_view.CreatingResource += (sender, viewModel) => ShowCreateResourceView(viewModel);
			_view.EditingResource += (sender, args) => ShowEditResourceView();
			_view.ReplacingResource += (sender, args) =>
			{
				var cmd = new ReplaceResourceCmd(_model, args);
				_undoService.Execute(cmd);
			};
			_view.DeletingResOp += (sender, viewModel) =>
			{
				var cmd = new DeleteResOpCmd(_model, viewModel);
				_undoService.Execute(cmd);
			};

			_view.ChangedMonth += (sender, month) =>
			{
				var cmd = new MonthCmd(_model, month);
				_undoService.Execute(cmd);
			};
			_view.VedomostClick += (sender, objectId) => _model.CreateVedomost(objectId);

			_view.WorkDaysOnlyClick += (sender, args) =>
			{
				var tabels = _model.GenTabels(false, true);
				var cmd = new TabelsCmd(_model, tabels);
				_undoService.Execute(cmd);
			};
			_view.AllDaysClick += (sender, args) =>
			{
				var tabels = _model.GenTabels(true, true);
				var cmd = new TabelsCmd(_model, tabels);
				_undoService.Execute(cmd);
			};
			_view.DeletingAllDays += (sender, args) =>
			{
				var tabels = _model.GenTabels(true, false);
				var cmd = new TabelsCmd(_model, tabels);
				_undoService.Execute(cmd);
			};

			_view.ChangedResOp += (sender, args) =>
			{
				var cmd = new ResOpCmd(_model, args);
				_undoService.Execute(cmd);
			};
			_view.SelectResource += (sender, viewModel) => _model.SelectResOp(viewModel);
			_view.ChangedTabel += (sender, viewModel) =>
			{
				var cmd = new TabelCmd(_model, viewModel);
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
			new LoginPresenter(model, view, isStartApp);
		}

		private void ShowCreateResourceView(ResOPViewModel vm)
		{
			var model = _model.GetCreateResourceModel(vm);
			var view = _view.CreateResourceView;
			new CreateResourcePresenter(model, view, _undoService);
		}
		private void ShowEditResourceView()
		{
			var view = _view.EditResourceView;
			if (view.ShowDialog() == DialogResult.OK)
			{
				var cmd = new EditResourceCmd(_model, view.ResOp, view.ResourceName);
				_undoService.Execute(cmd);
			}
		}
	}
}
