using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Players;
using Moq;
using System.Reactive.Linq;
using GamePlatform.Api.Players.Interfaces;
using System.Reactive.Disposables;
using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;

namespace GamePlatformTest
{
    [TestClass]
    public class PlayerTests
    {
        private Mock<IModifierBus> _bus;
        private PlayerBase _player;
        private Mock<IDirectModifier<IPlayer, IGameElement>> _modifier;
        private Mock<IGameElement> _target;

        [TestInitialize]
        public void Setup()
        {
            _bus = new Moq.Mock<IModifierBus>();
            _target = new Mock<IGameElement>();
            var observable = new Mock<IObservable<IModifier>>();

            _player = new PlayerBase(_bus.Object);

            _modifier = new Mock<IDirectModifier<IPlayer, IGameElement>>();

            _modifier.SetupGet(m => m.Target).Returns(_target.Object);
        }

        [TestMethod]
        public void Player_Modifies()
        {
            _player.HeaveModifier(_modifier.Object);

            _modifier.VerifySet(m => m.Source = _player, Times.Once);
            _bus.Verify(b => b.OnNext(It.Is<IModifier>(x => x == _modifier.Object)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Player_ModifiesWithNoTarget_Exception()
        {
            _modifier.SetupGet(m => m.Target).Returns(() => { return null; });
            _player.HeaveModifier(_modifier.Object);
        }
    }
}
