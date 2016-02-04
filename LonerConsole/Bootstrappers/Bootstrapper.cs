using Autofac;
using Autofac.Features.OwnedInstances;
using GamePlatform.Api.Players;
using GamePlatform.Api.Players.Interfaces;
using GamePlatform.Api.Services.Interfaces;
using System;
using System.Linq;

namespace LonerConsole.Bootstrappers
{
    public class Bootstrapper
    {
        private IContainer _container;

        public void Configure()
        {
            var builder = new ContainerBuilder();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetTypes().Any(x => x.IsAssignableTo<Autofac.Module>() && x != typeof(Autofac.Module))).ToArray();

            builder.RegisterAssemblyModules(assemblies);

            builder.RegisterType<PlayerBase>().As<IPlayer>();

            _container = builder.Build();
        }

        public Func<Owned<IGameService>> Get()
        {
            return _container.Resolve<Func<Owned<IGameService>>>();
        }

        public IPlayer GetPlayer()
        {
            return _container.Resolve<IPlayer>();
        }
    }
}