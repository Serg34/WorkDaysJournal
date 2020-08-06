using Furmanov.Data.Data;
using Furmanov.Services.UndoRedo;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class SalaryPayCmd : ICommand
	{
		public SalaryPayCmd(IMainModel model, UndoRedoEventArgs<SalaryPay> e)
		{
			_model = model;
			_value = e.Value;
			_prevValue = e.PrevValue;
			Name = $"Изменение оплаты сотруднику '{_value.Name}'";
		}

		private readonly IMainModel _model;
		private readonly SalaryPay _value;
		private readonly SalaryPay _prevValue;

		public string Name { get; }

		public void Execute()
		{
			_model.SaveSalaryPay(_value);
		}
		public void UnExecute()
		{
			_model.SaveSalaryPay(_prevValue);
		}
	}
}