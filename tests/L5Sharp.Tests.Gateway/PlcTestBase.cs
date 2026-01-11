using L5Sharp.Gateway;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Services;
using L5Sharp.Samples;

namespace L5Sharp.Tests.Gateway;

public abstract class PlcTestBase
{
    private const string Ip = "10.11.19.204";
    private const ushort Slot = 1;

    // Toggle this to switch all tests between Virtual and Physical PLC (ITagService)
    private static bool UseVirtual => true;

    protected static IPlcClient CreateClient(Action<PlcOptions>? config = null)
    {
        var builder = Plc.ConnectTo(Ip).Slot(Slot);

        if (config is not null)
            builder.WithOptions(config);

        if (UseVirtual)
            // Seed the Virtual Service with a sample L5X file.
            builder.UseTagService(() => VirtualTagService.Upload(Known.Simple, TimeSpan.Zero));

        return builder.Build();
    }
}