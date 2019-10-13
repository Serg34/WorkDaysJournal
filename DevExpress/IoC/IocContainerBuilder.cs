using Autofac;
using Furmanov.Dal;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Furmanov.MVP.Services.UndoRedo;

namespace Furmanov.UI.IoC
{
	public static class IocContainerBuilder
	{
		public static IResolver Build()
		{
			IContainer container = null;

			var builder = new ContainerBuilder();
			var resolver = new Resolver(() => container);

			builder.Register(a => resolver)
				.As<IResolver>()
				.SingleInstance();

			builder.Register(a => new DbDataContext())
				.As<DbDataContext>()
				.SingleInstance();

			builder.RegisterType<DataAccessService>().As<IdataAccessService>().SingleInstance();

			builder.RegisterType<LoginModel>().As<ILoginModel>().SingleInstance();
			builder.RegisterType<MainModel>().As<IMainModel>().SingleInstance();
			builder.RegisterType<UndoRedoService>().As<IUndoRedoService>().SingleInstance();

			container = builder.Build();

			return resolver;
		}
	}
}
