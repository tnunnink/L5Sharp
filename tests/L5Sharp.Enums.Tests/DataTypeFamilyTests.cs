using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class DataTypeFamilyTests
    {
        [Test]
        public void None_WhenCalled_ShouldNotBeNull()
        {
            DataTypeFamily.None.Should().NotBeNull();
            DataTypeFamily.None.Name.Should().Be("None");
            DataTypeFamily.None.Value.Should().Be("NoFamily");
        }
        
        [Test]
        public void String_WhenCalled_ShouldNotBeNull()
        {
            DataTypeFamily.String.Should().NotBeNull();
            DataTypeFamily.String.Name.Should().Be("String");
            DataTypeFamily.String.Value.Should().Be("StringFamily");
        }
    }
}