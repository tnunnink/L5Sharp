using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class ProduceInfoTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var produceInfo = new ProduceInfo();

        produceInfo.Should().NotBeNull();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var produceInfo = new ProduceInfo
        {
            ProduceCount = 5,
            ProgrammaticallySendEventTrigger = true,
            UnicastPermitted = true,
            MinimumRPI = 1.0,
            MaximumRPI = 100.0,
            DefaultRPI = 10.0
        };

        produceInfo.ProduceCount.Should().Be(5);
        produceInfo.ProgrammaticallySendEventTrigger.Should().BeTrue();
        produceInfo.UnicastPermitted.Should().BeTrue();
        produceInfo.MinimumRPI.Should().Be(1.0);
        produceInfo.MaximumRPI.Should().Be(100.0);
        produceInfo.DefaultRPI.Should().Be(10.0);
    }

    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var produceInfo = new ProduceInfo
        {
            ProduceCount = 5,
            ProgrammaticallySendEventTrigger = true,
            UnicastPermitted = true,
            MinimumRPI = 1.0,
            MaximumRPI = 100.0,
            DefaultRPI = 10.0
        };

        return Verify(produceInfo.Serialize().ToString());
    }
}
