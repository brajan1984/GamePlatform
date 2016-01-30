using GamePlatform.Api.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Modifiers.Interfaces
{
    public interface IDirectModifier<out TTarget> : ICastModifier
        where TTarget : IGameElement
    {
        TTarget Target { get; }
    }
}
