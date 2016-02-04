using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;

namespace GamePlatform.Api.Rulers.Interfaces
{
    public interface IRules : IGameElement
    {
        IScenario Advise(IModifier modifier);

        IScenario PostProcessing();

        IInfo ReportGameStatus();
    }
}