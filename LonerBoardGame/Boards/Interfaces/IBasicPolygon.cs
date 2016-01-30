using GamePlatform.Api.Boards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Boards.Interfaces
{
    public enum PolygonState
    {
        Empty,
        Filled,
        Solid
    }

    public interface IBasicPolygon : IPolygon
    {
        PolygonState State { get; set; }
    }
}
