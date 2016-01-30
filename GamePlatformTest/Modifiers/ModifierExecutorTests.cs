using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.ModifierBus;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Reactive.Disposables;

namespace GamePlatformTest.Modifiers
{
    [TestClass]
    public class ModifierExecutorTests
    {
        private Mock<IModifierBus> _bus;
        private ModifierExecutor _modifierExecutor;
        private Mock<IDirectModifier<IGameElement>> _modifier;
        private Mock<IScenario> _scenario;

        [TestInitialize]
        public void Setup()
        {
            _bus = new Moq.Mock<IModifierBus>();
            _scenario = new Mock<IScenario>();
            _modifier = new Mock<IDirectModifier<IGameElement>>();
            _modifierExecutor = new ModifierExecutor(_bus.Object);

            _scenario.SetupGet(s => s.Modifiers).Returns(Enumerable.Range(0, 5).Select(x => new Mock<IModifier>().Object).ToList());
        }

        [TestMethod]
        public void ModifierExecutor_Subscribes()
        {
            _modifierExecutor.Subscribe();

            _bus.Verify(b => b.SubscribeOnScenario(It.Is<ModifierExecutor>(r => r == _modifierExecutor)), Times.Once);
        }

        [TestMethod]
        public void ModifierExecutor_Unsubscribes()
        {
            var subscription = Disposable.Empty;
            _bus.Setup(b => b.SubscribeOnScenario(_modifierExecutor)).Returns(subscription);

            _modifierExecutor.Subscribe();
            _modifierExecutor.Unsubscribe();

            _bus.Verify(b => b.Unsubscribe(It.Is<IDisposable>(s => s == subscription)), Times.Once);
        }

        [TestMethod]
        public void ModifierExecutor_Modifies()
        {
            _modifierExecutor.OnNext(_scenario.Object);

            _scenario.Verify(s => s.Modify(), Times.Once);
        }
    }
}