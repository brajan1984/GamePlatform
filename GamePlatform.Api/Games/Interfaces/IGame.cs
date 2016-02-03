using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Games.Interfaces
{
    public interface IGame
    {
        bool IsStarted { get; }
        bool HasEnded { get; }
        IObservable<IInfo> InfoChannel { get; }

        void Start();
        void End();
        IPlayer Join();
    }
}
