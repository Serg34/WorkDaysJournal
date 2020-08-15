using Autofac;
using Furmanov.Dal;
using Furmanov.Data;
using Furmanov.Data.Data;
using Furmanov.MVP.Login;
using Furmanov.MVP.MainView;
using Furmanov.Services.UndoRedo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Furmanov.IoC
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

			var loginPasswordRepository = new XmlRepository<List<LoginPassword>>(
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				Assembly.GetExecutingAssembly().FullName,
				"LoginPasswords.xml"));
			builder.Register(a => new DataAccessService(connectionString, loginPasswordRepository))
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
