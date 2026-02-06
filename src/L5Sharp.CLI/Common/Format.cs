namespace L5Sharp.CLI.Common;

/// <summary>
/// Represents the supported output formats for data representation.
/// </summary>
public enum Format
{
    /// <summary>
    /// Indicates the data presentation in a tabular structure for better readability and organization.
    /// </summary>
    Table,

    /// <summary>
    /// Represents the XML format for structured output, providing data in a hierarchical, tag-based representation.
    /// </summary>
    Xml,

    /// <summary>
    /// Specifies the data representation in JSON format, suitable for structured and hierarchical data exchange.
    /// </summary>
    Json,

    /// <summary>
    /// Represents the data format using comma-separated values, suitable for spreadsheet applications or flat-file databases.
    /// </summary>
    Csv
}