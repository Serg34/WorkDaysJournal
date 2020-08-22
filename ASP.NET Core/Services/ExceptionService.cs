using System;

namespace Furmanov.Services
{
	public class ExceptionService
	{
		private Exception _exception;
		private static readonly object LockObject = new object();

		public Exception Exception
		{
			get => _exception;
			set
			{
				lock (LockObject)
				{
					_exception = value;
				}
			}
		}
	}
}
