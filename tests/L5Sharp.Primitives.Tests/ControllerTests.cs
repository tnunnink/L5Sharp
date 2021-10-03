using System.Linq;
using FluentAssertions;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
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
        public void New_InvalidName_ShouldNotBeNull()
        {
            var controller = new Controller("Test_Controller");

            controller.Should().NotBeNull();
        }
        
        [Test]
        public void Get_DataTypeExistingType_ShouldNotBeNull()
        {
            var controller = new Controller("Test_Controller");

            var type = controller.Get<DataType>("Injector");

            type.Should().NotBeNull();
        }

        /*[Test]
        public void Add_DataType_DataTypesShouldContainExpectedDataType()
        {
            var dataType = new DataType("TestType");
            var controller = new Controller("Test");

            controller.Add(dataType);

            controller.DataTypes.Should().Contain(dataType);
        }
        
        [Test]
        public void Add_DataTypeValidType_ShouldHaveExpectedCount()
        {
            var controller = new Controller("Name");
            
            controller.Add(new DataType("TypeName", "This is a test"));

            controller.DataTypes.Where(x => x.Name == "TypeName").Should().HaveCount(1);
        }

        [Test]
        public void Add_DataTypeValidDataType_ShouldUpdateCollection()
        {
            var controller = new Controller("Name");
            var type = new DataType("TestType");
            type.AddMember("Member01", DataType.Dint);
            
            controller.Add(type);

            controller.DataTypes.Where(x => x.Name == "TestType").Should().HaveCount(1);
        }

        [Test]
        public void Create_DatatypeWithMembers_ShouldHaveExpectedDataTypes()
        {
            var controller = new Controller("ControllerName");
            
            controller.Create().DataType("DT1", b =>
                b.HasDescription("Test Type 1")
                    .WithMember("TestMember", DataType.Bool));
            
            controller.Create().DataType("DT2", b =>
                b.HasDescription("Test Type 2")
                    .WithMember("TestMember", DataType.Dint));

            controller.DataTypes.Should().Contain(d => d.Name == "DT1");
            controller.DataTypes.Should().Contain(d => d.Name == "DT2");
        }*/
    }
}