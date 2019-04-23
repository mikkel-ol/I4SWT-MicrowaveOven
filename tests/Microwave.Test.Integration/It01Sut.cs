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


        }


        // Sut: Display
        // Dep: Output
        [TestCase(TestName ="ShouldOutputTime")]
        public void DisplayTimeSut(int min, int sec)
        {
            // Søren
            // Testing the only dependency: Output
            _sutDisplay.ShowTime(min, sec);
            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:00")));



            Assert.Pass();
        }
        [TestCase(TestName = "ShouldOutputPower")]
        public void DisplayPowerSut(int power)
        {
            // Søren
            // Testing the only dependency: Output

            _sutDisplay.ShowPower(power);
            //_sutDisplay.ShowTime(power, power);


            _sutDisplay.ShowTime(0, 0);
            _fakeOutput.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:00")));



            Assert.Pass();
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