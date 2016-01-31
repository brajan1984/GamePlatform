using Microsoft.VisualStudio.TestTools.UnitTesting;
using LonerBoardGame.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonerBoardGame.Games.Interfaces;
using Moq;

namespace LonerBoardGame.Modifiers.Tests
{
    [TestClass()]
    public class EndGameModifierTests
    {
        [TestMethod()]
        public void EndGameModifier_Modify()
        {
            var _game = new Mock<ILonerGame>();

            var modifier = new EndGameModifier(_game.Object);

            modifier.Modify();

            _game.Verify(g => g.End());
        }
    }
}