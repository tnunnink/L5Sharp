namespace L5Sharp.CLI.Common;

/// <summary>
/// Specifies the types of Logix project files used in Rockwell Automation Studio 5000 Logix Designer.
/// </summary>
public enum FileType
{
    /// <summary>
    /// Represents an ACD  project type file. Rockwell Automation Studio 5000 Logix Designer project archives.
    /// </summary>
    ACD,

    /// <summary>
    /// Represents an L5X project type file. Rockwell Automation Studio 5000 Logix Designer XML-based project format.
    /// </summary>
    L5X
}