using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerBoardGame.Modifiers.Interfaces
{
    public interface IMakeMoveModifier : IDirectModifier<IBasicBoard>
    {
    }
}
