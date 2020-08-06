using System;

namespace Furmanov.Services.UndoRedo
{
	public class UndoRedoEventArgs<T> : EventArgs
	{
		public UndoRedoEventArgs(T value, T prevValue)
		{
			Value = value;
			PrevValue = prevValue;
		}

		public T Value { get; }
		public T PrevValue { get; }
	}
}
