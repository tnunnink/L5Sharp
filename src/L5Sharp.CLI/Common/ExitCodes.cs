namespace L5Sharp.CLI.Common;

/// <summary>
/// Defines standard exit codes returned by the L5Sharp CLI application to indicate
/// the result of command execution.
/// </summary>
public static class ExitCodes
{
    /// <summary>
    /// Indicates the command executed successfully without errors.
    /// </summary>
    public const int Success = 0;

    /// <summary>
    /// Indicates a general unspecified error occurred during command execution.
    /// </summary>
    public const int GeneralError = -1;

    /// <summary>
    /// Indicates incorrect command usage, such as invalid arguments or missing required parameters.
    /// </summary>
    public const int UsageError = -2;

    /// <summary>
    /// Indicates the L5X file format is invalid or malformed.
    /// </summary>
    public const int FormatError = -3;

    /// <summary>
    /// Indicates no input was provided when input was expected.
    /// </summary>
    public const int NoInput = -4;

    /// <summary>
    /// Indicates the specified file or project path was not found.
    /// </summary>
    public const int FileNotFound = -5;

    /// <summary>
    /// Indicates an internal application error occurred during processing.
    /// </summary>
    public const int InternalError = -6;

    /// <summary>
    /// Indicates a system-level error occurred, such as I/O or permissions issues.
    /// </summary>
    public const int SystemError = -7;

    /// <summary>
    /// Indicates a configuration error was encountered.
    /// </summary>
    public const int ConfigError = -8;

    /// <summary>
    /// Indicates the specified Logix component was not found in the project.
    /// </summary>
    public const int NotFound = -20;

    /// <summary>
    /// Indicates that the provided type is invalid or not recognized by the application.
    /// This exit code is typically used when a command fails due to an unsupported or
    /// malformed type argument.
    /// </summary>
    public const int InvalidType = -21;

    /// <summary>
    /// Indicates a Logix component with the same name already exists in the project.
    /// </summary>
    public const int ComponentCollision = -22;

    /// <summary>
    /// Indicates an error occurred during conversion between file formats (e.g., ACD to L5X).
    /// </summary>
    public const int ConversionError = -23;
}