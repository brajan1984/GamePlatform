using GamePlatform.Api.Infos.Interfaces;

namespace GamePlatform.Api.Infos
{
    public class GameLostInfo : IGameLostInfo
    {
        public string Message { get; set; }

        public GameLostInfo()
        {
            Message = "You lose!";
        }
    }
}