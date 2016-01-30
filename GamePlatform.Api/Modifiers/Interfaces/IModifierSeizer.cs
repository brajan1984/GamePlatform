﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.ModifierBus.Interfaces
{
    public interface IModifierSeizer : IObserver<IModifier>
    {
        void Subscribe();
        void Unsubscribe();
    }
}