using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Modifiers
{
    public class ModifierBus : IModifierBus
    {
        private ISubject<IModifier> _broadcaster = null;

        public ModifierBus()
        {
            _broadcaster = new Subject<IModifier>();
        }

        public void OnCompleted()
        {
            _broadcaster.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _broadcaster.OnError(error);
        }

        public void OnNext(IModifier value)
        {
            _broadcaster.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<IModifier> observer)
        {
            return _broadcaster.Subscribe(observer);
        }

        public IDisposable SubscribeOnCast(IObserver<IModifier> subscriber)
        {
            return _broadcaster.Where(x => x is ICastModifier).Subscribe(subscriber);
        }

        public IDisposable SubscribeOnScenario(IObserver<IModifier> subscriber)
        {
            return _broadcaster.Where(x => x is IScenario).Subscribe(subscriber);
        }

        public void Unsubscribe(IDisposable subscription)
        {
            subscription.Dispose();
        }
    }
}
