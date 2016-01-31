using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LonerBoardGame.Games;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Rules;
using GamePlatform.Api.Rulers.Interfaces;
using GamePlatform.Api.ModifierBus;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Rulers;
using LonerBoardGame.Boards;
using GamePlatform.Api.Boards.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using GamePlatform.Api.Modifiers;
using System.Reactive.Subjects;
using GamePlatform.Api.Infos.Interfaces;

namespace LonerBoardGame.Modules
{
    public class LonerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Subject<IInfo>>().As<ISubject<IInfo>>().InstancePerLifetimeScope();
            builder.RegisterType<ModifierBus>().As<IModifierBus>().SingleInstance();
            builder.RegisterType<StandardRules>().As<IRules>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<ModifierExecutor>().As<IModifierSeizer>().InstancePerLifetimeScope();
            builder.RegisterType<RulerBase>()
                .As<IRuler>()
                .As<IModifierSeizer>().InstancePerLifetimeScope();
            builder.RegisterType<BasicBoard>().As<IBoard<IBasicPolygon>>().InstancePerLifetimeScope();
            builder.RegisterType<LonerGame>().As<ILonerGame>().InstancePerLifetimeScope();
        }
    }
}
