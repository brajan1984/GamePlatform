using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Players
{
    public class PlayerBase : IPlayer
    {
        private IModifierBus _bus;
        
        public PlayerBase(IModifierBus bus)
        {
            _bus = bus;
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
