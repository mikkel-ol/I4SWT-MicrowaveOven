using System;
using NUnit.Framework;
using NSubstitute;
using Microwave.Core.Boundary;
using Microwave.Core.Interfaces;
using Microwave.Core.Controllers;

namespace Tests
{
    // System under test
    // Iteration 02, Light
    public class It02Sut2
    {
        private IDisplay _fakeDisp;
        private IOutput _fakeOut;
        private IPowerTube _fakePowerTube;

        // Double dependency
        private IUserInterface _fakeUI;

        private Timer _Timer;
        private Button _PBtn;
        private Button _TBtn;
        private Button _SCBtn;
        private Door _Door;
        private Light _Light;

        private CookController _sutCookCtrl;

        [SetUp]
        public void Setup()
        {
            _fakeDisp = Substitute.For<IDisplay>();
            _fakeOut = Substitute.For<IOutput>();
            _fakePowerTube = Substitute.For<IPowerTube>();
            _fakeUI = Substitute.For<IUserInterface>();

            _Timer = new Timer();
            _PBtn = new Button();
            _TBtn = new Button();
            _SCBtn = new Button();
            _Door = new Door();
            _Light = new Light(_fakeOut);

            _sutCookCtrl = new CookController(_Timer, _fakeDisp, _fakePowerTube, _fakeUI);
        }

        [TestCase(50, 30, TestName = "StartCooking ShouldCall PowerTubeTurnOn And TimerStart")]
        public void TestStartCooking(int power, int time)
        {
            _sutCookCtrl.StartCooking(power, time);

            _fakePowerTube.Received(1).TurnOn(power);
            _Timer.Received(1).Start(time);
        }

        [TestCase(50, 5, TestName = "OnTimerExpired ShouldCall PowerTubeTurnOff And UICookingIsDone")]
        public void TestOnTimerExpired(int power, int time)
        {
            _sutCookCtrl.StartCooking(power, time); // Set isCooking = true

            _sutCookCtrl.OnTimerExpired(null, EventArgs.Empty);

            _fakePowerTube.Received(1).TurnOff();
            _fakeUI.Received(1).CookingIsDone();
        }

        [TestCase(60, TestName = "OnTimerTick FakeTimeRemaining ShouldCall DisplayShowTime")]
        public void TestOnTimerTick(int timeRemaining)
        {
            _Timer.TimeRemaining.Returns(timeRemaining);

            _sutCookCtrl.OnTimerTick(null, EventArgs.Empty);

            _fakeDisp.Received(1).ShowTime(timeRemaining/60, timeRemaining % 60);
        }

        [TestCase(60, TestName = "OnTimerTick ShouldCall DisplayShowTime WithOneSecondLess")]
        public void TestOnTimerTickDisplayUpdate(int timeRemaining)
        {
            _Timer.TimeRemaining.Returns(timeRemaining);

            _sutCookCtrl.OnTimerTick(null, EventArgs.Empty);

            _fakeDisp.Received(1).ShowTime(timeRemaining / 60, timeRemaining % 60);
        }
    }
}