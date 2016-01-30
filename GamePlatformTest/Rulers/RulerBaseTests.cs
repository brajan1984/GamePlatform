using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Rulers;
using GamePlatform.Api.Rulers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Reactive.Disposables;

namespace GamePlatformTest.Rulers
{
    [TestClass]
    public class RulerBaseTests
    {
        private Mock<IModifierBus> _bus;
        private Mock<IRules> _rules;
        private RulerBase _ruler;
        private Mock<IDirectModifier<IGameElement>> _modifier;
        private Mock<IGameElement> _target;
        private Mock<IScenario> _scenario;

        [TestInitialize]
        public void Setup()
        {
            _bus = new Moq.Mock<IModifierBus>();
            _target = new Mock<IGameElement>();
            _rules = new Mock<IRules>();
            _scenario = new Mock<IScenario>();
            _modifier = new Mock<IDirectModifier<IGameElement>>();
            _modifier.SetupGet(m => m.Target).Returns(_target.Object);
            _rules.Setup(r => r.Advise(_modifier.Object)).Returns(_scenario.Object);

            _ruler = new RulerBase(_bus.Object, _rules.Object);
        }

        [TestMethod]
        public void RulerBase_BroadcastsMessage()
        {
            _ruler.OnNext(_modifier.Object);
            _rules.Verify(r => r.Advise(It.Is<IDirectModifier<IGameElement>>(m => m == _modifier.Object)), Times.Once);
            _bus.Verify(b => b.OnNext(It.Is<IScenario>(s => s == _scenario.Object)), Times.Once);
        }

        [TestMethod]
        public void RulerBase_Subscribes()
        {
            _ruler.Subscribe();

            _bus.Verify(b => b.SubscribeOnCast(It.Is<RulerBase>(r => r == _ruler)), Times.Once);
        }

        [TestMethod]
        public void RulerBase_Unsubscribes()
        {
            var subscription = Disposable.Empty;
            _bus.Setup(b => b.SubscribeOnCast(_ruler)).Returns(subscription);

            _ruler.Subscribe();
            _ruler.Unsubscribe();

            _bus.Verify(b => b.Unsubscribe(It.Is<IDisposable>(s => s == subscription)), Times.Once);
        }
    }
}