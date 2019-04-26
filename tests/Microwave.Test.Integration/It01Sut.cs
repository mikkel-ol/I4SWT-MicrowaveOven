using System;
using System.IO;
using NUnit.Framework;
using Microwave.Core.Boundary;
using Microwave.Core.Interfaces;
using NSubstitute;
using Microwave.Core.Controllers;
using System.ComponentModel;
using System.Collections.Generic;

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

        private List<string> _receivedData;

        // System under test
        // Iteration 01
        [SetUp]
        public void Setup()
        {
            _fakeCC = Substitute.For<ICookController>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLight = Substitute.For<ILight>();

            _PBtn = new Button();
            _TBtn = new Button();
            _SCBtn = new Button();
            _Door = new Door();

            _sutUI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisplay, _fakeLight, _fakeCC);
        }


        // Sut: Door
        // Dep: Output
        [TestCase(TestName = "OpenDoor ShouldCallOnDoorOpened")]
        public void UIDisplaySut()
        {
            //int count = 0;

            _Door.Open();

            //_sutUI.OnDoorOpened += (sender, e) => { }
            //_Door.Open();
            //Assert.That(_Door.Opened[0], Is.EqualTo());
        }
    }
}
