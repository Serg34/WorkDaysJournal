using Furmanov.Dal.Dto;
using Furmanov.MVP.Services.UndoRedo;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class SalaryPayCmd : ICommand
	{
		public SalaryPayCmd(IMainModel model, UndoRedoEventArgs<SalaryPayViewModel> e)
		{
			_model = model;
			_value = e.Value;
			_prevValue = e.PevValue;
			Name = $"Изменение оплаты сотруднику '{_value.Name}'";
		}

		private readonly IMainModel _model;
		private readonly SalaryPayViewModel _value;
		private readonly SalaryPayViewModel _prevValue;

		public string Name { get; }

		public void Execute()
		{
			_model.SaveResOp(_value);
		}
		public void UnExecute()
		{
			_model.SaveResOp(_prevValue);
		}
	}
}