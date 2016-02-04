using GamePlatform.Api.ModifierBus.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Modifiers.Interfaces;
using System;

namespace LonerBoardGame.Modifiers.Initializers
{
    public class MakeMoveModifierInitializer : IModifierInitializer
    {
        private IBasicBoard _board;

        public Type ModifierType
        {
            get { return typeof(MakeMoveModifier); }
        }

        public T Create<T>()
            where T : class, IModifier
        {
            return new MakeMoveModifier(_board) as T;
        }

        public MakeMoveModifierInitializer(IBasicBoard board)
        {
            _board = board;
        }
    }
}