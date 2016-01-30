using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModificationHeaver
    {
        void HeaveModifier<TTarget>(IDirectModifier<TTarget> modifier)
            where TTarget : IGameElement;
    }
}