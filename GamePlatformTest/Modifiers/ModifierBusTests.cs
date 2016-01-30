using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatformTest.Modifiers
{
    [TestClass]
    public class BusTests
    {
        private ModifierBus _bus;
        private Mock<IDirectModifier<IPlayer, IGameElement>> _modifier;
        private Mock<IScenario> _scenario;
        private Mock<IObserver<IModifier>> _observer;

        [TestInitialize]
        public void Setup()
        {
            _bus = new ModifierBus();
            _modifier = new Mock<IDirectModifier<IPlayer, IGameElement>>();
            _scenario = new Mock<IScenario>();
            _observer = new Mock<IObserver<IModifier>>();
        }

        [TestMethod]
        public void ModifierBus_ObserverSubscribesOnCast()
        {
            _bus.SubscribeOnCast(_observer.Object);
            _observer.Verify(o => o.OnNext(It.Is<IModifier>(m => m == _modifier.Object)), Times.Never);

            _bus.OnNext(_modifier.Object);
            _bus.OnNext(_scenario.Object);
            _observer.Verify(o => o.OnNext(It.Is<IModifier>(m => m == _modifier.Object)), Times.Once);
        }

        [TestMethod]
        public void ModifierBus_ObserverSubscribesOnScenario()
        {
            _bus.SubscribeOnScenario(_observer.Object);
            _observer.Verify(o => o.OnNext(It.Is<IModifier>(m => m == _modifier.Object)), Times.Never);

            _bus.OnNext(_modifier.Object);
            _bus.OnNext(_scenario.Object);
            _observer.Verify(o => o.OnNext(It.Is<IModifier>(m => m == _scenario.Object)), Times.Once);
        }

        public void ModifierBus_ObserverUnsubscribes()
        {
            var subscription = _bus.SubscribeOnScenario(_observer.Object);
            _bus.OnNext(_scenario.Object);

            _observer.Verify(o => o.OnNext(It.Is<IModifier>(m => m == _scenario.Object)), Times.Once);

            _bus.Unsubscribe(subscription);

            _bus.OnNext(_scenario.Object);

            _observer.Verify(o => o.OnNext(It.Is<IModifier>(m => m == _scenario.Object)), Times.Never);

        }
    }
}
