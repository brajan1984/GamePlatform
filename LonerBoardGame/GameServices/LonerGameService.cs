using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using GamePlatform.Api.Services.Interfaces;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LonerBoardGame.GameServices
{
    public class LonerGameService : IGameService
    {
        private Func<ILonerGame> _gameCreator;
        private Func<IPlayer> _playerFactory;
        private IList<IModifierInitializer> _modifiersFactory;

        public IGame Game
        {
            get
            {
                return _gameCreator();
            }
        }

        public LonerGameService(Func<ILonerGame> gameCreator, IList<IModifierInitializer> modifiersFactory, Func<IPlayer> playerFactory)
        {
            _gameCreator = gameCreator;
            _modifiersFactory = modifiersFactory;
            _playerFactory = playerFactory;
        }

        public bool HasEnded
        {
            get
            {
                return Game.HasEnded;
            }
        }

        public IObservable<IInfo> InfoChannel
        {
            get
            {
                return Game.InfoChannel;
            }
        }

        public bool IsStarted
        {
            get
            {
                return Game.IsStarted;
            }
        }

        public void End()
        {
            Game.End();
        }

        public void Join(IPlayer player)
        {
            Game.Join(player);
        }

        public IPlayer GetNewPlayer()
        {
            return _playerFactory();
        }

        public void Start()
        {
            Game.Start();
        }

        public T GetModifierOfType<T>()
            where T : class, IModifier
        {
            var modifierInitializer = _modifiersFactory.Where(m => m.ModifierType == typeof(T)).First();
            
            return _modifiersFactory[0].Create<T>();
        }
    }
}