using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using System;

namespace GamePlatform.Api.Rulers.Interfaces
{
    public interface IRuler : IModifierSeizer, IGameElement
    {
        IObservable<IInfo> InfoChannel { get; }
    }
}