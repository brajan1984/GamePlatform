using LonerBoardGame.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Boards.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Rulers.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.Players;

namespace LonerBoardGame.Games
{
    public class EasyLonerGame : IEasyLonerGame, IDisposable
    {
        private IEnumerable<IModifierSeizer> _modifierSeizers;
        private IRuler _ruler;
        private Func<IPlayer> _playerCreator;

        public IBoard<IBasicPolygon> Board { get; private set; }

        public bool HasEnded { get; private set; }

        public bool IsStarted { get; private set; }

        public IEnumerable<IPlayer> Players { get; private set; }

        public IObservable<IInfo> InfoChannel
        {
            get
            {
                return _ruler.InfoChannel;
            }
        }

        public EasyLonerGame(IRuler ruler, IBoard<IBasicPolygon> board, IEnumerable<IModifierSeizer> modifierSeizers, Func<IPlayer> playerCreator)
        {
            Board = board;
            _modifierSeizers = modifierSeizers;
            _ruler = ruler;
            _playerCreator = playerCreator;

            Players = new List<IPlayer>();
        }

        public void End()
        {
            _modifierSeizers.ToList().ForEach(m => m.Unsubscribe());

            IsStarted = false;
            HasEnded = true;

            (Players as List<IPlayer>).Clear();
        }

        public IPlayer Join()
        {
            var player = _playerCreator();
            (Players as List<IPlayer>).Add(player);

            return player;
        }

        public void Start()
        {
            Board.CreateBoard();
            IsStarted = true;
            HasEnded = false;

            _modifierSeizers.ToList().ForEach(m => m.Subscribe());
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EasyLonerGame() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
