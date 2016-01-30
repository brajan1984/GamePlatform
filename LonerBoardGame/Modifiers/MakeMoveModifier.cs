using GamePlatform.Api.Entities;
using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using System;
using System.Linq;

namespace LonerBoardGame.Modifiers
{
    public class MakeMoveModifier : IDirectModifier<IBasicBoard>
    {
        public IGameElement Source { get; set; }

        public IBasicBoard Target { get; }

        public Point3d From { get; }
        public Point3d To { get; }
        
        public MakeMoveModifier(IBasicBoard target, Point3d from, Point3d to)
        {
            Target = target;
            From = from;
            To = to;
        }

        public void Modify()
        {
            var fromCell = Target.Cells.Where(c => c.Coordintes.X == From.X && c.Coordintes.Y == From.Y).FirstOrDefault();
            var toCell = Target.Cells.Where(c => c.Coordintes.X == To.X && c.Coordintes.Y == To.Y).FirstOrDefault();

            if (fromCell != null && toCell != null)
            {
                fromCell.State = PolygonState.Empty;
                toCell.State = PolygonState.Filled;
            }
        }
    }
}