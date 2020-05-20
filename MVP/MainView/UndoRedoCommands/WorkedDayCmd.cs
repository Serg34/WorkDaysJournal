using Furmanov.Dal.Data;
using Furmanov.Services;
using Furmanov.Services.UndoRedo;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class WorkedDayCmd : ICommand
	{
		public WorkedDayCmd(IMainModel model, WorkedDay value)
		{
			_model = model;
			_pay = _model.CurrentPay;
			_value = value;
			_prevValue = Cloner.DeepCopy(value);
			_prevValue.IsWorked = !value.IsWorked;
			Name = $"Изменение дня выхода для сотрудника '{_model.CurrentEmployeeName}'";
		}

		private readonly IMainModel _model;
		private readonly SalaryPay _pay;
		private readonly WorkedDay _value;
		private readonly WorkedDay _prevValue;

		public string Name { get; }

		public void Execute()
		{
			var oldPay = _model.CurrentPay;
			_model.CurrentPay = _pay;
			_model.SaveWorkDay(_value);
			_model.CurrentPay = oldPay;
		}
		public void UnExecute()
		{
			var oldPay = _model.CurrentPay;
			_model.CurrentPay = _pay;
			_model.SaveWorkDay(_prevValue);
			_model.CurrentPay = oldPay;
		}
	}
}