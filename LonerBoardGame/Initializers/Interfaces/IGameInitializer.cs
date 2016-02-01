using GamePlatform.Api.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Initializers.Interfaces
{
    public interface IGameInitializer
    {
        Type GameType { get; }
        Func<IGame> Factory { get; }
    }
}
