using Furmanov.Dal.Dto;
using Furmanov.MVP.Services.UndoRedo;
using Services;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class WorkedDayCmd : ICommand
	{
		public WorkedDayCmd(IMainModel model, WorkedDayViewModel value)
		{
			_model = model;
			_pay = _model.CurrentPay;
			_value = value;
			_prevValue = Cloner.DeepCopy(value);
			_prevValue.IsWorked = !value.IsWorked;
			Name = $"Изменение дня выхода для сотрудника '{_model.CurrentEmployeeName}'";
		}

		private readonly IMainModel _model;
		private readonly SalaryPayViewModel _pay;
		private readonly WorkedDayViewModel _value;
		private readonly WorkedDayViewModel _prevValue;

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