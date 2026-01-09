using L5Sharp.Gateway;

namespace L5Sharp.Tests.Gateway.PlcClientTests;

[TestFixture]
public class PingTests
{
    private const string TestIp = "10.11.19.204";
    private const int TestSlot = 1;
    
    [Test]
    public async Task Ping_ValidReachableAddress_ShouldBeTrue()
    {
        using var client = new PlcClient(TestIp, TestSlot);

        var result = await client.Ping();

        Assert.That(result, Is.True);
    }
}