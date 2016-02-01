using LonerBoardGame.Initializers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Games.Interfaces;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Games;

namespace LonerBoardGame.Initializers
{
    public class EasyLonerInitializer : IGameInitializer
    {
        public Func<IGame> Factory { get; private set; }

        public Type GameType { get; private set; }

        public EasyLonerInitializer(Func<ILonerGame> factory)
        {
            GameType = typeof(LonerGame);

            Factory = factory;
        }
    }
}
