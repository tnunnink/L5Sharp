using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagUserDefinedTests
    {
        [Test]
        public void New_ValidTypeAndName_TagShouldNotBeNull()
        {
            var type = new DataType("Test", new DataTypeMember("Member01", Logix.DataType.Dint));
            var tag = new Tag("TestTag", type);

            tag.Should().NotBeNull();
        }
    }
}