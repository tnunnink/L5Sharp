using CliFx;
using CliFx.Infrastructure;
using L5Sharp.CLI.Common;

namespace L5Sharp.CLI;

public static class App
{
    public static async Task<int> Main()
    {
        return await new CliApplicationBuilder()
            .SetTitle("Logix.Cli")
            .SetDescription("Console application providing CLI for Logix projects.")
            .SetExecutableName("logix")
            .UseConsole(new SystemConsole())
            .AddCommandsFromThisAssembly()
            .AddLogixServices()
            .Build()
            .RunAsync();
    }
}