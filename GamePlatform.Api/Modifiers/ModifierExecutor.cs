using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using System;

namespace GamePlatform.Api.ModifierBus
{
    public class ModifierExecutor : IModifierSeizer
    {
        private IModifierBus _bus;
        private IDisposable _subscription;

        public ModifierExecutor(IModifierBus bus)
        {
            _bus = bus;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IModifier value)
        {
            value.Modify();
        }

        public void Subscribe()
        {
            _subscription = _bus.SubscribeOnScenario(this);
        }

        public void Unsubscribe()
        {
            _bus.Unsubscribe(_subscription);
        }

        #region IDisposable Support

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _subscription.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}