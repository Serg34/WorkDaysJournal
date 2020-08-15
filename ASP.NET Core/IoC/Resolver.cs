using Autofac;
using System;
using System.Diagnostics;

namespace Furmanov.IoC
{
	public interface IResolver
	{
		TType Resolve<TType>();
		TType Resolve<TType>(string key);
	}
	public class Resolver : IResolver
	{
		private readonly Func<IContainer> _func;

		public Resolver(Func<IContainer> func)
		{
			_func = func;
		}

		[DebuggerStepThrough]
		public TType Resolve<TType>()
		{
			return _func().Resolve<TType>();
		}

		[DebuggerStepThrough]
		public TType Resolve<TType>(string key)
		{
			return _func().ResolveKeyed<TType>(key);
		}
	}
}
