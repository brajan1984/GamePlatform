using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using System;

namespace GamePlatform.Api.Games.Interfaces
{
    public interface IGame
    {
        bool IsStarted { get; }
        bool HasEnded { get; }
        IObservable<IInfo> InfoChannel { get; }

        void Start();

        void End();

        void Join(IPlayer player);
    }
}