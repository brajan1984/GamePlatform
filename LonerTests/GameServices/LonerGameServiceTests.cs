using Microsoft.VisualStudio.TestTools.UnitTesting;
using LonerBoardGame.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using LonerBoardGame.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using LonerBoardGame.Modifiers.Interfaces;
using FluentAssertions;
using LonerBoardGame.Modifiers;

namespace LonerBoardGame.GameServices.Tests
{
    [TestClass()]
    public class LonerGameServiceTests
    {
        private LonerGameService _service;
        private Mock<ILonerGame> _game;
        private IList<ILonerModifierInitializer> _initializers;
        private Mock<ILonerModifierInitializer> _modifierInitializer1;
        private Mock<ILonerModifierInitializer> _modifierInitializer2;
        private Mock<MakeMoveModifier> _modifier;
        private Mock<IPlayer> _player;
        private Func<IPlayer> _playerCreator;
        
        [TestInitialize]
        public void Setup()
        {
            _game = new Mock<ILonerGame>();
            _player = new Mock<IPlayer>();
            _initializers = new List<ILonerModifierInitializer>();
            _modifierInitializer1 = new Mock<ILonerModifierInitializer>();
            _modifierInitializer1.SetupGet(m => m.ModifierType).Returns(typeof(EmptyCellModifier));
            _modifierInitializer2 = new Mock<ILonerModifierInitializer>();
            _modifierInitializer2.SetupGet(m => m.ModifierType).Returns(typeof(MakeMoveModifier));
            _modifier = new Mock<MakeMoveModifier>();
            _initializers.Add(_modifierInitializer1.Object);
            _initializers.Add(_modifierInitializer2.Object);
            _playerCreator = () => { return _player.Object; };

            _service = new LonerGameService(_game.Object, _initializers, _playerCreator);
        }

        [TestMethod()]
        public void LonerGameService_HasEnded()
        {
            bool _serviceHasEnded = _service.HasEnded;

            _game.VerifyGet(g => g.HasEnded, Times.Once);
        }

        [TestMethod()]
        public void LonerGameService_IsStarted()
        {
            bool _serviceHasEnded = _service.IsStarted;

            _game.VerifyGet(g => g.IsStarted, Times.Once);
        }

        [TestMethod()]
        public void LonerGameService_InfoChannel()
        {
            var infoChannel = _service.InfoChannel;

            _game.VerifyGet(g => g.InfoChannel, Times.Once);
        }

        [TestMethod()]
        public void LonerGameService_End()
        {
            _service.End();

            _game.Verify(g => g.End(), Times.Once);
        }

        [TestMethod()]
        public void LonerGameService_Join()
        {
            _service.Join(_player.Object);

            _game.Verify(g => g.Join(It.Is<IPlayer>(p => p == _player.Object)), Times.Once);
        }

        [TestMethod()]
        public void LonerGameService_GetNewPlayer()
        {
            var player = _service.GetNewPlayer();

            player.Should().Be(_player.Object);
        }

        [TestMethod()]
        public void LonerGameService_GetModifierOfType()
        {
            var modifier = _service.GetModifierOfType<MakeMoveModifier>();

            _modifierInitializer2.Verify(m => m.Create<MakeMoveModifier>(), Times.Once);
        }

        [TestMethod()]
        public void LonerGameService_Dispose()
        {
            _service.Dispose();

            _game.Verify(m => m.Dispose(), Times.Once);

            //After two disposals game Dispose should be called once
            _service.Dispose();

            _game.Verify(m => m.Dispose(), Times.Once);
        }
    }
}