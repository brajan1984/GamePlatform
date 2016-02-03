using Autofac.Features.OwnedInstances;
using GamePlatform.Api.Games.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Initializers.Interfaces
{
    public interface ITypedInitializer
    {
        Type GameType { get; }
    }

    public interface ICreator<T>
    {
        Func<Owned<T>> Creator { get; }
    }

    public interface IGameInitializer<T> : ITypedInitializer, ICreator<T>
    {

    }
}
