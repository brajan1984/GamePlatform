using GamePlatform.Api.Boards.Interfaces;

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