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

namespace LonerBoardGame.Games
{
    public class LonerGame : ILonerGame
    {
        public IBoard<IBasicPolygon> Board { get; private set; }

        public bool HasEnded { get; private set; }

        public bool IsStarted { get; private set; }

        public List<IPlayer> Players { get; private set; }

        public LonerGame(IRuler ruler, IBoard<IBasicPolygon> board)
        {
            Board = board;
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void Join(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
