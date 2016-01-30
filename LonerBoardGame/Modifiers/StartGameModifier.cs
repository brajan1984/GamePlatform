using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Games.Interfaces;
using LonerBoardGame.Games.Interfaces;

namespace LonerBoardGame.Modifiers
{
    public class StartGameModifier : IDirectModifier<ILonerGame>
    {
        public IGameElement Source { get; set; }

        public ILonerGame Target { get; private set; }

        public StartGameModifier(ILonerGame game)
        {
            Target = game;
        }

        public void Modify()
        {
            Target.Start();
        }
    }
}
