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

namespace LonerBoardGame.Rules.Tests
{
    [TestClass()]
    public class StandardRulesTests
    {
        private Mock<IBasicBoard> _board;
        private Mock<ILonerGame> _game;
        private StandardRules _rules;

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
                        var solid = new Mock<IBasicPolygon>();
                        solid.SetupGet(s => s.State).Returns(PolygonState.Solid);
                        solid.SetupGet(s => s.Coordintes).Returns(new Point3d() { X = x, Y = y });
                        testBoard.Add(solid.Object);
                    }
                    else
                    {
                        var empty = new Mock<IBasicPolygon>();
                        empty.SetupGet(s => s.State).Returns(PolygonState.Empty);
                        empty.SetupGet(s => s.Coordintes).Returns(new Point3d() { X = x, Y = y });
                        testBoard.Add(empty.Object);
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

            _game = new Mock<ILonerGame>();
            _game.SetupGet(g => g.Board).Returns(_board.Object);

            _rules = new StandardRules(_game.Object);
        }

        [TestMethod()]
        public void StandardRules_AdviseMoveCorrectlyHorizontallyFrontGameInProgress_Test()
        {
            var cellToFill = _board.Object.Cells.Where(c => c.Coordintes.X == 1 && c.Coordintes.Y == 2).First();
            cellToFill.State = PolygonState.Filled;

            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 1, Y = 2 }, new Point3d() { X = 3, Y = 2 });

            var scenario = _rules.Advise(modifier);

            scenario.Modifiers.Count.Should().Be(2);

            scenario.Modifiers[0].Should().Be(modifier);
            (scenario.Modifiers[1] as EmptyCellModifier).Should().NotBe(null);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.X.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Y.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Z.Should().Be(0);
        }

        [TestMethod()]
        public void StandardRules_AdviseMoveCorrectlyHorizontallyBackGameInProgress_Test()
        {
            var cellToFill = _board.Object.Cells.Where(c => c.Coordintes.X == 3 && c.Coordintes.Y == 2).First();
            cellToFill.State = PolygonState.Filled;

            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 3, Y = 2 }, new Point3d() { X = 1, Y = 2 });

            var scenario = _rules.Advise(modifier);

            scenario.Modifiers.Count.Should().Be(2);

            scenario.Modifiers[0].Should().Be(modifier);
            (scenario.Modifiers[1] as EmptyCellModifier).Should().NotBe(null);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.X.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Y.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Z.Should().Be(0);
        }

        [TestMethod()]
        public void StandardRules_AdviseMoveCorrectlyVerticallyFrontGameInProgress_Test()
        {
            var cellToFill = _board.Object.Cells.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 1).First();
            cellToFill.State = PolygonState.Filled;

            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 1 }, new Point3d() { X = 2, Y = 3 });

            var scenario = _rules.Advise(modifier);

            scenario.Modifiers.Count.Should().Be(2);

            scenario.Modifiers[0].Should().Be(modifier);
            (scenario.Modifiers[1] as EmptyCellModifier).Should().NotBe(null);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.X.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Y.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Z.Should().Be(0);
        }

        [TestMethod()]
        public void StandardRules_AdviseMoveCorrectlyVerticallyBackGameInProgress_Test()
        {
            var cellToFill = _board.Object.Cells.Where(c => c.Coordintes.X == 2 && c.Coordintes.Y == 3).First();
            cellToFill.State = PolygonState.Filled;

            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 3 }, new Point3d() { X = 2, Y = 1 });

            var scenario = _rules.Advise(modifier);

            scenario.Modifiers.Count.Should().Be(2);

            scenario.Modifiers[0].Should().Be(modifier);
            (scenario.Modifiers[1] as EmptyCellModifier).Should().NotBe(null);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.X.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Y.Should().Be(2);
            (scenario.Modifiers[1] as EmptyCellModifier).Target.Coordintes.Z.Should().Be(0);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellHorizontallyFront_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 1 }, new Point3d() { X = 3, Y = 1 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellVerticallyFront_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 1 }, new Point3d() { X = 2, Y = 2 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellHorizontallyBack_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 1 }, new Point3d() { X = 1, Y = 1 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellVerticallyBack_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 2 }, new Point3d() { X = 2, Y = 1 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellDiagonallyNE_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 2 }, new Point3d() { X = 3, Y = 1 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellDiagonallySE_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 2 }, new Point3d() { X = 3, Y = 3 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellDiagonallySW_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 2 }, new Point3d() { X = 1, Y = 3 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveOnNeighbourCellDiagonallyNW_Test()
        {
            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 2, Y = 2 }, new Point3d() { X = 1, Y = 1 });

            var scenario = _rules.Advise(modifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StandardRules_AdviseMoveCorrectlyOnSolid_Test()
        {
            var cellToFill = _board.Object.Cells.Where(c => c.Coordintes.X == 3 && c.Coordintes.Y == 1).First();
            cellToFill.State = PolygonState.Filled;

            cellToFill = _board.Object.Cells.Where(c => c.Coordintes.X == 3 && c.Coordintes.Y == 2).First();
            cellToFill.State = PolygonState.Filled;

            var modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 3, Y = 2 }, new Point3d() { X = 3, Y = 0 });

            var scenario = _rules.Advise(modifier);
        }
    }
}