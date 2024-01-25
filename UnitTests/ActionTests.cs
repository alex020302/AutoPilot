using AutoPilot.Actions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ActionTests
    {
        [Test]
        public void TestMouseClick()
        {
            MouseClick mouseClick = new MouseClick
            {
                X_Coordinate = 100,
                Y_Coordinate = 200,
                NumberOfClicks = 2,
                Comment = "Test MouseClick"
            };

            Assert.AreEqual(mouseClick.Y_Coordinate, 200, "TestMouseClick() hasn't passed");
        }

        [Test]
        public void TestDelay()
        {
            Delay delay = new Delay()
            {
                Milliseconds = 1000,
                Comment = "Test Delay"
            };

            Assert.AreEqual(delay.Milliseconds, 1000, "TestDelay() hasn't passed");
        }

        [Test]
        public void TestTextEmulation()
        {
            TextEmulation textEmulation = new TextEmulation
            {
                Text = "Test",
                Comment = "Test TextEmulation"
            };

            Assert.AreEqual(textEmulation.Text, "Test", "TestTextEmulation() hasn't passed");
        }

        [Test]
        public void TestDataInput()
        {
            DataInput dataInput = new DataInput
            {
                Column = 2,
                ExcelPath = "test.xlsx",
                Row = 1,
                Comment = "Test DataInput"
            };

            Assert.AreEqual(dataInput.Column, 2, "TestDataInput() hasn't passed");
        }
    }
}
