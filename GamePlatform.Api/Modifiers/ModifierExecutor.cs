﻿using GamePlatform.Api.ModifierBus.Interfaces;
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
    }
}