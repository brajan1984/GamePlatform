using GamePlatform.Api.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Infos.Interfaces
{
    public interface IModificationInfo : IInfo
    {
        IGameElement Modified { get; set; }
    }
}
