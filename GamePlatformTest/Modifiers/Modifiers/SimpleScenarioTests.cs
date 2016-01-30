using Microsoft.VisualStudio.TestTools.UnitTesting;
using GamePlatform.Api.Modifiers.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using GamePlatform.Api.ModifierBus.Interfaces;

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