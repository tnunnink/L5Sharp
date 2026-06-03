using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class RedundancyInfoTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var redundancyInfo = new RedundancyInfo();

        redundancyInfo.Should().NotBeNull();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var redundancyInfo = new RedundancyInfo
        {
            Enabled = true,
            KeepTestEditsOnSwitchOver = true,
            IOMemoryPadPercentage = 10.5f,
            DataTablePadPercentage = 50.0f
        };

        redundancyInfo.Enabled.Should().BeTrue();
        redundancyInfo.KeepTestEditsOnSwitchOver.Should().BeTrue();
        redundancyInfo.IOMemoryPadPercentage.Should().Be(10.5f);
        redundancyInfo.DataTablePadPercentage.Should().Be(50.0f);
    }

    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var redundancyInfo = new RedundancyInfo
        {
            Enabled = true,
            KeepTestEditsOnSwitchOver = true,
            IOMemoryPadPercentage = 10.5f,
            DataTablePadPercentage = 50.0f
        };

        return Verify(redundancyInfo.Serialize().ToString());
    }
}
