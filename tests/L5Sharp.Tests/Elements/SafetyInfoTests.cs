using FluentAssertions;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class SafetyInfoTests
{
    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var info = new SafetyInfo();

        info.SafetyLocked.Should().BeFalse();
        info.SafetySignature.Should().BeNull();
        info.SafetyLockPassword.Should().BeNull();
        info.SafetyUnlockPassword.Should().BeNull();
        info.SignatureRunModeProtect.Should().BeFalse();
        info.ConfigureSafetyIOAlways.Should().BeFalse();
        info.SafetyTagMap.Should().BeEmpty();
    }

    [Test]
    public Task New_Overriden_ShouldBeVerified()
    {
        var info = new SafetyInfo
        {
            SafetyLocked = true,
            SafetySignature = "!@#$ASDF123",
            SignatureRunModeProtect = true,
            ConfigureSafetyIOAlways = true,
            SafetyLockPassword = "Test",
            SafetyUnlockPassword = "Test",
            SafetyTagMap = new SafetyTagMap()
            {
                "Test_Tag_01",
                "Test_Tag_02",
                "Test_Tag_03",
            }
        };
        
        return VerifyXml(info.Serialize().ToString());
    }
}