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
            var type = new Undefined();
            
            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Undefined_ShouldHaveExpectedDefaults()
        {
            var type = new Undefined();

            type.Name.Should().Be(nameof(Undefined));
            type.Description.Should().Be("Data type is not defined");
            type.Family.Should().Be(DataTypeFamily.None);
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Radix.Should().Be(Radix.Null);
            type.DataFormat.Should().BeNull();
        }
    }
}