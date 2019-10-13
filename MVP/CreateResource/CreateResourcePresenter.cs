using Furmanov.MVP.CreateResource.UndoRedoCommands;
using Furmanov.MVP.Services.UndoRedo;

namespace Furmanov.MVP.CreateResource
{
	public class CreateResourcePresenter
	{
		private readonly ICreateResourceModel _model;
		private readonly ICreateResourceView _view;
		private readonly IUndoRedoService _undoService;

		public CreateResourcePresenter(ICreateResourceModel model, ICreateResourceView view, IUndoRedoService undoService)
		{
			_model = model;
			_view = view;
			_undoService = undoService;

			_view.Creating += (sender, viewModel) =>
			{
				var cmd = new CreateResourceCmd(_model, viewModel);
				_undoService.Execute(cmd);
			};

			_model.Error += (sender, error) => _view.ShowError(error);
			_model.Changed += (sender, args) => _view.Close();

			_view.ShowDialog();
		}
	}
}
