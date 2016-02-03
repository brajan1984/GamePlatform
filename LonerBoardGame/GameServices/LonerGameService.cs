using GamePlatform.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Initializers.Interfaces;
using LonerBoardGame.Games;
using Autofac.Features.OwnedInstances;

namespace LonerBoardGame.GameServices
{
    public class LonerGameService : IGameService
    {
        private IEnumerable<IGameInitializer<ILonerGame>> _lonerGamesFactory;
        private Func<IPlayer> _playerFactory;
        private Owned<ILonerGame> _currentGame;

        public LonerGameService(IEnumerable<IGameInitializer<ILonerGame>> lonerGamesFactory, Func<IPlayer> playerFactory)
        {
            _lonerGamesFactory = lonerGamesFactory;
            _playerFactory = playerFactory;
        }

        public bool HasEnded
        {
            get
            {
                return _currentGame.Value.HasEnded; 
            }
        }

        public IObservable<IInfo> InfoChannel
        {
            get
            {
                return _currentGame.Value.InfoChannel;
            }
        }

        public bool IsStarted
        {
            get
            {
                return _currentGame.Value.IsStarted;
            }
        }

        public void End()
        {
            _currentGame.Value.End();
        }

        public Owned<TGameInterface> GetGameOfType<TGame, TGameInterface>()
            where TGame : class
        {
            var initializer = _lonerGamesFactory.Where(og => og.GameType == typeof(TGame)).First();

            if (initializer != null)
            {
                _currentGame = initializer.Creator();
            }

            return _currentGame as Owned<TGameInterface>;
        }

        public TModifier GetModifierOfType<TModifier>() where TModifier : class, IModifier
        {
            throw new NotImplementedException();
        }

        public TPlayer GetNewPlayerOfType<TPlayer>() where TPlayer : class, IPlayer
        {
            var func = _playerFactory;
            
            return func() as TPlayer;
        }

        public IPlayer Join()
        {
            return _currentGame.Value.Join();
        }

        public void Start()
        {
            _currentGame.Value.Start();
        }
    }
}
