using System;
using System.IO;
using NUnit.Framework;
using Microwave.Core.Boundary;
using Microwave.Core.Interfaces;
using NSubstitute;
using Microwave.Core.Controllers;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;

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
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLight = Substitute.For<ILight>();

            _PBtn = new Button();
            _TBtn = new Button();
            _SCBtn = new Button();
            _Door = new Door();

        }

        // OnDoorOpen Tests
        // From all states to States.DOOROPEN
        [TestCase(1, TestName = "OpenDoor StateREADY ShouldCall_fakeLight(1)")]
        public void TestDoorLight(int result)
        {
            _sutUI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisplay, _fakeLight, _fakeCC);

            var wasCalledCount = 0;
            _Door.Opened += (o, e) => wasCalledCount = ++wasCalledCount; 

            _Door.Open();

            _fakeLight.Received(1).TurnOn();
            Assert.That(wasCalledCount, Is.EqualTo(result));
        }

        
        [TestCase(2, TestName = "OpenDoor StateSETPOWER ShouldCall_fakeLight(1)_fakeDisplay(1)")]
        public void TestDoorPowerLight(int result)
        {
            _sutUI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisplay, _fakeLight, _fakeCC);

            var wasCalledCount = 0;
            _Door.Opened += (o, e) => wasCalledCount = ++wasCalledCount;
            _PBtn.Pressed += (o, e) => wasCalledCount = ++wasCalledCount;

            _PBtn.Press();  // 1
            _Door.Open();   // 2

            _fakeDisplay.Received(1).Clear();   // 1
            _fakeLight.Received(1).TurnOn();    // 2
            Assert.That(wasCalledCount, Is.EqualTo(result));
        }

        [TestCase(3, TestName = "OpenDoor StateSETTIME ShouldCall_fakeLight(1)_fakeDisplay(3)")]
        public void TestDoorPowerLightTime(int result)
        {
            _sutUI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisplay, _fakeLight, _fakeCC);

            var wasCalledCount = 0;
            _Door.Opened += (o, e) => wasCalledCount = ++wasCalledCount;
            _PBtn.Pressed += (o, e) => wasCalledCount = ++wasCalledCount;
            _TBtn.Pressed += (o, e) => wasCalledCount = ++wasCalledCount;

            _PBtn.Press();  // 1
            _TBtn.Press();  // 2
            _Door.Open();   // 3


            _fakeDisplay.Received(1).ShowPower(Arg.Is<int>(50));                // 1 
            _fakeDisplay.Received(1).ShowTime(Arg.Is<int>(1), Arg.Is<int>(0));  // 2
            _fakeDisplay.Received(1).Clear();                                   // 3 
            _fakeLight.Received(1).TurnOn();                                    // 3
            Assert.That(wasCalledCount, Is.EqualTo(result));
        }

        [TestCase(4, TestName = "OpenDoor StateCOOKING ShouldCall_fakeLight(1)_fakeDisplay(3)")]
        public void TestDoorStart(int result)
        {
            _sutUI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisplay, _fakeLight, _fakeCC);

            var wasCalledCount = 0;
            _Door.Opened += (o, e) => wasCalledCount = ++wasCalledCount;
            _PBtn.Pressed += (o, e) => wasCalledCount = ++wasCalledCount;
            _TBtn.Pressed += (o, e) => wasCalledCount = ++wasCalledCount;
            _SCBtn.Pressed += (o, e) => wasCalledCount = ++wasCalledCount;

            _PBtn.Press();  // 1
            _TBtn.Press();  // 2
            _SCBtn.Press(); // 3
            _Door.Open();   // 4


            _fakeDisplay.Received(1).ShowPower(Arg.Is<int>(50));                // 1 
            _fakeDisplay.Received(1).ShowTime(Arg.Is<int>(1), Arg.Is<int>(0));  // 2
            _fakeCC.Received(1).Stop();                                         // 3
            _fakeDisplay.Received(1).Clear();                                   // 4 
            _fakeLight.Received(1).TurnOn();                                    // 4
            Assert.That(wasCalledCount, Is.EqualTo(result));
        }


        // OnDoorClosed Tests
        // OnDoor Closed returns to States.READY
        [TestCase(2, TestName = "CloseDoor ShouldCall_fakeLight(2)")]
        public void TestDoorClose(int result)
        {
            _sutUI = new UserInterface(_PBtn, _TBtn, _SCBtn, _Door, _fakeDisplay, _fakeLight, _fakeCC);

            var wasCalledCount = 0;
            _Door.Opened += (o, e) => wasCalledCount = ++wasCalledCount;
            _Door.Closed += (o, e) => wasCalledCount = ++wasCalledCount;

            _Door.Open();
            _Door.Close();

            _fakeLight.Received(1).TurnOn();
            _fakeLight.Received(1).TurnOff();
            Assert.That(wasCalledCount, Is.EqualTo(result));
        }

        // Door Tets:
        // Further Cases:

    }
}
