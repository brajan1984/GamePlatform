using FluentAssertions;
using LonerBoardGame.Boards;
using LonerBoardGame.Boards.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LonerTests.Boards
{
    [TestClass]
    public class BasicBoardTests
    {
        private BasicBoard _board;

        [TestInitialize]
        public void Setup()
        {
            _board = new BasicBoard();
        }

        [TestMethod]
        public void BasicBoard_Creates_MiddleCellIsEmpty()
        {
            _board.CreateBoard();

            var cells = _board.Cells;

            var middleCell = cells.Where(c => c.Coordintes.X == 3 && c.Coordintes.Y == 3 && c.Coordintes.Z == 0).ToList();

            middleCell.Count.Should().Be(1);
            middleCell[0].State.Should().Be(PolygonState.Empty);
        }

        [TestMethod]
        public void BasicBoard_Creates_EdgesSolid()
        {
            _board.CreateBoard();

            var cells = _board.Cells;

            var solidCells = cells.Where(c =>
                ((c.Coordintes.X >= 0 && c.Coordintes.X <= 1 && c.Coordintes.Y >= 0 && c.Coordintes.Y <= 1 && c.Coordintes.Z == 0) ||
                (c.Coordintes.X >= 5 && c.Coordintes.X <= 6 && c.Coordintes.Y >= 0 && c.Coordintes.Y <= 1 && c.Coordintes.Z == 0) ||
                (c.Coordintes.X >= 0 && c.Coordintes.X <= 1 && c.Coordintes.Y >= 5 && c.Coordintes.Y <= 6 && c.Coordintes.Z == 0) ||
                (c.Coordintes.X >= 5 && c.Coordintes.X <= 6 && c.Coordintes.Y >= 5 && c.Coordintes.Y <= 6 && c.Coordintes.Z == 0)) &&
                c.State == PolygonState.Solid).ToList();

            solidCells.Count.Should().Be(16);
        }

        [TestMethod]
        public void BasicBoard_Creates_InsideFilled()
        {
            _board.CreateBoard();

            var cells = _board.Cells;

            var filledCells = cells.Where(c =>
                (((c.Coordintes.X >= 0 && c.Coordintes.X <= 6 && c.Coordintes.Y >= 2 && c.Coordintes.Y <= 4 && c.Coordintes.Z == 0) ||
                (c.Coordintes.X >= 2 && c.Coordintes.X <= 4 && c.Coordintes.Y >= 0 && c.Coordintes.Y <= 6 && c.Coordintes.Z == 0)) &&
                c.State == PolygonState.Filled)).ToList();

            filledCells.Count.Should().Be(32);
        }
    }
}