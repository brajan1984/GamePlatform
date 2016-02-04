using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Infos.Interfaces;

namespace GamePlatform.Api.Infos
{
    public class ModificationInfo : IModificationInfo
    {
        public string Message { get; set; }

        public IGameElement Modified { get; set; }
    }
}