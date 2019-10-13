namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class DeleteResOpCmd : ICommand
	{
		public DeleteResOpCmd(IMainModel model, ResOPViewModel value)
		{
			_model = model;
			_value = value;
		}

		private readonly IMainModel _model;
		private readonly ResOPViewModel _value;

		public string Name => $"Удаление оплаты сотруднику '{_value.Name}'";

		public void Execute()
		{
			_model.DeleteResOp();
		}
		public void UnExecute()
		{
			_model.SaveResOp(_value);
		}
	}
}