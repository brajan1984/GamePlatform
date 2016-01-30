using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModifierBus : ISubject<IModifier>, IModifierSubscriber<IModifier>
    {  
    }
}
