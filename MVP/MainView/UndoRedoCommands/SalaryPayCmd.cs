using Furmanov.Dal.Dto;
using Furmanov.MVP.Services.UndoRedo;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class SalaryPayCmd : ICommand
	{
		public SalaryPayCmd(IMainModel model, UndoRedoEventArgs<SalaryPayVisual> e)
		{
			_model = model;
			_value = e.Value;
			_prevValue = e.PevValue;
		}

		private readonly IMainModel _model;
		private readonly SalaryPayVisual _value;
		private readonly SalaryPayVisual _prevValue;

		/// <summary>
		/// Name for MenuItem
		/// </summary>
		public string Name => $"Изменение оплаты сотруднику '{_value.Name}'";

		/// <summary>
		/// Execute the command
		/// </summary>
		public void Execute()
		{
			_model.SaveResOp(_value);
		}
		/// <summary>
		/// UnExecute the command
		/// </summary>
		public void UnExecute()
		{
			_model.SaveResOp(_prevValue);
		}
	}
}