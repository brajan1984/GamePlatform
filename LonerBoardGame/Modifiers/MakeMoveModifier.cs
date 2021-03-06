﻿using GamePlatform.Api.Entities;
using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Modifiers.Interfaces;
using System.Linq;

namespace LonerBoardGame.Modifiers
{
    public class MakeMoveModifier : IMakeMoveModifier
    {
        public IGameElement Source { get; set; }

        public IBasicBoard Target { get; }

        public Point3d From { get; set; }
        public Point3d To { get; set; }

        public MakeMoveModifier(IBasicBoard target)
        {
            Target = target;
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