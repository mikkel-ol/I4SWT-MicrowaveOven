using Microwave.Core.Boundary;
using Microwave.Core.Controllers;
using Microwave.Core.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    // System under test
    // Iteration 03
    // Mainly PowerTube
    public class It03Sut
    {

        private IDisplay _fakeDisp;
        private IOutput _fakeOut;
        private UserInterface _UI;
        private Timer _Timer;
        private Button _PBtn;
        private Button _TBtn;
        private Button _SCBtn;
        private Door _Door;
        private Light _Light;
        private CookController _CookCtrl;

        private PowerTube _SutPowerTube;

        [SetUp]
        public void Setup()
        {
            _fakeDisp = Substitute.For<IDisplay>();
            _fakeOut = Substitute.For<IOutput>();

            _Timer = new Timer();
            _PBtn = new Button();
            _TBtn = new Button();
            _SCBtn = new Button();
            _Door = new Door();
            _Light = new Light(_fakeOut);

            // New Integration
            _SutPowerTube = new PowerTube(_fakeOut);

            _CookCtrl = new CookController(_Timer, _fakeDisp, _SutPowerTube);
            _UI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisp, _Light, _CookCtrl);
            _CookCtrl.UI = _UI;

        }

        // Test power ved forskellige klik og ingen klik på powerbutton og et press for mange, om den så går tilbage til 50 eller fortsætter op
        // Test power ER  reset ved start-cancel klik
        // Test power IKKE bliver reset ved open door
        // Test power ER reset efter start-cancel klik under cooking state
        // Test power ER reset efter open-door under cooking state
        // Test power ved start-cancel, uden power? Kan man gøre som deres unit test exceptions?

        [Test]
        public void TurnOn_1PressOnPwrButton_PowerIs50()
        {
            _PBtn.Press();
            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        [Test]
        public void TurnOn_2PressOnPwrButton_PowerIs100()
        {
            _PBtn.Press();
            _PBtn.Press();
            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("100")));
        }

        [Test]
        public void TurnOn_3PressOnPwrButton_PowerIs150()
        {
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("150")));

        }

        [Test]
        public void TurnOn_8PressOnPwrButton_PowerIs400()
        {
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("400")));

        }


        [Test]
        public void TurnOn_14PressOnPwrButton_PowerIs700()
        {
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();

            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("700")));
        }

        [Test]
        public void TurnOn_15PressOnPwrButton_PowerIs50()
        {
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();
            _PBtn.Press();

            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        // Test power ER  reset ved start-cancel klik

        [Test]
        public void TurnOn_PwrButtonFollowedByStartCancelButton_PowerResetsTo50()
        {
            _PBtn.Press();
            _PBtn.Press(); // power == 100

            _SCBtn.Press(); // prompting reset

            _PBtn.Press(); // power == 50

            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        // Test power IKKE bliver reset ved open door
        [Test]
        public void TurnOn_PwrButtonFollowedByDoorOpenAndClosed_PowerNotResetStays100()
        {
            _PBtn.Press();
            _PBtn.Press(); // power == 100

            _Door.Open();
            _Door.Close();

            _PBtn.Press(); // power == 100

            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("100")));
        }

        // Test power ER reset efter start-cancel klik under cooking state
        [Test]
        public void TurnOn_StartCancelButtonPressedDuringCooking_PowerResetsTo50()
        {
            _PBtn.Press();
            _PBtn.Press(); // power == 100
            _TBtn.Press();
            _SCBtn.Press(); // starts cooking

            _SCBtn.Press(); // cancels cooking

            // To see if power is reset to 50
            _PBtn.Press();
            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        // Test power ER reset efter open-door under cooking state
        [Test]
        public void TurnOn_DoorOpenedDuringCooking_PowerResetsTo50()
        {
            _PBtn.Press();
            _PBtn.Press(); // power == 100
            _TBtn.Press();
            _SCBtn.Press(); // starts cooking

            _Door.Open();
            _Door.Close();

            // To see if power is reset to 50
            _PBtn.Press();
            _TBtn.Press();
            _SCBtn.Press();

            _fakeOut.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        [Test]
        public void TurnOn_StartPressedBeforePower_PowerNotTurnedOn()
        {
            _SCBtn.Press();

           // _SutPowerTube.DidNotReceive().TurnOn(50);
              _fakeOut.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        [Test]
        public void TurnOn_StartPressedBeforeTimer_PowerIs50()
        {
            _PBtn.Press();
            _SCBtn.Press();

            // _SutPowerTube.DidNotReceive().TurnOn(50);
            _fakeOut.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }
    }
}