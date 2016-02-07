using Microsoft.VisualStudio.TestTools.UnitTesting;
using LonerBoardGame.Modifiers.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonerBoardGame.Modifiers.Interfaces;
using Moq;
using FluentAssertions;

namespace LonerBoardGame.Modifiers.Initializers.Tests
{
    [TestClass()]
    public class MakeMoveModifierInitializerTests
    {
        private MakeMoveModifierInitializer _initializer;
        private Mock<IMakeMoveModifier> _modifier;
        private Func<IMakeMoveModifier> _creator;

        [TestInitialize]
        public void Setup()
        {
            _modifier = new Mock<IMakeMoveModifier>();
            
            _creator = () => { return _modifier.Object; };

            _initializer = new MakeMoveModifierInitializer(_creator);
        }

        [TestMethod()]
        public void MakeMoveModifierInitializer_Create()
        {
            var createdModifier = _initializer.Create<IMakeMoveModifier>();

            createdModifier.Should().Be(_modifier.Object);
        }

        [TestMethod()]
        public void MakeMoveModifierInitializer_ModifierType()
        {
            _initializer.ModifierType.Should().Be(typeof(MakeMoveModifier));
        }
    }
}