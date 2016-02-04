using GamePlatform.Api.Infos.Interfaces;

namespace GamePlatform.Api.Infos
{
    public class GameInProgressInfo : IInfo
    {
        public string Message { get; set; }

        public GameInProgressInfo()
        {
            Message = "Game in progress";
        }
    }
}