using Microsoft.VisualStudio.TestTools.UnitTesting;
using LonerBoardGame.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonerBoardGame.Boards.Interfaces;
using Moq;
using GamePlatform.Api.Entities;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Modifiers;
using FluentAssertions;
using LonerBoardGame.Boards;
using GamePlatform.Api.Infos.Interfaces;

namespace LonerBoardGame.Rules.Tests
{
    [TestClass()]
    public class HardRulesTests
    {
        private Mock<IBasicBoard> _board;
        private Mock<IHardLonerGame> _game;
        private HardRules _rules;

        [TestInitialize]
        public void Setup()
        {
            _board = new Mock<IBasicBoard>();

            //0000000
            //0111110
            //0121210
            //0111110
            //0121110
            //0111110
            //0000000
            var testBoard = new List<IBasicPolygon>();

            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (y == 0 || y == 6 || x == 0 || x == 6)
                    {
                        var solid = new BasicPolygon();
                        solid.State = PolygonState.Solid;
                        solid.Coordintes = new Point3d() { X = x, Y = y };
                        testBoard.Add(solid);
                    }
                    else
                    {
                        var empty = new BasicPolygon();
                        empty.State = PolygonState.Empty;
                        empty.Coordintes = new Point3d() { X = x, Y = y };
                        testBoard.Add(empty);
                    }
                }
            }

            var cellToFill = testBoard.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 2).First();
            cellToFill.State = PolygonState.Filled;
            cellToFill = testBoard.Where(c => c.Coordintes.X == 4 && c.Coordintes.Y == 4).First();
            cellToFill.State = PolygonState.Filled;
            cellToFill = testBoard.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 4).First();
            cellToFill.State = PolygonState.Filled;
            
            _board.SetupGet(b => b.Cells).Returns(testBoard);

            _game = new Mock<IHardLonerGame>();
            _game.SetupGet(g => g.Board).Returns(_board.Object);

            _rules = new HardRules();
            _rules.Game = _game.Object;
        }
        
        [TestMethod]
        public void StandardRules_ReportGameStatus_Win()
        {
            var cellToEmpty = _board.Object.Cells.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 2).First();
            cellToEmpty.State = PolygonState.Empty;
            cellToEmpty = _board.Object.Cells.Where(c => c.Coordintes.X == 4 && c.Coordintes.Y == 4).First();
            cellToEmpty.State = PolygonState.Empty;
            cellToEmpty = _board.Object.Cells.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 4).First();
            cellToEmpty.State = PolygonState.Empty;

            var cellToFill = _board.Object.Cells.Where(c => c.Coordintes.X == 3 && c.Coordintes.Y == 3).First();
            cellToFill.State = PolygonState.Filled;

            var finished = _rules.ReportGameStatus();

            finished.Should().BeAssignableTo<IGameWonInfo>();
        }
        
        [TestMethod]
        public void StandardRules_ReportGameStatus_Lost()
        {
            var cellToEmpty = _board.Object.Cells.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 2).First();
            cellToEmpty.State = PolygonState.Empty;
            cellToEmpty = _board.Object.Cells.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 4).First();
            cellToEmpty.State = PolygonState.Empty;

            var finished = _rules.ReportGameStatus();

            finished.Should().BeAssignableTo<IGameLostInfo>();
        }
    }
}