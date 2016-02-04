using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
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