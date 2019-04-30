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
    // Iteration 01
    public class It03Sut2
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
        public void ShowPower_WasCalled_PowerButtonPressed()
        {
            _btnPower.Press();

            _fakeOutput.Received().OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void ShowTime_WasCalled_TimerButtonPressed()
        {
            _btnTimer.Press();

            _fakeOutput.Received().OutputLine($"Display shows: 00:01");
        }

        [Test]
        public void Clear_WasCalled_StartCancelButtonPressed()
        {
            _btnPower.Press();
            _btnStartCancel.Press();

            _fakeOutput.Received().OutputLine($"Display cleared");
        }
    }
}