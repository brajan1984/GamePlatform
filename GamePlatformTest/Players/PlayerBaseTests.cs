using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace GamePlatformTest
{
    [TestClass]
    public class PlayerTests
    {
        //class SomePlayer : PlayerBase
        //{
        //    public string SomeP { get; set; }

        //    public SomePlayer(IModifierBus bus)
        //        : base(bus)
        //    {
        //        SomeP = "aaa";
        //    }
        //}

        //class SomeMod : IDirectModifier<SomePlayer>
        //{
        //    public IGameElement Source { get; set; }

        //    public SomePlayer Target { private set; get; }

        //    public void Modify()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //class SomePlayer2 : SomePlayer
        //{
        //    public SomePlayer2(IModifierBus bus)
        //        : base(bus)
        //    {
        //    }
        //}

        private Mock<IModifierBus> _bus;
        private PlayerBase _player;
        private Mock<IDirectModifier<IGameElement>> _modifier;
        private Mock<IGameElement> _target;

        [TestInitialize]
        public void Setup()
        {
            _bus = new Moq.Mock<IModifierBus>();
            _target = new Mock<IGameElement>();
            var observable = new Mock<IObservable<IModifier>>();

            _player = new PlayerBase(_bus.Object);

            _modifier = new Mock<IDirectModifier<IGameElement>>();

            _modifier.SetupGet(m => m.Target).Returns(_target.Object);

            //SomePlayer p1 = new SomePlayer(_bus.Object);
            //SomePlayer p2 = new SomePlayer(_bus.Object);

            //IDirectModifier<IPlayer> g = new SomeMod();

            //p1.HeaveModifier(new SomeMod());
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