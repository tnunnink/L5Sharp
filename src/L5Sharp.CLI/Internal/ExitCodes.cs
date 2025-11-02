namespace L5Sharp.CLI.Internal;

public static class ExitCodes
{
    /// <summary>Success</summary>
    public const int Success = 0;

    /// <summary>General error</summary>
    public const int GeneralError = 1;

    /// <summary>Incorrect command usage</summary>
    public const int UsageError = 2;

    // L5Sharp specific codes (64-78 range follows sysexits.h)
    /// <summary>Input data error (invalid L5X, malformed JSON, etc.)</summary>
    public const int FormatError = 65;

    /// <summary>Input file/stream not available</summary>
    public const int NoInput = 66;

    /// <summary>Service/feature unavailable</summary>
    public const int Unavailable = 69;

    /// <summary>Internal error (unexpected exception)</summary>
    public const int InternalError = 70;

    /// <summary>System error (I/O, permissions, etc.)</summary>
    public const int SystemError = 71;

    /// <summary>Configuration error</summary>
    public const int ConfigError = 78;

    // L5Sharp domain-specific codes (80+)
    /// <summary>Project validation failed</summary>
    public const int ProjectValidationError = 80;

    /// <summary>Component not found</summary>
    public const int ComponentNotFound = 81;
    
    /// <summary>Component name collision error.</summary>
    public const int ComponentCollision = 82;

    /// <summary>Conversion failed</summary>
    public const int ConversionError = 83;
}