using System;
using NUnit.Framework;
using Microwave.Core.Boundary;
using Microwave.Core.Interfaces;
using NSubstitute;

namespace Tests
{
    public class It01Sut
    {

        private IOutput _output;
        private Display _sutDisplay;
        private Light _sutLight;
        private PowerTube _sutPowerTube;

        // System under test
        // Iteration 01
        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _sutDisplay = new Display(_output);
            _sutLight = new Light(_output);
            _sutPowerTube = new PowerTube(_output);

        }


        // Sut: Display
        // Dep: Output
        [TestCase(TestName ="DisplaySut")]
        [TestCase(TestName ="Should")]
        public void DisplaySut()
        {
            




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