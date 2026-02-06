using CliFx;
using CliFx.Infrastructure;
using L5Sharp.CLI.Common;

namespace L5Sharp.Tests.CLI;

/// <summary>
/// A testing context for CLI command integration tests, providing an in-memory console
/// and helper methods for configuring a CLI application instance.
/// </summary>
public sealed class CliTestContext : IDisposable
{
    /// <summary>
    /// Represents the console interface used for interacting with the CLI, providing input and output capabilities.
    /// </summary>
    public FakeInMemoryConsole Console { get; } = new();

    /// <summary>
    /// Creates a CLI application instance configured with the specified command type.
    /// </summary>
    /// <typeparam name="TCommand">The command type to be added to the application.</typeparam>
    /// <returns>A configured <see cref="CliApplication"/> instance ready to execute commands.</returns>
    public CliApplication CreateApp<TCommand>() where TCommand : ICommand
    {
        return new CliApplicationBuilder()
            .SetTitle("Logix.Cli")
            .SetDescription("Console application providing CLI for Logix projects.")
            .SetExecutableName("logix")
            .UseConsole(Console)
            .AddCommand<TCommand>()
            .AddLogixServices()
            .Build();
    }

    public void Dispose()
    {
        Console.Dispose();
    }
}