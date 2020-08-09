using System.Collections.Generic;
using System.Linq;

namespace Furmanov.Services.UndoRedo
{
	public interface IUndoRedoService
	{
		string[] UndoItems { get; }
		string[] RedoItems { get; }
		void Execute(ICommand command);
		void Undo(int count = 1);
		void Redo(int count = 1);

		void Reset();
	}

	public class UndoRedoService : IUndoRedoService
	{
		private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
		private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

		public string[] UndoItems => _undoStack.Select(c => c.Name).ToArray();
		public string[] RedoItems => _redoStack.Select(c => c.Name).ToArray();

		public void Execute(ICommand command)
		{
			_undoStack.Push(command);
			_redoStack.Clear();
			command.Execute();
		}

		public void Undo(int count = 1)
		{
			if (count < 1) return;
			for (int i = 0; i < count; i++) Undo();
		}
		public void Redo(int count = 1)
		{
			if (count < 1) return;
			for (int i = 0; i < count; i++) Redo();
		}

		private void Undo()
		{
			var command = _undoStack.Pop();
			_redoStack.Push(command);
			command.UnExecute();
		}
		private void Redo()
		{
			var command = _redoStack.Pop();
			_undoStack.Push(command);
			command.Execute();
		}

		public void Reset()
		{
			_undoStack.Clear();
			_redoStack.Clear();
		}
	}
}