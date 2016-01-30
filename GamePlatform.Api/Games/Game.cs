using GamePlatform.Api.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using GamePlatform.Api.Boards.Interfaces;

namespace GamePlatform.Api.Games
{
    public class Game : IGame
    {
        private IModifierBus _modifierBus = null;
        //private IList<IModifierSeizer> _modiferDesireres = null;

        public virtual bool IsStarted { get; protected set; }

        public virtual List<IPlayer> Players { get; protected set; }

        public virtual IPlayer TheWinnerIs { get; protected set; }

        public virtual bool HasEnded { get; protected set; }

        public virtual IBoard GameBoard { get; protected set; }

        public Game(IModifierBus bus, IBoard gameBoard/*, IList<IModifierSeizer> modifierDesirers*/)
        {
            _modifierBus = bus;
            //_modiferDesireres = modifierDesirers;

            GameBoard = gameBoard;
        }
        
        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void End()
        {
            throw new NotImplementedException();
        }
        
        public virtual void Join(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
