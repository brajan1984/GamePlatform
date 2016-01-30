﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.ModifierBus.Interfaces;
using System.Reactive.Linq;
using GamePlatform.Api.Rulers.Interfaces;
using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;

namespace GamePlatform.Api.Rulers
{
    public class RulerBase : IModifierSeizer, IGameElement
    {
        private IModifierBus _bus;
        private IRules _rules;
        private IDisposable _subscription;

        public RulerBase(IModifierBus bus, IRules rules)
        {
            _bus = bus;
            _rules = rules;
        }

        public virtual void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public virtual void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public virtual void OnNext(IModifier value)
        {
            var scenario = _rules.Advise(value);

            _bus.OnNext(scenario);
        }

        public void Subscribe()
        {
            _subscription = _bus.SubscribeOnCast(this);
        }

        public void Unsubscribe()
        {
            _bus.Unsubscribe(_subscription);
        }
    }
}