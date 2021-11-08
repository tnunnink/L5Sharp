using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class StringTests
    {
        [Test]
        public void New_String_ShouldNotBeNull()
        {
            var type = new String();

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.String);
        }

        [Test]
        public void Predefined_String_ShouldHaveMembers()
        {
            var type = new String();    

            type.LEN.Should().NotBeNull();
            type.DATA.Should().NotBeNull();
        }
    }
}