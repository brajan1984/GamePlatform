using Microsoft.VisualStudio.TestTools.UnitTesting;
using LonerBoardGame.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using GamePlatform.Api.Entities;
using LonerBoardGame.Boards.Interfaces;
using FluentAssertions;

namespace LonerBoardGame.Modifiers.Tests
{
    [TestClass()]
    public class EmptyCellModifierTests
    {
        [TestMethod()]
        public void EmptyCellModifier_Modify_Test()
        {
            var cell1 = new Mock<IBasicPolygon>();
            cell1.SetupGet(c1 => c1.Coordintes).Returns(new Point3d() { X = 0, Y = 0, Z = 0 });
            cell1.SetupGet(c1 => c1.State).Returns(PolygonState.Filled);

            var modifier = new EmptyCellModifier(cell1.Object);

            modifier.Modify();

            cell1.VerifySet(c => c.State = PolygonState.Empty, Times.Once);

            cell1.Object.Coordintes.X.Should().Be(0);
            cell1.Object.Coordintes.Y.Should().Be(0);
            cell1.Object.Coordintes.Z.Should().Be(0);
        }
    }
}