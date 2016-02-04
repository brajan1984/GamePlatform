using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;

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
            Target.State = PolygonState.Empty;
        }
    }
}