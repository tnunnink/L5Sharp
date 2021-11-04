using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class LogixTypeTests
    {
        [Test]
        public void Get_String_ShouldNotBeNull()
        {
            var type = Logix.DataType.String;

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
    }
}