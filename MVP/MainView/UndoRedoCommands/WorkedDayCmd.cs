using Furmanov.Data.Data;
using Furmanov.Services;
using Furmanov.Services.UndoRedo;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class WorkedDayCmd : ICommand
	{
		public WorkedDayCmd(IMainModel model, WorkedDay value)
		{
			_model = model;
			_value = value;
			_prevValue = value.Clone();
			_prevValue.IsWorked = !value.IsWorked;
			Name = $"Изменение дня выхода для сотрудника '{_model.CurrentEmployeeName}'";
		}

		private readonly IMainModel _model;
		private readonly WorkedDay _value;
		private readonly WorkedDay _prevValue;

		public string Name { get; }

		public void Execute()
		{
			_model.SaveWorkedDays(_value);
		}
		public void UnExecute()
		{
			_model.SaveWorkedDays(_prevValue);
		}
	}
}