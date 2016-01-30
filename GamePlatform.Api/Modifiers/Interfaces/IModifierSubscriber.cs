using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
