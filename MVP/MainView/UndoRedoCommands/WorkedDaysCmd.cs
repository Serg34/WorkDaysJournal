using System.Collections.Generic;
using Furmanov.Data.Data;
using Furmanov.Services.UndoRedo;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class WorkedDaysCmd : ICommand
	{
		public WorkedDaysCmd(IMainModel model, WorkedDay[] value, WorkedDay[] prevValue)
		{
			_model = model;
			_value = value;
			_prevValue = prevValue;
			Name = $"Изменение дней выхода для сотрудника '{_model.CurrentEmployeeName}'";
		}

		private readonly IMainModel _model;
		private readonly WorkedDay[] _value;
		private readonly WorkedDay[] _prevValue;

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