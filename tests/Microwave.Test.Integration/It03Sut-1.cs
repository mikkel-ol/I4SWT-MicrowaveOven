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

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}