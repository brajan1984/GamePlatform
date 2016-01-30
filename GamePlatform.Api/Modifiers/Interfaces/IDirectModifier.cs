using GamePlatform.Api.Games.Interfaces;

namespace GamePlatform.Api.Modifiers.Interfaces
{
    public interface IDirectModifier<out TTarget> : ICastModifier
        where TTarget : IGameElement
    {
        TTarget Target { get; }
    }
}