using GamePlatform.Api.Boards.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using System.Collections.Generic;

namespace GamePlatform.Api.Games.Interfaces
{
    public interface IBoardGame<TPolygon, TPlayer> : IGameElement
        where TPolygon : IPolygon
        where TPlayer : IPlayer
    {
        bool IsStarted { get; }
        bool HasEnded { get; }
        List<TPlayer> Players { get; }
        IBoard<TPolygon> Board { get; }

        void Start();

        void End();

        void Join(IPlayer player);
    }
}