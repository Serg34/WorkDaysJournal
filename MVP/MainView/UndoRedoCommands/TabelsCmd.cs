using Furmanov.MVP.MainView;
using System.Collections.Generic;

namespace SwissClean.Services.UndoRedo.Commands
{
	public class TabelsCmd : ICommand
	{
		public TabelsCmd(IMainModel model, List<CTabel> value)
		{
			_model = model;
			_value = value;
			_prevValue = _model.CurrentTabels;
		}

		private readonly IMainModel _model;
		private readonly List<CTabel> _value;
		private readonly List<CTabel> _prevValue;

		public string Name => $"Изменение дней выхода для сотрудника '{_model.CurrentResourceName}'";

		public void Execute()
		{
			_model.SaveTabels(_value);
		}
		public void UnExecute()
		{
			_model.SaveTabels(_prevValue);
		}
	}
}