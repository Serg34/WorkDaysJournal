using Furmanov.MVP.MainView;

namespace Furmanov.MVP.EditResource.UndoRedoCommands
{
	public class EditResourceCmd : ICommand
	{
		public EditResourceCmd(IMainModel model, CResOP resOp, string resourceName)
		{
			_model = model;
			_resOp = resOp;
			Name = $"Назначение сотрудника '{resourceName}'";
		}

		private readonly IdataAccessService _db = new DataAccessService();

		private readonly IMainModel _model;
		private readonly CResOP _resOp;

		public string Name { get; }

		public void Execute()
		{
			_db.SaveResOp(_resOp);
			_model.Reload();
		}
		public void UnExecute()
		{
			_db.DeleteResOp(_resOp);
			_model.Reload();
		}
	}
}