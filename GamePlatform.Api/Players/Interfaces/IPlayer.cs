using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;

namespace GamePlatform.Api.Players.Interfaces
{
    public interface IPlayer : IModificationHeaver, IGameElement
    {
    }
}