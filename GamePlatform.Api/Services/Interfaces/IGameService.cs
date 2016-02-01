using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Api.Services.Interfaces
{
    public interface IGameService : IGame
    {
        TModifier GetModifierOfType<TModifier>()
            where TModifier : class, IModifier;

        TPlayer GetNewPlayerOfType<TPlayer>()
            where TPlayer : class, IPlayer;

        TGame GetGameOfType<TGame>()
            where TGame : class, IGame;

    }
}
