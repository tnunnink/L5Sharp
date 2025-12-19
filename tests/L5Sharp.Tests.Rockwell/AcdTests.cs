using L5Sharp.Rockwell;
using L5Sharp.Samples;

namespace L5Sharp.Tests.Rockwell;

public class AcdTests
{
    [Test]
    public async Task ConvertAsync_ValidFile_ShouldNotBeNull()
    {
        var project = await ACD.LoadAsync(Known.TestAcd);

        Assert.That(project, Is.Not.Null);
    }
}