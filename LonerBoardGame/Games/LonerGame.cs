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

namespace LonerBoardGame.Games
{
    public class LonerGame : ILonerGame
    {
        private IEnumerable<IModifierSeizer> _modifierSeizers;
        private IRuler _ruler;

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

        public LonerGame(IRuler ruler, IBoard<IBasicPolygon> board, IEnumerable<IModifierSeizer> modifierSeizers)
        {
            Board = board;
            _modifierSeizers = modifierSeizers;
            _ruler = ruler;

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
    }
}
