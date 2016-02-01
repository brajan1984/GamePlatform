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

namespace LonerBoardGame.GameServices
{
    public class LonerGameService : IGameService
    {
        private IEnumerable<IGameInitializer> _lonerGamesFactory;
        private Func<IPlayer> _playerFactory;
        private IGame _currentGame;

        public bool HasEnded
        {
            get
            {
                return _currentGame.HasEnded;
            }
        }

        public IObservable<IInfo> InfoChannel
        {
            get
            {
                return _currentGame.InfoChannel;
            }
        }

        public bool IsStarted
        {
            get
            {
                return _currentGame.IsStarted;
            }
        }

        public LonerGameService(IEnumerable<IGameInitializer> lonerGamesFactory, Func<IPlayer> playerFactory)
        {
            _lonerGamesFactory = lonerGamesFactory;
            _playerFactory = playerFactory;
        }

        public void End()
        {
            _currentGame.End();
        }

        public TGame GetGameOfType<TGame>() 
            where TGame : class, IGame 
        {
            var func = _lonerGamesFactory.Where(g => g.GameType == typeof(TGame)).FirstOrDefault();

            _currentGame = func.Factory();
            return  _currentGame as TGame;
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

        public void Join(IPlayer player)
        {
            _currentGame.Join(player);
        }

        public void Start()
        {
            _currentGame.Start();
        }
    }
}
