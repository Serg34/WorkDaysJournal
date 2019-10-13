using Furmanov.Dal.Dto;
using Furmanov.MVP.Services.UndoRedo;
using Services;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class WorkedDayCmd : ICommand
	{
		public WorkedDayCmd(IMainModel model, WorkedDayVisual value)
		{
			_model = model;
			_value = value;
			_prevValue = Cloner.DeepCopy(value);
			_prevValue.IsWorked = !value.IsWorked;
		}

		private readonly IMainModel _model;
		private readonly WorkedDayVisual _value;
		private readonly WorkedDayVisual _prevValue;

		public string Name => $"Изменение дня выхода для сотрудника '{_model.CurrentEmployeeName}'";

		public void Execute()
		{
			_model.SaveWorkDays(_value);
		}
		public void UnExecute()
		{
			_model.SaveWorkDays(_prevValue);
		}
	}
}