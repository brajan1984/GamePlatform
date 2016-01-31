using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Rulers.Interfaces;
using System;
using GamePlatform.Api.Infos.Interfaces;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using GamePlatform.Api.Infos;

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
                info.Message = "Invalid move: {ex.Message}";

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
    }
}