using System.Collections.Generic;
using System.Linq;

namespace Furmanov.MVP.Services.UndoRedo
{
	public interface IUndoRedoService
	{
		IEnumerable<string> UndoItems { get; }
		IEnumerable<string> RedoItems { get; }
		void Execute(ICommand command);
		void Undo(int count = 1);
		void Redo(int count = 1);

		void Reset();
	}

	public class UndoRedoService : IUndoRedoService
	{
		private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
		private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

		public IEnumerable<string> UndoItems => _undoStack.Select(c => c.Name);
		public IEnumerable<string> RedoItems => _redoStack.Select(c => c.Name);

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