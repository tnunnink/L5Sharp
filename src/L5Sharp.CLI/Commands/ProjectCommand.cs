using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Internal;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// Represents the base implementation of a CLI command to be executed
/// against a specified project. This class provides a common structure
/// for commands that require project-specific operations.
/// </summary>
public abstract class ProjectCommand : ICommand
{
    /// <inheritdoc />
    public abstract ValueTask ExecuteAsync(IConsole console);

    /// <summary>
    /// Determines whether the file specified by the given path is supported as a Logix project file.
    /// </summary>
    /// <param name="filePath">The path of the file to check.</param>
    /// <returns>True if the file is a supported Logix project file, otherwise false.</returns>
    protected virtual bool IsSupported(string filePath) => filePath.IsLogixFile(FileType.L5X);

    /// <summary>
    /// Gets or sets the path to the project file, such as an .L5X or .ACD file, to be used for
    /// the command execution. This property specifies the location of the project file to load
    /// or operate on within the context of the CLI command.
    /// </summary>
    [CommandOption("project", 'p', Description = "Path to project file (L5X or ACD)")]
    public string? Project { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether detailed execution logs and output
    /// should be displayed in the console. When set to true, additional
    /// information about the command's execution process is shown, which can
    /// be helpful for debugging or understanding command behavior.
    /// </summary>
    [CommandOption("verbose", 'v', Description = "Enables verbose logging output to the console for debugging.")]
    public bool Verbose { get; set; }

    /// <summary>
    /// Resolves the path to the project file, validating its existence.
    /// </summary>
    /// <param name="console">The console interface used to interact with the user.</param>
    /// <returns>A task representing the asynchronous operation, containing the resolved project file path.</returns>
    /// <exception cref="CommandException">Thrown if the project path cannot be determined or validated.</exception>
    protected string ResolveProjectPath(IConsole console)
    {
        //Always defer to the explicitly provided project path.
        if (!string.IsNullOrWhiteSpace(Project))
            return ValidateLogixFile(Project);

        //Check if the console input is a path to a supported project for the command.
        if (console.IsInputRedirected)
        {
            var inputPath = console.Input.ReadLine()?.Trim();

            if (inputPath is not null && inputPath.IsLogixFile())
                return ValidateLogixFile(inputPath);
        }

        //Fall back to looking in the current working directory for a logix file.
        return ValidateLogixFile(DiscoverProjectPath());
    }

    /// <summary>
    /// Resolves the output path for a project file based on the specified project path
    /// and the optional output path provided.
    /// </summary>
    /// <param name="projectPath">The file path of the project to be processed.</param>
    /// <param name="outputPath">An optional output path where the processed project will be saved.
    /// If not specified or null, a default path based on the project type will be used.</param>
    /// <returns>The resolved output path as a string.</returns>
    protected string ResolveSavePath(string projectPath, string? outputPath)
    {
        if (string.IsNullOrEmpty(outputPath))
            return ChangeProjectType(projectPath);

        if (Directory.Exists(outputPath) || (!Path.HasExtension(outputPath) && !File.Exists(outputPath)))
        {
            var fileName = ChangeProjectType(Path.GetFileName(projectPath));
            var directory = Path.GetFullPath(outputPath);
            Directory.CreateDirectory(directory);
            return Path.Combine(directory, fileName);
        }

        return ValidateLogixFile(outputPath);

        static string ChangeProjectType(string path)
        {
            var extension = path.IsLogixFile(FileType.ACD) ? nameof(FileType.L5X) : nameof(FileType.ACD);
            return Path.ChangeExtension(path, extension);
        }
    }

    /// <summary>
    /// Discovers the project file path in the current working directory. If no valid project file is found,
    /// or if multiple project files are detected, an exception will be thrown.
    /// </summary>
    /// <returns>The file path of the discovered project.</returns>
    /// <exception cref="CommandException">Thrown when no project file is found or when multiple project files exist in the directory.</exception>
    private string DiscoverProjectPath()
    {
        var workingDirectory = Directory.GetCurrentDirectory();
        var logixFiles = Directory.GetFiles(workingDirectory).Where(IsSupported).ToArray();

        return logixFiles.Length switch
        {
            1 => logixFiles[0],
            0 => throw new CommandException(
                "No project file was specified or found. Specify --project to run command."),
            _ => throw new CommandException(
                "Could not determine project to execute command against. Specify --project to run command.")
        };
    }

    /// <summary>
    /// Validates the provided project path to ensure it corresponds to a supported Logix file type.
    /// </summary>
    /// <param name="projectPath">The path to the project file to validate.</param>
    /// <returns>The validated project path string if it meets the criteria.</returns>
    /// <exception cref="CommandException">
    /// Thrown when the file path does not correspond to a valid Logix file type or
    /// when the file type is not supported by the current command.
    /// </exception>
    private string ValidateLogixFile(string projectPath)
    {
        if (!projectPath.IsLogixFile())
            throw new CommandException(
                "The provided project path is not a valid logix file type. Supported file types are ACD or L5X.");

        if (!IsSupported(projectPath))
            throw new CommandException(
                "The provided file type is not supported by the current command. Run convert to reformat the file.");

        return Path.GetFullPath(projectPath);
    }
}