using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class SecurityTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var security = new Security();

        security.Should().NotBeNull();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var security = new Security
        {
            Code = 1234567890,
            SecurityAuthorityID = "TestID",
            SecurityAuthorityURI = "TestURI",
            PermissionSet = "TestSet",
            ChangesToDetect = "TestChanges",
            TrustedSlots = "TestSlots"
        };

        security.Code.Should().Be(1234567890);
        security.SecurityAuthorityID.Should().Be("TestID");
        security.SecurityAuthorityURI.Should().Be("TestURI");
        security.PermissionSet.Should().Be("TestSet");
        security.ChangesToDetect.Should().Be("TestChanges");
        security.TrustedSlots.Should().Be("TestSlots");
    }

    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var security = new Security
        {
            Code = 1234567890,
            SecurityAuthorityID = "TestID",
            SecurityAuthorityURI = "TestURI",
            PermissionSet = "TestSet",
            ChangesToDetect = "TestChanges",
            TrustedSlots = "TestSlots"
        };

        return Verify(security.Serialize().ToString());
    }
}
