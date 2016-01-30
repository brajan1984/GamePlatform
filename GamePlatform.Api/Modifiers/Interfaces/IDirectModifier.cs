using GamePlatform.Api.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Modifiers.Interfaces
{
    public interface IDirectModifier<in TSource, out TTarget> : ICastModifier
        where TSource : IGameElement
    {
        TSource Source { set; }
        TTarget Target { get; }
    }
}
