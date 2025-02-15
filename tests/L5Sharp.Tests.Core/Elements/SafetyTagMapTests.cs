using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class SafetyTagMapTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var map = new SafetyTagMap();

        map.Should().NotBeNull();
    }

    [Test]
    public Task New_WithTags_ShouldBeVerified()
    {
        var map = new SafetyTagMap();
        
        map.Add("TestTag1");
        map.Add("TestTag2");
        map.Add("TestTag3");

        return VerifyXml(map.Serialize().ToString());
    }
}