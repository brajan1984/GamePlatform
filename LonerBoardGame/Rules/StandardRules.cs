using GamePlatform.Api.Rulers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.ModifierBus.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Modifiers;
using LonerBoardGame.Games.Interfaces;
using GamePlatform.Api.Entities;

namespace LonerBoardGame.Rules
{
    public class StandardRules : IRules
    {
        private readonly Dictionary<Type, Func<IModifier, IScenario>> _judge = new Dictionary<Type, Func<IModifier, IScenario>>();
        private readonly ILonerGame _game;

        public StandardRules(ILonerGame game)
        {
            _game = game;

            _judge.Add(typeof(MakeMoveModifier), m => { return MoveScenario(m as MakeMoveModifier); });
        }

        private bool CheckMoveCoordianted(Point3d from, Point3d to)
        {
            bool result = false;

            return result;
        }

        private IScenario MoveScenario(MakeMoveModifier _moveModifier)
        {
            return null;
        }

        public IScenario Advise(IModifier modifier)
        {
            throw new NotImplementedException();
        }
    }
}
