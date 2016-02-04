using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using LonerBoardGame.Boards.Interfaces;

namespace LonerBoardGame.Games.Interfaces
{
    public interface ILonerGame : IBoardGame<IBasicPolygon, IPlayer>
    {
    }
}