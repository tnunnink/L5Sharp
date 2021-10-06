using System;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
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
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new Controller("This is Invalid !@#$")).Should().Throw<InvalidNameException>();
        }
        
        //todo add property tests
        
        [Test]
        public void AddDataType_ValidDataType_DataTypesShouldHaveComponent()
        {
            var controller = new Controller("Test_Controller");
            var datatype = new DataType("TestType");

            controller.AddDataType(datatype);

            controller.DataTypes.Should().Contain(datatype);
        }
        
        [Test]
        public void AddDataType_Null_ShouldThrowArgumentNullException()
        {
            var controller = new Controller("Test_Controller");

            FluentActions.Invoking(() => controller.AddDataType(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void AddDataType_AlreadyExists_ShouldThrowComponentNameCollisionException()
        {
            var controller = new Controller("Test_Controller");
            var datatype = new DataType("TestType");
            controller.AddDataType(datatype);

            FluentActions.Invoking(() => controller.AddDataType(datatype)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void RemoveDataType_ValidDataType_DataTypesShouldHaveComponent()
        {
            var controller = new Controller("Test_Controller");
            var datatype = new DataType("TestType");
            controller.AddDataType(datatype);
            
            controller.RemoveDataType(datatype);
            
            controller.DataTypes.Should().NotContain(datatype);
        }
    }
}