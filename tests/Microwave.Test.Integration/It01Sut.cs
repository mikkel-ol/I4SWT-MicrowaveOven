using System;
using System.IO;
using NUnit.Framework;
using Microwave.Core.Boundary;
using Microwave.Core.Interfaces;
using NSubstitute;
using Microwave.Core.Controllers;

namespace Tests
{
    public class It01Sut
    {
        private CookController _fakeCC;
        private Output _fakeOutput;
        private Button _fakePBtn;
        private Button _fakeTBtn;
        private Button _fakeSCBtn;

        private Display _sutDisplay;
        private Light _sutLight;
        private PowerTube _sutPowerTube;
        private Door _sutDoor;
        private UserInterface _sutUI;

        private int _powerLevel;
        private int _time;


        // System under test
        // Iteration 01
        [SetUp]
        public void Setup()
        {
            _fakeCC = Substitute.For<CookController>();
            _fakeOutput = Substitute.For<Output>();
            _fakePBtn = Substitute.For<Button>();
            _fakeTBtn = Substitute.For<Button>();
            _fakeSCBtn = Substitute.For<Button>();



            _sutDisplay = new Display(_fakeOutput);
            _sutLight = new Light(_fakeOutput);
            _sutPowerTube = new PowerTube(_fakeOutput);

            _sutUI = new UserInterface(_fakePBtn, _fakeTBtn, _fakeSCBtn, _sutDoor, _sutDisplay, _sutLight, _fakeCC);

            _powerLevel = 50;
            _time = 1;
        }


        // Sut: Display
        // Dep: Output
        [TestCase(TestName = "ShouldOutputPower")]
        public void UIDisplaySut()
        {
            // Søren
            // Using fake
            _fakePBtn.Press();
            // 
            //_sutDisplay.ShowTime(min, sec);
            //_sutUI.OnPowerPressed()
            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{_powerLevel}")));
        }



        // Sut: Light 
        // Dep: output
        [TestCase(TestName ="LightSut")]
        public void LightSut()
        {
            Assert.Pass();
        }

        // Sut: PowerTube
        // Dep: Output
        [TestCase(TestName = "PowerTubeSut")]
        public void PowerTubeSut()
        {
            Assert.Pass();
        }
    }
}