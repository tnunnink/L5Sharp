namespace L5Sharp.Tests.Components;

[TestFixture]
public class ControllerTests
{
    [Test]
    public void SafetyInfo_GetValue_ShouldBeExpected()
    {
        var controller = new Controller
        {
            SafetyInfo = new SafetyInfo
            {
                SafetyLocked = true,
                SafetySignature = "...."
            }
        };
        
    }
}