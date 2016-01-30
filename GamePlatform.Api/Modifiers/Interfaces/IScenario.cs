﻿using System.Collections.Generic;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IScenario : IModifier
    {
        List<IModifier> Modifiers { get; set; }
    }
}