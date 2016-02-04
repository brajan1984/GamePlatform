using Autofac;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Players;
using GamePlatform.Api.Players.Interfaces;
using GamePlatform.Api.Rulers;
using GamePlatform.Api.Rulers.Interfaces;
using GamePlatform.Api.Services.Interfaces;
using LonerBoardGame.Boards;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Games;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.GameServices;
using LonerBoardGame.Modifiers;
using LonerBoardGame.Modifiers.Initializers;
using LonerBoardGame.Modifiers.Interfaces;
using LonerBoardGame.Rules;
using LonerBoardGame.Rules.Interfaces;
using System.Reactive.Subjects;

namespace LonerBoardGame.Modules
{
    public class LonerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Subject<IInfo>>().As<ISubject<IInfo>>().InstancePerLifetimeScope();
            builder.RegisterType<ModifierBus>().As<IModifierBus>().InstancePerLifetimeScope();
            builder.RegisterType<StandardRules>()
                .As<IStandardRules>()
                .As<IRules>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<ModifierExecutor>().As<IModifierSeizer>().InstancePerLifetimeScope();
            builder.RegisterType<RulerBase>()
                .As<IRuler>()
                .As<IModifierSeizer>().InstancePerLifetimeScope();

            #region Modifiers

            builder.RegisterType<MakeMoveModifier>().As<IDirectModifier<IBasicBoard>>();
            builder.RegisterType<MakeMoveModifierInitializer>().As<IModifierInitializer>().InstancePerLifetimeScope();

            #endregion Modifiers

            builder.RegisterType<BasicBoard>().As<IBasicBoard>().InstancePerLifetimeScope();
            builder.RegisterType<PlayerBase>().As<IPlayer>();
            builder.RegisterType<LonerGame>().As<ILonerGame>().InstancePerLifetimeScope();

            builder.RegisterType<LonerGameService>().As<IGameService>();
        }
    }
}