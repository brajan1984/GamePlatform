using LonerBoardGame.Games.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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