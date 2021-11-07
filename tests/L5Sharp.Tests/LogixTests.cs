using System.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class LogixTests
    {
        [Test]
        public void Names_WhenCalled_ShouldNotBeEmpty()
        {
            var dataTypes = Logix.DataType.Names;

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Contains_TypeThatExistsAsPredefined_ShouldBeTrue()
        {
            Logix.DataType.Contains("BOOL").Should().BeTrue();
        }

        [Test]
        public void Contains_TypeThatDoesNotExistAsPredefined_ShouldBeFalse()
        {
            Logix.DataType.Contains("TEMP").Should().BeFalse();
        }
        
        [Test]
        public void ParseType_RegisteredType_ShouldNotBeNull()
        {
            var type = Logix.DataType.Create("Bool");
            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Should().BeOfType<Bool>();
        }

        [Test]
        public void ParseType_StaticField_ShouldNotBeNull()
        {
            var type = Logix.DataType.Create("bit");
            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Should().BeOfType<Bool>();
        }

        [Test]
        public void ParseType_AssemblyValidType_ShouldNotBeExpected()
        {
            var type = Logix.DataType.Create("MyPredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("MyPredefined");
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void ParseType_AssemblyInvalidType_ShouldNotBeUndefined()
        {
            var type = Logix.DataType.Create("MyNullNamePredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("Undefined");
            type.Should().BeOfType<Undefined>();
        }
        
        [Test]
        public void ParseType_NonExistingType_ShouldNotBeUndefined()
        {
            var type = Logix.DataType.Create("Invalid");
            type.Name.Should().Be("Undefined");
            type.Should().BeOfType<Undefined>();
        }
    }
}