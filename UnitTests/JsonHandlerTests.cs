using System.Collections.ObjectModel;
using AutoPilot;
using AutoPilot.Actions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class JsonHandlerTests
    {
        private JsonHandler lJsonHandler;
        private string TestPath, TestPath2;
        private ObservableCollection<AutoPilot.Action> expectedActions;
        private ObservableCollection<AutoPilot.Action> actualActions;

        [SetUp]
        public void Setup()
        {
            lJsonHandler = new JsonHandler();
            TestPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData/WrittenTestFile.json");
            TestPath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData/TestFile.json");
            expectedActions = new ObservableCollection<AutoPilot.Action>();
            actualActions = new ObservableCollection<AutoPilot.Action>();
        }

        [Test]
        public void TestJsonHandler()
        {
            MouseClick lMouseClick = new MouseClick
            {
                X_Coordinate = 100,
                Y_Coordinate = 323,
                NumberOfClicks = 1,
                Comment = "TestAction1"
            };

            expectedActions.Add(lMouseClick);

            lJsonHandler.WriteData(expectedActions, TestPath);
            actualActions = lJsonHandler.ReadData(TestPath);

            AutoPilot.Actions.MouseClick actualMouseClick = (MouseClick)(actualActions.FirstOrDefault());

            NUnit.Framework.Assert.AreEqual(100, actualMouseClick.X_Coordinate, "Fehler: JsonHandler Fehler. Path: " + TestPath);
        }

        [Test]
        public void TestWriteData()
        {
            MouseClick lMouseClick = new MouseClick
            {
                X_Coordinate = 100,
                Y_Coordinate = 323,
                NumberOfClicks = 1,
                Comment = "TestAction1"
            };

            expectedActions.Add(lMouseClick);

            lJsonHandler.WriteData(expectedActions, TestPath2);

            // Ensure that the file exists after writing data
            Assert.IsTrue(File.Exists(TestPath2));
        }

        [Test]
        public void TestReadData()
        {
            actualActions = lJsonHandler.ReadData(TestPath2);
            Assert.IsNotNull(actualActions);
        }

        [Test]
        public void TestCreateActionsFromJsonFile()
        {
            List<AutoPilot.Action> actions = lJsonHandler.CreateActionsFromJsonFile(TestPath2);
            Assert.IsNotNull(actions);
        }

        [Test]
        public void TestToCollection()
        {
            List<Dictionary<string, object>> jsonObjects = new List<Dictionary<string, object>>();
            List<AutoPilot.Action> actions = lJsonHandler.ToCollection(jsonObjects);
            Assert.IsNotNull(actions);
        }

        [Test]
        public void TestCreateActionFromJsonObject()
        {
            Dictionary<string, object> jsonObject = new Dictionary<string, object>
            {
                { "ActionType", "MouseClick" },
                { "X_Coordinate", 100 },
                { "Y_Coordinate", 323 },
                { "NumberOfClicks", 1 },
                { "Comment", "TestAction1" }
            };

            AutoPilot.Action action = lJsonHandler.CreateActionFromJsonObject(jsonObject);
            Assert.IsNotNull(action);
        }

        [Test]
        public void TestCreateActionFromJsonObjectWithType()
        {
            Dictionary<string, object> jsonObject = new Dictionary<string, object>
            {
                { "ActionType", "MouseClick" },
                { "X_Coordinate", 100 },
                { "Y_Coordinate", 323 },
                { "NumberOfClicks", 1 },
                { "Comment", "TestAction1" }
            };

            MouseClick mouseClick = lJsonHandler.CreateActionFromJsonObject<MouseClick>(jsonObject);
            Assert.IsNotNull(mouseClick);
            Assert.AreEqual(100, mouseClick.X_Coordinate);
            Assert.AreEqual(323, mouseClick.Y_Coordinate);
            Assert.AreEqual(1, mouseClick.NumberOfClicks);
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(TestPath))
            {
                File.Delete(TestPath);
            }
        }
        
    }
}