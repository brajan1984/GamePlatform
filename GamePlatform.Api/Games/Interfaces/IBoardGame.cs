using GamePlatform.Api.Boards.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using System;
using System.Collections.Generic;

namespace GamePlatform.Api.Games.Interfaces
{
    public interface IBoardGame<TPolygon, TPlayer> : IGame, IGameElement
        where TPolygon : IPolygon
        where TPlayer : IPlayer
    {
        IEnumerable<TPlayer> Players { get; }
        IBoard<TPolygon> Board { get; }
    }
}