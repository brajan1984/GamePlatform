﻿using GamePlatform.Api.Entities;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Modifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonerTests.Modifiers
{
    [TestClass]
    public class MakeMoveModifierTests
    {
        private Mock<IBasicBoard> _board;
        private MakeMoveModifier _modifier;
        private Mock<IBasicPolygon> _cell1;
        private Mock<IBasicPolygon> _cell2;

        [TestInitialize]
        public void Setup()
        {
            _board = new Mock<IBasicBoard>();

            _cell1 = new Mock<IBasicPolygon>();
            _cell1.SetupGet(c1 => c1.Coordintes).Returns(new Point3d() { X = 0, Y = 0, Z = 0 });
            _cell1.SetupGet(c1 => c1.State).Returns(PolygonState.Filled);

            _cell2 = new Mock<IBasicPolygon>();
            _cell2.SetupGet(c1 => c1.Coordintes).Returns(new Point3d() { X = 1, Y = 1, Z = 0 });
            _cell2.SetupGet(c1 => c1.State).Returns(PolygonState.Empty);

            _board.SetupGet(b => b.Cells).Returns(new List<IBasicPolygon>()
            {
                _cell1.Object,
                _cell2.Object
            });
        }

        [TestMethod]
        public void MakeMoveModifier_Modify()
        {
            _modifier = new MakeMoveModifier(_board.Object, new Point3d() { X = 0, Y = 0 }, new Point3d() { X = 1, Y = 1 });

            _modifier.Modify();

            _cell1.VerifySet(c1 => c1.State = PolygonState.Empty);
            _cell2.VerifySet(c2 => c2.State = PolygonState.Filled);
        }
    }
}
