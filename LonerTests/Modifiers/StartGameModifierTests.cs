using LonerBoardGame.Modifiers;
using GamePlatform.Api.Modifiers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class StartGameModifierTests
    {
        private StartGameModifier _modifier;
        private Mock<ILonerGame> _game;

        [TestInitialize()]
        public void Setup()
        {
            _game = new Mock<ILonerGame>();

            _modifier = new StartGameModifier(_game.Object);
        }

        [TestMethod()]
        public void StartGameModifier_Modify()
        {
            _modifier.Modify();

            _game.Verify(g => g.Start(), Times.Once);
        }
    }
}
