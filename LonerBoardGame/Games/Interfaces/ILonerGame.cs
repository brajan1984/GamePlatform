using Autofac.Features.OwnedInstances;
using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Games.Interfaces
{
    public interface ILonerGame : IBoardGame<IBasicPolygon, IPlayer>
    {
    }
}
