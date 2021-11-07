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
        public void AddDataType_Configuration_DataTypesShouldHaveExpected()
        {
            var controller = new Controller("Test");

            controller.DataTypes.Add("Test",
                t => t.HasDescription("This is a test data type")
                    .HasMember("TestMember", c => c
                        .OfType(new Alarm())
                        .WithDimension(new Dimensions(4))
                        .HasDescription("This is a test"))
                    .HasMember("AnotherMember", c => c
                        .OfType(new Real())
                        .WithRadix(Radix.Exponential)));
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

        [Test]
        public void UpdateDataTypeAndSeeHowThatWorks()
        {
            var controller = new Controller("Test");
            var datatype = new DataType("TestType");
            datatype.Members.Add(new DataTypeMember<IDataType>("Test", new Bool()));
            controller.DataTypes.Add(datatype);

            var type = controller.DataTypes.First();

            var member = type.Members.Get("Test");
            //member.(new Dint());

            member.DataType.Should().Be(new Dint());
            controller.DataTypes.First().Members.First().DataType.Should().Be(new Dint());
        }
    }
}