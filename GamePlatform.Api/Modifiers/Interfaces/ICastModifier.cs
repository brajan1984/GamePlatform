using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Modifiers.Interfaces
{
    public interface ICastModifier : IModifier
    {
        IGameElement Source { get; set; }
    }
}
