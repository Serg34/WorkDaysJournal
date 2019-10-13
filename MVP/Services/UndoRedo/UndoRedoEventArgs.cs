using System;

namespace Furmanov.MVP.Services.UndoRedo
{
	public class UndoRedoEventArgs<T> : EventArgs
	{
		public UndoRedoEventArgs(T value, T pevValue)
		{
			Value = value;
			PevValue = pevValue;
		}

		public T Value { get; }
		public T PevValue { get; }
	}
}
