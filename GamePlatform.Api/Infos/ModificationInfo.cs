using GamePlatform.Api.Infos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Games.Interfaces;

namespace GamePlatform.Api.Infos
{
    public class ModificationInfo : IModificationInfo
    {
        public string Message { get; set; }

        public IGameElement Modified { get; set; }
    }
}
