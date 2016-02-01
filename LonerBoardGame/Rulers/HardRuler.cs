using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Rulers;
using LonerBoardGame.Rulers.Interfaces;
using LonerBoardGame.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Rulers
{
    public class HardRuler : RulerBase, IHardRuler
    {
        public HardRuler(IModifierBus bus, IHardRules rules, ISubject<IInfo> infoChannel)
            : base(bus, rules, infoChannel)
        {

        }
    }
}
