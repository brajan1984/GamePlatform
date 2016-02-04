using GamePlatform.Api.ModifierBus.Interfaces;
using System;

namespace LonerBoardGame.Modifiers.Interfaces
{
    public interface IModifierInitializer
    {
        Type ModifierType { get; }

        T Create<T>()
            where T : class, IModifier;
    }
}