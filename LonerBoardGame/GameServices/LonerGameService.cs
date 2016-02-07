using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
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
        private Func<IPlayer> _playerFactory;
        private IList<ILonerModifierInitializer> _modifiersFactory;

        public IGame Game { get; }

        public LonerGameService(ILonerGame game, IList<ILonerModifierInitializer> modifiersFactory, Func<IPlayer> playerFactory)
        {
            Game = game;
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
            
            return modifierInitializer.Create<T>();
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Game.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}