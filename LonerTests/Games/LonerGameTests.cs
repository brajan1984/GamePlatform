using FluentAssertions;
using GamePlatform.Api.Boards.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using GamePlatform.Api.Rulers.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace LonerBoardGame.Games.Tests
{
    [TestClass()]
    public class EasyLonerGameTests
    {
        private Mock<IRuler> _ruler;
        private Mock<IModifierSeizer> _modifierExecutor;
        private Mock<IBasicBoard> _board;
        private LonerGame _game;
        private Mock<IPlayer> _player;

        [TestInitialize]
        public void Setup()
        {
            _ruler = new Mock<IRuler>();
            _modifierExecutor = new Mock<IModifierSeizer>();
            _board = new Mock<IBasicBoard>();
            _player = new Mock<IPlayer>();

            var modifierSeizers = new List<IModifierSeizer>();

            modifierSeizers.Add(_ruler.Object);
            modifierSeizers.Add(_modifierExecutor.Object);

            _game = new LonerGame(_ruler.Object, _board.Object, modifierSeizers);
        }

        [TestMethod()]
        public void EasyLonerGame_Start()
        {
            _game.Start();

            _game.IsStarted.Should().Be(true);
            _game.HasEnded.Should().Be(false);

            _board.Verify(b => b.CreateBoard(), Times.Once);

            _ruler.Verify(r => r.Subscribe(), Times.Once);
            _modifierExecutor.Verify(m => m.Subscribe(), Times.Once);
        }

        [TestMethod()]
        public void EasyLonerGame_End()
        {
            _game.End();

            _game.IsStarted.Should().Be(false);
            _game.HasEnded.Should().Be(true);

            _ruler.Verify(r => r.Unsubscribe(), Times.Once);
            _modifierExecutor.Verify(m => m.Unsubscribe(), Times.Once);
            (_game.Players as List<IPlayer>).Count.Should().Be(0);
        }

        [TestMethod()]
        public void EasyLonerGame_Join()
        {
            var player = new Mock<IPlayer>();

            _game.Join(player.Object);

            (_game.Players as List<IPlayer>)[0].Should().Be(player.Object);
        }
    }
}