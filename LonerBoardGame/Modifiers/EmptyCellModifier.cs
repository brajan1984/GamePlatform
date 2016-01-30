using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Games.Interfaces;

namespace LonerBoardGame.Modifiers
{
    public class EmptyCellModifier : IDirectModifier<IBasicPolygon>
    {
        public IGameElement Source { get; set; }

        public IBasicPolygon Target { get; private set; }

        public EmptyCellModifier(IBasicPolygon polygon)
        {
            Target = polygon;
        }

        public void Modify()
        {
            throw new NotImplementedException();
        }
    }
}
