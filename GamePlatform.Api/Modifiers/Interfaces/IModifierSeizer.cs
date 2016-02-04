using System;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModifierSeizer : IObserver<IModifier>, IDisposable
    {
        void Subscribe();

        void Unsubscribe();
    }
}