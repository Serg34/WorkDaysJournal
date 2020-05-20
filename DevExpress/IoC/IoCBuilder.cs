using Autofac;
using Furmanov.Dal;
using Furmanov.Data;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Furmanov.Services.UndoRedo;

namespace Furmanov.UI.IoC
{
	public static class IoCBuilder
	{
		public static IResolver Build(string connectionString)
		{
			IContainer container = null;

			var builder = new ContainerBuilder();
			var resolver = new Resolver(() => container);

			builder.Register(a => resolver)
				.As<IResolver>()
				.SingleInstance();

			builder.Register(a => new DataAccessService(connectionString))
				.As<IDataAccessService>()
				.SingleInstance();

			builder.RegisterType<LoginModel>().As<ILoginModel>().SingleInstance();
			builder.RegisterType<MainModel>().As<IMainModel>().SingleInstance();
			builder.RegisterType<UndoRedoService>().As<IUndoRedoService>().SingleInstance();

			container = builder.Build();

			return resolver;
		}
	}
}
