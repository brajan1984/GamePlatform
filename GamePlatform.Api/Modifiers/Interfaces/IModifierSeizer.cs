using System;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModifierSeizer : IObserver<IModifier>
    {
        void Subscribe();

        void Unsubscribe();
    }
}