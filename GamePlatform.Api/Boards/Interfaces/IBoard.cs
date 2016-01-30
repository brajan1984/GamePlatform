using GamePlatform.Api.Games.Interfaces;
using System.Collections.Generic;

namespace GamePlatform.Api.Boards.Interfaces
{
    public interface IBoard<TPolygon> : IGameElement
        where TPolygon : IPolygon
    {
        List<TPolygon> Cells { get; }

        void CreateBoard();
    }
}