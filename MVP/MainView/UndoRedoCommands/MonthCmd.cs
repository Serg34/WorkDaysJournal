using Furmanov.Services.UndoRedo;

namespace Furmanov.MVP.MainView.UndoRedoCommands
{
	public class MonthCmd : ICommand
	{
		public MonthCmd(IMainModel model, MonthEventArgs e)
		{
			_model = model;
			_year = e.Year;
			_month = e.Month;
			_prevYear = _model.Year;
			_prevMonth = _model.Month;
		}

		private readonly IMainModel _model;
		private readonly int _year;
		private readonly int _month;
		private readonly int _prevYear;
		private readonly int _prevMonth;

		public string Name => "Изменение месяца";

		public void Execute()
		{
			_model.ChangeMonth(_year, _month);
			_model.Update();
		}
		public void UnExecute()
		{
			_model.ChangeMonth(_prevYear, _prevMonth);
			_model.Update();
		}
	}
}