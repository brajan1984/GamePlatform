using GamePlatform.Api.Modifiers.Interfaces;
using System;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModifierSubscriber<out TModifier>
        where TModifier : IModifier
    {
        IDisposable SubscribeOnCast(IObserver<TModifier> subscriber);

        IDisposable SubscribeOnScenario(IObserver<TModifier> subscriber);

        void Unsubscribe(IDisposable subscription);
    }
}