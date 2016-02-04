using GamePlatform.Api.Boards.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using GamePlatform.Api.Rulers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LonerBoardGame.Games
{
    public class LonerGame : ILonerGame, IDisposable
    {
        private IEnumerable<IModifierSeizer> _modifierSeizers;
        private IRuler _ruler;
        private Guid _id;

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

        public LonerGame(IRuler ruler, IBasicBoard board, IEnumerable<IModifierSeizer> modifierSeizers)
        {
            Board = board;
            _modifierSeizers = modifierSeizers;
            _ruler = ruler;
            _id = Guid.NewGuid();

            Players = new List<IPlayer>();
        }

        public void End()
        {
            _modifierSeizers.ToList().ForEach(m => m.Unsubscribe());

            IsStarted = false;
            HasEnded = true;

            (Players as List<IPlayer>).Clear();
        }

        public void Join(IPlayer player)
        {
            (Players as List<IPlayer>).Add(player);
        }

        public void Start()
        {
            Board.CreateBoard();
            IsStarted = true;
            HasEnded = false;

            _modifierSeizers.ToList().ForEach(m => m.Subscribe());
        }

        #region IDisposable Support

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}