using GamePlatform.Api.Infos.Interfaces;

namespace GamePlatform.Api.Infos
{
    public class GameWonInfo : IGameWonInfo
    {
        public string Message { get; set; }

        public GameWonInfo()
        {
            Message = "You win!";
        }
    }
}