using System.Collections.Generic;
using FluentAssertions;
using L5Sharp;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5SharpTests
{
    [TestFixture]
    public class LogixTests
    {
        [Test]
        public void Names_WhenCalled_ShouldNotBeEmpty()
        {
            var dataTypes = Logix.DataTypes;

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Contains_TypeThatExistsAsPredefined_ShouldBeTrue()
        {
            Logix.ContainsType("BOOL").Should().BeTrue();
        }

        [Test]
        public void Contains_TypeThatDoesNotExistAsPredefined_ShouldBeFalse()
        {
            Logix.ContainsType("TEMP").Should().BeFalse();
        }

        [Test]
        public void CreateType_RegisteredType_ShouldNotBeNull()
        {
            var type = Logix.InstantiateType("Bool");
            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Should().BeOfType<Bool>();
        }

        [Test]
        public void CreateType_StaticField_ShouldNotBeNull()
        {
            var type = Logix.InstantiateType("bit");
            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Should().BeOfType<Bool>();
        }

        [Test]
        public void CreateType_AssemblyValidType_ShouldNotBeExpected()
        {
            var type = Logix.InstantiateType("MyPredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("MyPredefined");
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void CreateType_AssemblyInvalidType_ShouldNotBeUndefined()
        {
            var type = Logix.InstantiateType("MyNullNamePredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("Undefined");
            type.Should().BeOfType<Undefined>();
        }

        [Test]
        public void CreateType_NonExistingType_ShouldNotBeUndefined()
        {
            var type = Logix.InstantiateType("Invalid");
            type.Name.Should().Be("Undefined");
            type.Should().BeOfType<Undefined>();
        }

        [Test]
        public void CreateType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.InstantiateType("string");

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.String);
            type.Radix.Should().Be(Radix.Null);
        }

        [Test]
        public void RegisterType_ValidArgument_ShouldContainType()
        {
            var type = new DataType("TestType",
                "This is a test type that will be created",
                new List<IMember<IDataType>>
            {
                Member.Create<IDataType>("Member01", new Dint(25)),
                Member.Create<IDataType>("Member02", new Timer(new Dint(1000)))
            });

            Logix.Register(type.Name, type.Instantiate);

            Logix.ContainsType(type.Name).Should().BeTrue();

            var instance = Logix.InstantiateType(type.Name);

            instance.Should().NotBeNull();
            instance.Name.Should().Be("TestType");
            instance.Description.Should().Be("This is a test type that will be created");
            instance.GetMember("Member01").Should().NotBeNull();
            instance.GetMember("Member01").DataType.As<Dint>().Value.Should().Be(0);
            instance.GetMember("Member02").DataType.As<Timer>().PRE.DataType.Value.Should().Be(0);
            instance.Should().NotBeSameAs(type);
        }
    }
}