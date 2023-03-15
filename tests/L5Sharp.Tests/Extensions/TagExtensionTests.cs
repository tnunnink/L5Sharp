using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Extensions;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Extensions;

[TestFixture]
public class TagExtensionTests
{
    [Test]
    public void Member_ValidExpression_ShouldNotBeNull()
    {
        /*var tag = new Tag
        {
            Name = "Test",
            Data = new MyNestedType()
        };

        var member = tag.Member<MyNestedType>(t => t.Messages[1].Path.LEN);

        member.Should().NotBeNull();
        member.TagName.Should().Be("Test.Messages[1].Path.LEN");
        member.Data.Should().BeOfType<DINT>();*/
    }
}