using CliFx;
using CliFx.Infrastructure;

namespace L5Sharp.CLI;

public static class App
{
    public static async Task<int> Main()
    {
        return await new CliApplicationBuilder()
            .SetTitle("L5Sharp.Cli")
            .SetDescription("Console application providing CLI for Logix projects.")
            .SetExecutableName("logix")
            .UseConsole(new SystemConsole())
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
    }
}