using Furmanov.MVP.Services.UndoRedo;

namespace Furmanov.MVP.CreateResource.UndoRedoCommands
{
	public class CreateResourceCmd : ICommand
	{
		public CreateResourceCmd(ICreateResourceModel model, CreateResourceViewModel value)
		{
			_model = model;
			_value = value;
		}

		private readonly ICreateResourceModel _model;
		private readonly CreateResourceViewModel _value;

		public string Name => $"Создание сотрудника '{_value.Name}'";

		public void Execute()
		{
			_model.CreateResource(_value);
		}
		public void UnExecute()
		{
			_model.DeleteResource(_value);
		}
	}
}