using GamePlatform.Api.Infos;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Rulers.Interfaces;
using System;
using System.Reactive.Subjects;

namespace GamePlatform.Api.Rulers
{
    public class RulerBase : IRuler
    {
        private IModifierBus _bus;
        private IRules _rules;
        private IDisposable _subscription;

        private ISubject<IInfo> _infoChannel;

        public IObservable<IInfo> InfoChannel
        {
            get
            {
                return _infoChannel;
            }
        }

        public RulerBase(IModifierBus bus, IRules rules, ISubject<IInfo> infoChannel)
        {
            _bus = bus;
            _rules = rules;

            _infoChannel = infoChannel;
        }

        public virtual void OnCompleted()
        {
            _bus.OnCompleted();
        }

        public virtual void OnError(Exception error)
        {
            _bus.OnError(error);
        }

        public virtual void OnNext(IModifier value)
        {
            try
            {
                var scenario = _rules.Advise(value);

                _bus.OnNext(scenario);

                var postScenario = _rules.PostProcessing();

                _bus.OnNext(postScenario);

                IInfo gameStatus = _rules.ReportGameStatus();

                _infoChannel.OnNext(gameStatus);
            }
            catch (InvalidOperationException ex)
            {
                IErrorInfo info = new ErrorInfo();
                info.Error = ex;
                info.Message = "Invalid move: " + ex.Message;

                _infoChannel.OnNext(info);
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        public void Unsubscribe()
        {
            _bus.Unsubscribe(_subscription);
        }

        public void Subscribe()
        {
            _subscription = _bus.SubscribeOnCast(this);
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