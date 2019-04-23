using NUnit.Framework;

namespace Tests
{
    public class It01Sut
    {
        // System under test
        // Iteration 01
        [SetUp]
        public void Setup()
        {
        }


        // Sut: Display
        // Dep: Output
        [TestCase(TestName ="DisplaySut")]
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