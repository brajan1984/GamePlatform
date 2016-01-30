using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModificationHeaver
    {
        void HeaveModifier<TTarget>(IDirectModifier<TTarget> modifier)
            where TTarget : IGameElement;
    }
}
