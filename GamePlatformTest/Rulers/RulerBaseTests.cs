using GamePlatform.Api.Games.Interfaces;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Rulers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace GamePlatform.Api.Rulers.Tests
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
        private Mock<IScenario> _postScenario;
        private Mock<IInfo> _gameStatus;
        private Mock<ISubject<IInfo>> _infoChannel;
        private Mock<IModificationInfo> _modificationInfo;

        [TestInitialize]
        public void Setup()
        {
            _bus = new Moq.Mock<IModifierBus>();
            _target = new Mock<IGameElement>();
            _rules = new Mock<IRules>();
            _scenario = new Mock<IScenario>();
            _postScenario = new Mock<IScenario>();
            _gameStatus = new Mock<IInfo>();
            _modifier = new Mock<IDirectModifier<IGameElement>>();
            _modifier.SetupGet(m => m.Target).Returns(_target.Object);
            _rules.Setup(r => r.Advise(_modifier.Object)).Returns(_scenario.Object);
            _rules.Setup(r => r.PostProcessing()).Returns(_postScenario.Object);
            _rules.Setup(r => r.ReportGameStatus()).Returns(_gameStatus.Object);

            _infoChannel = new Mock<ISubject<IInfo>>();
            _modificationInfo = new Mock<IModificationInfo>();

            _ruler = new RulerBase(_bus.Object, _rules.Object, _infoChannel.Object);
        }

        [TestMethod]
        public void RulerBase_BroadcastsMessage()
        {
            _ruler.OnNext(_modifier.Object);
            _rules.Verify(r => r.Advise(It.Is<IDirectModifier<IGameElement>>(m => m == _modifier.Object)), Times.Once);

            _bus.Verify(b => b.OnNext(It.Is<IScenario>(s => s == _scenario.Object)), Times.Once);

            _rules.Verify(r => r.PostProcessing(), Times.Once);

            _bus.Verify(b => b.OnNext(It.Is<IScenario>(s => s == _postScenario.Object)), Times.Once);

            _rules.Verify(r => r.ReportGameStatus(), Times.Once);

            _infoChannel.Verify(i => i.OnNext(It.Is<IInfo>(x => x == _gameStatus.Object)), Times.Once);
        }

        [TestMethod]
        public void RulerBase_OnNext_ActionRejected()
        {
            var exception = new InvalidOperationException();
            _rules.Setup(r => r.Advise(_modifier.Object)).Throws(exception);
            _ruler.OnNext(_modifier.Object);

            _rules.Verify(r => r.Advise(It.Is<IDirectModifier<IGameElement>>(m => m == _modifier.Object)), Times.Once);

            _infoChannel.Verify(i => i.OnNext(It.Is<IInfo>(x => x is IErrorInfo && (x as IErrorInfo).Error == exception)), Times.Once);
        }

        [TestMethod()]
        public void RulerBase_OnError_Test()
        {
            var exception = new Exception();
            _ruler.OnError(exception);

            _bus.Verify(b => b.OnError(It.Is<Exception>(e => e == exception)), Times.Once);
        }

        [TestMethod()]
        public void RulerBase_OnCompleted_Test()
        {
            _ruler.OnCompleted();

            _bus.Verify(b => b.OnCompleted(), Times.Once);
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