using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class UndefinedTests
    {
        [Test]
        public void New_Undefined_ShouldNotBeNull()
        {
            var type = new Undefined("Test");
            
            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Undefined_ShouldHaveExpectedDefaults()
        {
            var type = new Undefined("Test");

            type.Name.Should().Be(nameof(Undefined));
            type.Description.Should().Be("Undefined DataType");
            type.Family.Should().Be(DataTypeFamily.None);
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Radix.Should().Be(Radix.Null);
            type.Format.Should().BeNull();
        }
    }
}