using System.Linq;
using FluentAssertions;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var controller = new Controller("Test_Controller");

            controller.Should().NotBeNull();
        }
        
        [Test]
        public void CreateDataType_ValidType_ShouldHaveExpectedCount()
        {
            var controller = new Controller("Name");
            
            controller.AddDataType(new DataType("Name", "This is a test"));

            controller.DataTypes.Where(x => x.Name == "Name").Should().HaveCount(1);
        }

        [Test]
        public void METHOD()
        {
            var controller = new Controller("Name");
            
            controller.Create().DataType("Name", b => b.HasDescription("This is a test"));

            controller.DataTypes.Where(x => x.Name == "Name").Should().HaveCount(1);
        }
    }
}