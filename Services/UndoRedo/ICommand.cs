namespace Furmanov.MVP.Services.UndoRedo
{
	/// <summary>
	/// Support command undo/redo
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// Name for MenuItem
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Execute the command
		/// </summary>
		void Execute();
		/// <summary>
		/// UnExecute the command
		/// </summary>
		void UnExecute();
	}
}