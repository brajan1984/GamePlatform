﻿using GamePlatform.Api.Infos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Infos
{
    public class GameLostInfo : IGameLostInfo
    {
        public string Message { get; set; }
    }
}