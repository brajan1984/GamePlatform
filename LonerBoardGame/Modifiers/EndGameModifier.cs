using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Games.Interfaces;

namespace LonerBoardGame.Modifiers
{
    public class EndGameModifier : IDirectModifier<ILonerGame>
    {
        public IGameElement Source { get; set; }

        public ILonerGame Target { get; private set; }

        public EndGameModifier(ILonerGame game)
        {
            Target = game;
        }

        public void Modify()
        {
            Target.End();
        }
    }
}