using Furmanov.Dal.Dto;
using Furmanov.MVP.MainView;
using Furmanov.MVP.Services.UndoRedo;
using System.Collections.Generic;

namespace SwissClean.Services.UndoRedo.Commands
{
	public class WorkedDaysCmd : ICommand
	{
		public WorkedDaysCmd(IMainModel model, List<WorkedDayViewModel> value)
		{
			_model = model;
			_pay = _model.CurrentPay;
			_value = value;
			_prevValue = _model.CurrentTables;
			Name = $"Изменение дней выхода для сотрудника '{_model.CurrentEmployeeName}'";
		}

		private readonly IMainModel _model;
		private readonly SalaryPayViewModel _pay;
		private readonly List<WorkedDayViewModel> _value;
		private readonly List<WorkedDayViewModel> _prevValue;

		public string Name { get; }

		public void Execute()
		{
			var oldPay = _model.CurrentPay;
			_model.CurrentPay = _pay;
			_model.SaveWorkedDays(_value);
			_model.CurrentPay = oldPay;
		}
		public void UnExecute()
		{
			var oldPay = _model.CurrentPay;
			_model.CurrentPay = _pay;
			_model.SaveWorkedDays(_prevValue);
			_model.CurrentPay = oldPay;
		}
	}
}