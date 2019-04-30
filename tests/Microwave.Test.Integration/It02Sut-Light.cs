using Microwave.Core.Boundary;
using Microwave.Core.Controllers;
using Microwave.Core.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    // System under test
    // Iteration 02, Light
    public class It02SutLight
    {
        private ICookController _fakeCookCtrl;
        private IDisplay _fakeDisp;
        private IOutput _fakeOut;

        private Button _PBtn;
        private Button _TBtn;
        private Button _SCBtn;
        private Door _Door;
        private UserInterface _UI;

        private Light _sutLight;

        [SetUp]
        public void Setup()
        {
            _fakeCookCtrl = Substitute.For<ICookController>();
            _fakeDisp = Substitute.For<IDisplay>();
            _fakeOut = Substitute.For<IOutput>();

            _PBtn = new Button();
            _TBtn = new Button();
            _SCBtn = new Button();
            _Door = new Door();

            _sutLight = new Light(_fakeOut);

            _UI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisp, _sutLight, _fakeCookCtrl);
        }

        [TestCase(1, TestName = "OpenDoor StateReady ShouldCall OutputLine")]
        public void TestTurnOn(int result)
        {
            var wasCalledCount = 0;

            _Door.Opened += (o, e) => wasCalledCount++;
            _Door.Open();

            _fakeOut.Received(1).OutputLine("Light is turned on");  // Assert Output received this line
            Assert.That(wasCalledCount, Is.EqualTo(result));        // Assert Door.Open() was called once
        }

        [TestCase(2, TestName = "OpenDoor CloseDoor ShouldCall OutputLine")]
        public void TestTurnOff(int result)
        {
            var wasCalledCount = 0;

            _Door.Opened += (o, e) => wasCalledCount++;
            _Door.Closed += (o ,e) => wasCalledCount++;
            _Door.Open();
            _Door.Close();

            _fakeOut.Received(1).OutputLine("Light is turned off");  // Assert Output received this line
            Assert.That(wasCalledCount, Is.EqualTo(result));        // Assert Door.Open() and Door.Close() was called
        }
    }
}