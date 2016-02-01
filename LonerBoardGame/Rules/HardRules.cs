using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Rules
{
    public class HardRules : StandardRules, IHardRules
    {
        public HardRules()
            : base()
        {

        }

        protected override bool IsGameWon()
        {
            return base.IsGameWon();
        }
    }
}
