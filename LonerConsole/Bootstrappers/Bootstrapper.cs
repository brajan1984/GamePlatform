﻿using Autofac;
using GamePlatform.Api.Players;
using GamePlatform.Api.Players.Interfaces;
using LonerBoardGame.Games;
using LonerBoardGame.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public ILonerGame Start()
        {
            return _container.Resolve<ILonerGame>();
        }

        public IPlayer GetPlayer()
        {
            return _container.Resolve<IPlayer>();
        }
    }
}