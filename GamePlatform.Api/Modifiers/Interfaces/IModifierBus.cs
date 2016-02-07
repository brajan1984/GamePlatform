using GamePlatform.Api.Modifiers.Interfaces;
using System.Reactive.Subjects;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModifierBus : ISubject<IModifier>, IModifierSubscriber<IModifier>
    {
    }
}