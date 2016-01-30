using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Boards.Interfaces
{
    public interface IBoard<out TPolygon> : IGameElement
        where TPolygon : IPolygon
    {
        TPolygon Cell { get; }
    }
}
