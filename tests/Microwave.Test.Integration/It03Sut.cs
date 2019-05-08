using System;
using Microwave.Core.Boundary;
using Microwave.Core.Controllers;
using Microwave.Core.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    // System under test
    // Iteration 03
    public class It03Sut
    {
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timerButton;
        private IButton _startCancelButton;
        private ITimer _timer;
        private ILight _light;

        private UserInterface _ui;
        private CookController _cookCtrl;

        private IDisplay _fakeDisp;
        private IPowerTube _fakePowerTube;
        private IOutput _fakeOutput;

        [SetUp]
        public void Setup()
        {
            _door = new Door();
            _powerButton = new Button();
            _timerButton = new Button();
            _startCancelButton = new Button();

            _timer = new Timer();

            _fakeDisp = Substitute.For<IDisplay>();
            _fakePowerTube = Substitute.For<IPowerTube>();
            _fakeOutput = Substitute.For<IOutput>();

            _light = new Light(_fakeOutput);

            _cookCtrl = new CookController(_timer, _fakeDisp, _fakePowerTube);

            _ui = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _fakeDisp, _light, _cookCtrl);

            // Double dependency
            _cookCtrl.UI = _ui;
        }

        [TestCase(TestName = "Timer Expired EventFired")]
        public void TestTimerExpired()
        {
            var wait = new System.Threading.ManualResetEvent(false);
            EventArgs e = null;

            _timer.Expired += 
                (o, args) => 
                {
                    e = args;
                    wait.Set();
                };

            _timer.Start(1);

            wait.WaitOne();
            
            Assert.That(e, Is.Not.Null);
        }

        [TestCase(TestName = "Timer Tick EventFired")]
        public void TestTimerTick()
        {
            var wait = new System.Threading.ManualResetEvent(false);
            EventArgs e = null;

            _timer.TimerTick +=
                (o, args) =>
                {
                    e = args;
                    wait.Set();
                };

            _timer.Start(2);

            wait.WaitOne();

            Assert.That(e, Is.Not.Null);
        }

        [TestCase(TestName = "CookController TimerExpired")]
        public void TestOnTimerExpired()
        {
            // New buttons to get new callbacks
            _powerButton = new Button();
            _timerButton = new Button();
            _startCancelButton = new Button();

            // Fake timer to test dependency that way
            _timer = Substitute.For<ITimer>();
            _cookCtrl = new CookController(_timer, _fakeDisp, _fakePowerTube);
            _ui = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _fakeDisp, _light, _cookCtrl);

            _timer.Expired += Raise.Event();

            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();

            _fakeDisp.Received(1).Clear();
            _fakeOutput.Received(1).OutputLine(Arg.Any<string>());
        }

        [TestCase(TestName = "CookController TimerTick")]
        public void TestOnTimerTick()
        {
            // New buttons to get new callbacks
            _powerButton = new Button();
            _timerButton = new Button();
            _startCancelButton = new Button();

            // Fake timer to test dependency that way
            _timer = Substitute.For<ITimer>();
            _cookCtrl = new CookController(_timer, _fakeDisp, _fakePowerTube);
            _ui = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _fakeDisp, _light, _cookCtrl);

            _timer.TimerTick += Raise.Event();

            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();

            _fakeDisp.Received(1).Clear();
            _fakeOutput.Received(1).OutputLine(Arg.Any<string>());
        }

        [TestCase(TestName = "StartButton Starts PowerTube")]
        public void TestUserInterfaceStartPowertube()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();

            _fakePowerTube.Received(1).TurnOn(50);
        }

        [TestCase(TestName = "StopButton Stops PowerTube")]
        public void TestUserInterfaceStopPowertube()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            _fakePowerTube.Received(1).TurnOff();
        }

        [TestCase(TestName = "OpenDoor Stops PowerTube")]
        public void TestUserInterfaceOpenDoorStopPowertube()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startCancelButton.Press();
            _door.Open();

            _fakePowerTube.Received(1).TurnOff();
        }
    }
}