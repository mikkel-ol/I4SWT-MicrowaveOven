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
        private ICookController _fakeCC;
        private IDisplay _fakeDisplay;
        private ILight _fakeLight;

        private Button _PBtn;
        private Button _TBtn;
        private Button _SCBtn;
        private Door _Door;

        private UserInterface _sutUI;

        // System under test
        // Iteration 01
        [SetUp]
        public void Setup()
        {
            _fakeCC = Substitute.For<ICookController>();
            _fakeDisplay = new Display(_fakeDisplay);
            _fakeLight = new Light(_fakeLight);

            _PBtn = new Button();
            _TBtn = new Button();
            _SCBtn = new Button();
            _Door = new Door();

            _sutUI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisplay, _fakeLight, _fakeCC);
        }


        // Sut: Display
        // Dep: Output
        [TestCase(TestName = "ShouldOutputPower")]
        public void UIDisplaySut()
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