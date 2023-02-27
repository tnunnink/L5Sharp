using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void TagTesting()
        {
            var tag = new Tag();

            tag.Name.Should().BeEmpty();
            tag.Data.Should().Be(LogixType.Null);
        }

        [Test]
        public void New_Tag_ShouldNotBeNull()
        {
            var tag = new Tag
            {
                Name = "MyTagName",
                Data = new BOOL(),
                TagType = TagType.Alias,
                AliasFor = new TagName("SomeOtherTag")
            };
        }
    }
}