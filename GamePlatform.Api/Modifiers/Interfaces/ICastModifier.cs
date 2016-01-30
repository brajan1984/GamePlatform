using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;

namespace GamePlatform.Api.Modifiers.Interfaces
{
    public interface ICastModifier : IModifier
    {
        IGameElement Source { get; set; }
    }
}