using GamePlatform.Api.Players.Interfaces;
using System.Collections.Generic;

namespace GamePlatform.Api.Games.Interfaces
{
    public interface IGame : IGameElement
    {
        bool IsStarted { get; }
        bool HasEnded { get; }
        IPlayer TheWinnerIs { get; }
        List<IPlayer> Players { get; }
        //IBoard Board { get; }

        void Start();

        void End();

        void Join(IPlayer player);
    }
}