using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Types;
using L5Sharp.Types.Atomic;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagUserDefinedTests
    {
        [Test]
        public void New_ValidTypeAndName_TagShouldNotBeNull()
        {
            var type = UserDefined.Create("Test", members: new[] { Member.Create("Member01", new Dint()) });
            var tag = Tag.Create("TestTag", type);

            tag.Should().NotBeNull();
        }
    }
}