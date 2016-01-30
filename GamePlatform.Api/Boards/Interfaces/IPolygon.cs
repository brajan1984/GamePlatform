using GamePlatform.Api.Entities;
using GamePlatform.Api.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Boards.Interfaces
{
    public interface IPolygon : IGameElement
    {
        Point3d Coordintes { get; set; }
    }
}
