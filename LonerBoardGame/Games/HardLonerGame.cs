using GamePlatform.Api.ModifierBus.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Rulers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Games
{
    public class HardLonerGame : LonerGame, IHardLonerGame
    {
        public HardLonerGame(IHardRuler ruler, IBasicBoard board, IEnumerable<IModifierSeizer> modifierSeizers)
            : base(ruler, board, modifierSeizers)
        {

        }
    }
}
