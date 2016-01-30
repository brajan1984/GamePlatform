using GamePlatform.Api.Entities;
using GamePlatform.Api.Games.Interfaces;

namespace GamePlatform.Api.Boards.Interfaces
{
    public interface IPolygon : IGameElement
    {
        Point3d Coordintes { get; set; }
    }
}