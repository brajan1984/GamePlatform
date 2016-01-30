using LonerBoardGame.Boards;
using LonerBoardGame.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerTests.Games
{
    [TestClass]
    public class LonerGameTests
    {
        private LonerGame _game;

        [TestInitialize]
        public void Setup()
        {
            var board = new BasicBoard();

            _game = new LonerGame(null, board);
        }

        [TestMethod]
        public void LonerGame_Starts()
        {
            Assert.Fail();
        }
    }
}
