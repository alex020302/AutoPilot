using AutoPilot.Actions;
using NUnit.Framework;
using WindowsInput.Native;

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

            Assert.AreEqual(mouseClick.Y_Coordinate, 200, "TestMouseClick() failed");
        }

        [Test]
        public void TestDelay()
        {
            Delay delay = new Delay()
            {
                Milliseconds = 1000,
                Comment = "Test Delay"
            };

            Assert.AreEqual(delay.Milliseconds, 1000, "TestDelay() failed");
        }

        [Test]
        public void TestTextEmulation()
        {
            TextEmulation textEmulation = new TextEmulation
            {
                Text = "Test",
                Comment = "Test TextEmulation"
            };

            Assert.AreEqual(textEmulation.Text, "Test", "TestTextEmulation() failed");
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

            Assert.AreEqual(dataInput.Column, 2, "TestDataInput() failed");
        }

        [Test]
        public void TestLink()
        {
            Link link = new Link
            {
                Url = "www.google.de",
                Comment = "Test Link"
            };

            Assert.AreEqual(link.Url, "www.google.de", "TestLink() failed");
        }

        [Test]
        public void TestSpecialKey()
        {
            SpecialKey specialKey = new SpecialKey()
            {
                Comment = "Test Special Key"
            };

            specialKey.AddCharToKeyCodes('C');
            specialKey.AddKeyToKeyCodes(KeyType.Windows);

            Assert.AreEqual(specialKey.KeyCodes[0], VirtualKeyCode.VK_C, "TestSpecialKey() failed");
            Assert.AreEqual(specialKey.KeyCodes[1], VirtualKeyCode.LWIN, "TestSpecialKey() failed");
        }

    }
}
