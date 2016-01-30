using GamePlatform.Api.Games.Interfaces;
using System.Collections.Generic;

namespace GamePlatform.Api.Boards.Interfaces
{
    public interface IBoard<TPolygon> : IGameElement
        where TPolygon : IPolygon
    {
        IEnumerable<TPolygon> Cells { get; }

        void CreateBoard();
    }
}