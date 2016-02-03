using LonerBoardGame.Initializers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Games.Interfaces;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Games;
using Autofac.Features.OwnedInstances;

namespace LonerBoardGame.Initializers
{
    public class EasyLonerInitializer : IGameInitializer<ILonerGame>
    {
        public Type GameType { get; private set; }

        public Func<Owned<ILonerGame>> Creator { get; private set; }

        public EasyLonerInitializer(Func<Owned<ILonerGame>> creator)
        {
            GameType = typeof(EasyLonerGame);

            Creator = creator;
        }
    }
}
