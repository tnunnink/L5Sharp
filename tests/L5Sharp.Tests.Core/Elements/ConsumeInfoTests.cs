using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class ConsumeInfoTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var consumeInfo = new ConsumeInfo();

        consumeInfo.Should().NotBeNull();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var consumeInfo = new ConsumeInfo
        {
            Producer = "TestProducer",
            RemoteTag = "RemoteTag",
            RemoteInstance = 1,
            RPI = 10.5,
            Unicast = true
        };

        consumeInfo.Producer.Should().Be("TestProducer");
        consumeInfo.RemoteTag.Should().Be("RemoteTag");
        consumeInfo.RemoteInstance.Should().Be(1);
        consumeInfo.RPI.Should().Be(10.5);
        consumeInfo.Unicast.Should().BeTrue();
    }

    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var consumeInfo = new ConsumeInfo
        {
            Producer = "TestProducer",
            RemoteTag = "RemoteTag",
            RemoteInstance = 1,
            RPI = 10.5,
            Unicast = true
        };

        return Verify(consumeInfo.Serialize().ToString());
    }
}
