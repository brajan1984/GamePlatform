using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Players.Interfaces;

namespace GamePlatform.Api.Services.Interfaces
{
    public interface IGameService : IGame
    {
        IGame Game { get; }

        T GetModifierOfType<T>()
            where T : class, IModifier;

        IPlayer GetNewPlayer();
    }
}