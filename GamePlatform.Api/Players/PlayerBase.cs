using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using System;

namespace GamePlatform.Api.Players
{
    public class PlayerBase : IPlayer
    {
        private IModifierBus _bus;
        private Guid _id;

        public PlayerBase(IModifierBus bus)
        {
            _bus = bus;
            _id = Guid.NewGuid();
        }

        public void HeaveModifier<TTarget>(IDirectModifier<TTarget> modifier)
            where TTarget : IGameElement
        {
            if (modifier.Target == null)
            {
                throw new InvalidOperationException("Can't heave modifier without target");
            }

            modifier.Source = this;
            _bus.OnNext(modifier);
        }
    }
}