using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GamePlatform.Api.Modifiers.Modifiers.Tests
{
    [TestClass()]
    public class SimpleScenarioTests
    {
        [TestMethod()]
        public void SimpleScenario_Modify_Test()
        {
            var scenario = new SimpleScenario();
            var modifier = new Mock<IModifier>();

            scenario.Modifiers.Add(modifier.Object);
            scenario.Modifiers.Add(modifier.Object);
            scenario.Modifiers.Add(modifier.Object);

            scenario.Modify();

            modifier.Verify(m => m.Modify(), Times.Exactly(3));
        }
    }
}