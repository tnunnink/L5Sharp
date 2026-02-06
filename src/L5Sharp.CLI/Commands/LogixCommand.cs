using System.Xml;
using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Common;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents the base implementation of a CLI command to be executed
/// against a specified project. This class provides a common structure
/// for commands that require project-specific operations.
/// </summary>
public abstract class LogixCommand : ICommand
{
    /// <inheritdoc />
    public abstract ValueTask ExecuteAsync(IConsole console);

    /// <summary>
    /// Gets or sets the path to the project file, such as an .L5X or .ACD file, to be used for
    /// the command execution. This property specifies the location of the project file to load
    /// or operate on within the context of the CLI command.
    /// </summary>
    [CommandOption("project", 'p', Description = "Path to project file (L5X or ACD)")]
    public string? Project { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether detailed execution logs and output
    /// should be displayed in the console. When set to true, additional
    /// information about the command's execution process is shown, which can
    /// be helpful for debugging or understanding command behavior.
    /// </summary>
    [CommandOption("verbose", 'v', Description = "Enables verbose logging output to the console for debugging.")]
    public bool Verbose { get; init; }

    /// <summary>
    /// Asynchronously reads and loads an L5X project from the specified project path,
    /// redirected console input, or a discovered file in the current working directory.
    /// This method provides a unified entry point for loading L5X project data from
    /// various input sources.
    /// </summary>
    /// <param name="console">
    /// The console instance used to interact with input and output streams and to
    /// register cancellation handlers for the operation.
    /// </param>
    /// <param name="token">
    /// A cancellation token to observe while waiting for the task to complete.
    /// If not provided or set to <see cref="CancellationToken.None"/>, a cancellation
    /// handler will be automatically registered from the console.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, resulting in a loaded instance
    /// of <see cref="L5X"/> containing the project data.
    /// </returns>
    /// <exception cref="CommandException">
    /// Thrown when the L5X format is invalid, when the input cannot be read, or when
    /// no suitable L5X file can be located from any input source.
    /// </exception>
    protected Task<L5X> ReadProject(IConsole console, CancellationToken token = default)
    {
        token = token == CancellationToken.None ? console.RegisterCancellationHandler() : token;

        try
        {
            return ReadFromInputOrPath(console, Project, token);
        }
        catch (XmlException e)
        {
            throw new CommandException($"Invalid L5X format: {e.Message}", ExitCodes.FormatError);
        }
        catch (Exception e)
        {
            throw new CommandException($"Failed to read L5X input: {e.Message}", ExitCodes.InternalError);
        }
    }

    /// <summary>
    /// Writes the specified <see cref="L5X"/> project to the console output in its serialized format.
    /// </summary>
    /// <param name="console">The console instance to which the serialized L5X project will be written.</param>
    /// <param name="project">The <see cref="L5X"/> project to serialize and write to the console.</param>
    protected static void WriteProject(IConsole console, L5X project)
    {
        console.Output.Write(project.ToString());
    }

    /// <summary>
    /// Reads and processes an L5X file from the provided path, redirected console input,
    /// or the current working directory. Determines the input source based on availability
    /// and validates the content before returning the processed data.
    /// </summary>
    /// <param name="console">
    /// The console instance used to interact with input and output streams.
    /// </param>
    /// <param name="path">
    /// The path to the L5X project file. If null or empty, other input sources will be considered.
    /// </param>
    /// <param name="token">
    /// A cancellation token to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, resulting in a loaded instance of <see cref="L5X"/>.
    /// </returns>
    /// <exception cref="CliFx.Exceptions.CommandException">
    /// Thrown when the input is invalid or a suitable L5X file cannot be located.
    /// </exception>
    private static async Task<L5X> ReadFromInputOrPath(IConsole console, string? path, CancellationToken token)
    {
        // Always defer to the explicitly provided project path.
        if (!string.IsNullOrWhiteSpace(path))
        {
            var filePath = ValidateLogixFile(path, FileType.L5X);
            return await L5X.LoadAsync(filePath, token);
        }

        // Check the console input for piped in content and see if we can parse it as XML.
        if (console.IsInputRedirected)
        {
            var xml = await console.Input.ReadToEndAsync(token);

            if (string.IsNullOrWhiteSpace(xml))
                throw new CommandException("Empty input provided. Expected L5X content.", ExitCodes.NoInput);

            return L5X.Parse(xml);
        }

        // See if there is a single L5X file in the current working directory as the fallback.
        if (TryDiscoverLogixFile(FileType.L5X, out var discovered))
            return await L5X.LoadAsync(discovered, token);

        throw new CommandException(
            "No L5X file was specified or found. Specify --project to run command.",
            ExitCodes.FileNotFound
        );
    }

    /// <summary>
    /// Resolves the project file path based on the specified file type. If the project file path is
    /// provided, it is validated. Otherwise, the method attempts to discover a suitable project file
    /// in the working directory.
    /// </summary>
    /// <param name="type">The type of the project file to resolve (e.g., ACD or L5X).</param>
    /// <returns>The validated project file path.</returns>
    /// <exception cref="CliFx.Exceptions.CommandException">
    /// Thrown when no project file is specified or found in the working directory.
    /// </exception>
    protected string ResolveProject(FileType type)
    {
        // If the project option is configured, then we are good to validate and return it.
        if (!string.IsNullOrWhiteSpace(Project))
        {
            return ValidateLogixFile(Project, type);
        }

        // Fallback to trying to find a project file of the specified type in the local working directory.
        if (TryDiscoverLogixFile(type, out var discovered))
        {
            return discovered;
        }

        throw new CommandException(
            $"No {type} file was specified or found. Specify --project to run command.",
            ExitCodes.FileNotFound
        );
    }

    /// <summary>
    /// Attempts to discover a Logix file of the specified type in the current working directory.
    /// </summary>
    /// <param name="type">The type of Logix file to search for, such as ACD or L5X.</param>
    /// <param name="path">When the method returns, contains the discovered file path if exactly one matching file is found; otherwise, null.</param>
    /// <returns>True if exactly one matching file is found; otherwise, false.</returns>
    private static bool TryDiscoverLogixFile(FileType type, out string path)
    {
        var workingDirectory = Directory.GetCurrentDirectory();
        var logixFiles = Directory.GetFiles(workingDirectory).Where(f => f.IsLogixFile(type)).ToArray();

        if (logixFiles.Length == 1)
        {
            path = logixFiles[0];
            return true;
        }

        path = null!;
        return false;
    }

    /// <summary>
    /// Validates the specified file path to ensure it exists and corresponds to the specified logix file type.
    /// </summary>
    /// <param name="path">The path of the file to validate.</param>
    /// <param name="type">The type of logix file to validate against (e.g., L5X or ACD).</param>
    /// <returns>The full path of the validated file.</returns>
    /// <exception cref="CommandException">
    /// Thrown if the specified file does not exist or if the file does not match the expected logix file type.
    /// </exception>
    private static string ValidateLogixFile(string path, FileType type)
    {
        if (!File.Exists(path))
            throw new CommandException(
                $"The specified file was not found: {path}",
                ExitCodes.FileNotFound
            );

        if (!path.IsLogixFile(type))
            throw new CommandException(
                $"The specified file is not a valid logix {type} file: {path}",
                ExitCodes.GeneralError
            );

        return Path.GetFullPath(path);
    }
}