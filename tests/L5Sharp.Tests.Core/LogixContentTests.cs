namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixContentTests
{
    [Test]
    public void New_Test_ShouldBeExpected()
    {
        var content = LogixContent.Create("Test", "1756-L83E", 36.1);

        var isPartial = content.ContainsContext;
        var targetType = content.TargetType;
        var targetName = content.TargetName;
    }
}