
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Modifiers.Interfaces;
using System;

namespace LonerBoardGame.Modifiers.Initializers
{
    public class MakeMoveModifierInitializer : ILonerModifierInitializer
    {
        private Func<IMakeMoveModifier> _creator;

        public Type ModifierType
        {
            get { return typeof(MakeMoveModifier); }
        }

        public T Create<T>()
            where T : class, IModifier
        {
            return _creator() as T;
        }

        public MakeMoveModifierInitializer(Func<IMakeMoveModifier> creator)
        {
            _creator = creator;
        }
    }
}