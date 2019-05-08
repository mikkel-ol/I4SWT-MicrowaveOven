using System.Threading;
using Microwave.Core.Boundary;
using Microwave.Core.Controllers;
using Microwave.Core.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = Microwave.Core.Boundary.Timer;

namespace Microwave.Test.Integration
{
    // System under test
    // Iteration 03 Display
    public class It05Sut
    {
        private IOutput _fakeOutput;

        private Button _btnPower;
        private Button _btnTimer;
        private Button _btnStartCancel;

        private Door _door;
        private Timer _timer;
        private UserInterface _userInterface;
        private Light _light;
        private CookController _cookController;
        private PowerTube _powerTube;
        
        private Display _sutDisplay;

        [SetUp]
        public void Setup()
        {
            _fakeOutput = Substitute.For<IOutput>();

            _btnPower = new Button();
            _btnTimer = new Button();
            _btnStartCancel = new Button();

            _door = new Door();
            _timer = new Timer();

            _light = new Light(_fakeOutput);
            _powerTube = new PowerTube(_fakeOutput);

            /* System Under Test */
            _sutDisplay = new Display(_fakeOutput);

            _cookController = new CookController(_timer, _sutDisplay, _powerTube);
            _userInterface = new UserInterface(_btnPower, _btnTimer, _btnStartCancel, _door, _sutDisplay, _light, _cookController);
        }

        [Test]
        public void PowerButtonPressed_SystemIsReady_CorrectOutput()
        {
            _btnPower.Press();

            _fakeOutput.Received().OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void PowerButtonPressedTwice_SystemIsReady_CorrectOutput()
        {
            _btnPower.Press();
            _btnPower.Press();

            _fakeOutput.Received().OutputLine($"Display shows: 100 W");
        }

        [Test]
        public void PowerButtonPressed15Times_SystemIsReady_CorrectOutput()
        {
            for (int i = 0; i < 15; i++)
            {
                _btnPower.Press();

            }

            _fakeOutput.Received(2).OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void TimerButtonPressed_OnPowerSetup_CorrectOutput()
        {
            _btnPower.Press();
            _btnTimer.Press();

            _fakeOutput.Received().OutputLine($"Display shows: 01:00");
        }

        [Test]
        public void TimerButtonPressedTwice_OnPowerSetup_CorrectOutput()
        {
            _btnPower.Press();
            _btnTimer.Press();
            _btnTimer.Press();

            _fakeOutput.Received().OutputLine($"Display shows: 02:00");
        }


        [Test]
        public void StartCancelButtonPressed_OnPowerSetup_DisplayClear()
        {
            _btnPower.Press();
            _btnStartCancel.Press();

            _fakeOutput.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void StartCancelButtonPressed_WhileCooking_DisplayClear()
        {
            _btnPower.Press();
            _btnTimer.Press();
            _btnStartCancel.Press();

            _btnStartCancel.Press();

            _fakeOutput.Received(2).OutputLine($"Display cleared");
        }

        /*
         * Fejlede, da OnTimerEvent trækker 1000 sek.
         * fra TimeRemaining i stedet for 1 sek.
         */
        [Test]
        public void StartedCooking_Wait1Second_CorrectOutput()
        {
            _btnPower.Press();
            _btnTimer.Press();
            _btnStartCancel.Press();

            Thread.Sleep(1500); // make sure 1 second actually passes

            _fakeOutput.Received().OutputLine("Display shows: 00:59");
        }

        [Test]
        public void DoorOpened_OnPowerSetup_DisplayClear()
        {
            _btnPower.Press();
            _door.Open();

            _fakeOutput.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void DoorOpened_OnTimeSetup_DisplayClear()
        {
            _btnPower.Press();
            _btnTimer.Press();
            _door.Open();

            _fakeOutput.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void DoorOpened_WhileCooking_DisplayClear()
        {
            _btnPower.Press();
            _btnTimer.Press();
            _btnStartCancel.Press();
            _door.Open();

            _fakeOutput.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void CookingIseDone_WhileCooking_DisplayClear()
        {
            _btnPower.Press();
            _btnTimer.Press();
            _btnStartCancel.Press();

            _userInterface.CookingIsDone();

            _fakeOutput.Received().OutputLine($"Display cleared");
        }

    }
}