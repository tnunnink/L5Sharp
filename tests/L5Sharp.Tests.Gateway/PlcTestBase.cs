using L5Sharp.Gateway;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Services;
using L5Sharp.Samples;

namespace L5Sharp.Tests.Gateway;

public abstract class PlcTestBase
{
    // Toggle this to switch all tests between Virtual and Physical PLC (ITagService)
    private static bool UseVirtual => false;

    protected static IPlcClient CreateClient(Action<PlcOptions>? config = null)
    {
        //Configure a common PLC target for testing.
        var options = new PlcOptions
        {
            IP = "10.11.19.204",
            Slot = 1
        };

        //Apply any custom configuration to the options as needed.
        config?.Invoke(options);

        //When virtual setting is enabled, return a new client with the virtual tag service for in memory data access.
        if (UseVirtual)
        {
            var service = VirtualTagService.Upload(Known.Simple, TimeSpan.FromMilliseconds(10));
            return new PlcClient(options, service);
        }

        //Otherwise return the default client with config options.
        return new PlcClient(options);
    }
}