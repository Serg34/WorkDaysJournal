using Furmanov.Dal.Dto;
using Furmanov.MVP.MainView;
using Furmanov.MVP.Services.UndoRedo;
using System.Collections.Generic;

namespace SwissClean.Services.UndoRedo.Commands
{
	public class WorkedDaysCmd : ICommand
	{
		public WorkedDaysCmd(IMainModel model, List<WorkedDayVisual> value)
		{
			_model = model;
			_value = value;
			_prevValue = _model.CurrentTables;
		}

		private readonly IMainModel _model;
		private readonly List<WorkedDayVisual> _value;
		private readonly List<WorkedDayVisual> _prevValue;

		public string Name => $"Изменение дней выхода для сотрудника '{_model.CurrentEmployeeName}'";

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