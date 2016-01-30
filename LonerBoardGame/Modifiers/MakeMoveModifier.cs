using GamePlatform.Api.Entities;
using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using System;

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
            throw new NotImplementedException();
        }
    }
}