namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class TabelCmd : ICommand
	{
		public TabelCmd(IMainModel model, TabelViewModel value)
		{
			_model = model;
			_value = value;
			_prevValue = new TabelViewModel
			{
				Id = value.Id,
				Date = value.Date,
				Object_Id = value.Object_Id,
				ResOP_Id = value.ResOP_Id,
				Resource_Id = value.Resource_Id,
				IsExit = !value.IsExit,
			};
		}

		private readonly IMainModel _model;
		private readonly TabelViewModel _value;
		private readonly TabelViewModel _prevValue;

		public string Name => $"Изменение дня выхода для сотрудника '{_model.CurrentResourceName}'";

		public void Execute()
		{
			_model.SaveTabel(_value);
		}
		public void UnExecute()
		{
			_model.SaveTabel(_prevValue);
		}
	}
}