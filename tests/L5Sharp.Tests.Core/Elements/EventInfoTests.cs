using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class EventInfoTests
{
    [Test]
    public void New_Default_ShouldHaveExpectedDefaults()
    {
        var eventInfo = new EventInfo();

        eventInfo.EventTrigger.Should().Be(TaskEventTrigger.ConsumedTag);
        eventInfo.EventTag.Should().BeNull();
        eventInfo.EnableTimeout.Should().BeFalse();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var eventInfo = new EventInfo
        {
            EventTrigger = TaskEventTrigger.ModuleInputDataStateChange,
            EventTag = "TestTag",
            EnableTimeout = true
        };

        eventInfo.EventTrigger.Should().Be(TaskEventTrigger.ModuleInputDataStateChange);
        eventInfo.EventTag.Should().Be("TestTag");
        eventInfo.EnableTimeout.Should().BeTrue();
    }

    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var eventInfo = new EventInfo
        {
            EventTrigger = TaskEventTrigger.ModuleInputDataStateChange,
            EventTag = "TestTag",
            EnableTimeout = true
        };

        return Verify(eventInfo.Serialize().ToString());
    }
}
