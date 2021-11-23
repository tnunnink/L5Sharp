using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
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
            FluentActions.Invoking(() => new Controller("This is Invalid !@#$")).Should()
                .Throw<ComponentNameInvalidException>();
        }

        //todo add property tests

        [Test]
        public void AddDataType_ValidDataType_DataTypesShouldHaveComponent()
        {
            var controller = new Controller("Test_Controller");
            var datatype = new DataType("TestType");

            controller.DataTypes.Add(datatype);

            controller.DataTypes.Should().Contain(datatype);
        }

        [Test]
        public void AddDataType_Null_ShouldThrowArgumentNullException()
        {
            var controller = new Controller("Test_Controller");

            FluentActions.Invoking(() => controller.DataTypes.Add((IUserDefined)null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddDataType_AlreadyExists_ShouldThrowComponentNameCollisionException()
        {
            var controller = new Controller("Test_Controller");
            var datatype = new DataType("TestType");
            controller.DataTypes.Add(datatype);

            FluentActions.Invoking(() => controller.DataTypes.Add(datatype)).Should()
                .Throw<ComponentNameCollisionException>();
        }
        

        [Test]
        public void RemoveDataType_ValidDataType_DataTypesShouldHaveComponent()
        {
            var controller = new Controller("Test_Controller");
            var datatype = new DataType("TestType");
            controller.DataTypes.Add(datatype);

            controller.DataTypes.Remove(datatype.Name);

            controller.DataTypes.Should().NotContain(datatype);
        }
    }
}