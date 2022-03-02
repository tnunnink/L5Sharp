using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Creators;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagUserDefinedTests
    {
        [Test]
        public void New_ValidTypeAndName_TagShouldNotBeNull()
        {
            var type = new UserDefined("Test", members: new[] { Member.Create("Member01", new Dint()) });
            var tag = Tag.Create("TestTag", type);

            tag.Should().NotBeNull();
        }
    }
}