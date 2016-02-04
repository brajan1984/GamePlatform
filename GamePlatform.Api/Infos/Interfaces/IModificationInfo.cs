using GamePlatform.Api.Games.Interfaces;

namespace GamePlatform.Api.Infos.Interfaces
{
    public interface IModificationInfo : IInfo
    {
        IGameElement Modified { get; set; }
    }
}