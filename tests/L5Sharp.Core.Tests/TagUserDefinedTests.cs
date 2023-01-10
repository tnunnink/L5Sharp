using FluentAssertions;
using L5Sharp.Creators;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagUserDefinedTests
    {
        [Test]
        public void New_ValidTypeAndName_TagShouldNotBeNull()
        {
            var type = new UserDefined("Test", members: new[] { Member.Create("Member01", new DINT()) });
            var tag = Tag.Create("TestTag", type);

            tag.Should().NotBeNull();
        }
    }
}