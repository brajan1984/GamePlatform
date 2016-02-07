using GamePlatform.Api.ModifierBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Modifiers.Interfaces
{
    public interface IModifierInitializer
    {
        Type ModifierType { get; }

        T Create<T>()
            where T : class, IModifier;
    }
}
