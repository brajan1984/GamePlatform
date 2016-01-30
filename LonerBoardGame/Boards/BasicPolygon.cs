using GamePlatform.Api.Boards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.Entities;
using LonerBoardGame.Boards.Interfaces;

namespace LonerBoardGame.Boards
{
    public class BasicPolygon : IBasicPolygon
    {
        public Point3d Coordintes { get; set; }

        public PolygonState State { get; set; }
    }
}
