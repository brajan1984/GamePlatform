﻿using GamePlatform.Api.ModifierBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Rulers.Interfaces
{
    public interface IRules
    {
        IScenario Advise(IModifier modifier);
    }
}