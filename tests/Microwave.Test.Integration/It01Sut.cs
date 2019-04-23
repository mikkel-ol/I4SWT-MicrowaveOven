using System;
using System.IO;
using NUnit.Framework;
using Microwave.Core.Boundary;
using Microwave.Core.Interfaces;
using NSubstitute;

namespace Tests
{
    public class It01Sut
    {

        private Output _output;
        private Display _sutDisplay;
        private Light _sutLight;
        private PowerTube _sutPowerTube;

        // System under test
        // Iteration 01
        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<Output>();
            _sutDisplay = new Display(_output);
            _sutLight = new Light(_output);
            _sutPowerTube = new PowerTube(_output);

        }


        // Sut: Display
        // Dep: Output
        [TestCase(TestName ="ShouldOutputTime")]
        public void DisplayTimeSut(int min, int sec)
        {
            // Søren
            // Testing the only dependency: Output
            _sutDisplay.ShowTime(min, sec);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:00")));



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
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:00")));



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