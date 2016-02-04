using GamePlatform.Api.ModifierBus.Interfaces;
using System.Collections.Generic;

namespace GamePlatform.Api.Modifiers.Modifiers
{
    public class SimpleScenario : IScenario
    {
        private readonly List<IModifier> _modifiers = new List<IModifier>();

        public List<IModifier> Modifiers
        {
            get
            {
                return _modifiers;
            }
        }

        public void Modify()
        {
            Modifiers.ForEach(m => m.Modify());
        }
    }
}