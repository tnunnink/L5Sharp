using System;

namespace L5Sharp.Generators.Data;

internal static class Extensions
{
    /// <summary>
    /// Sanitizes the specified name by replacing invalid characters with underscores.
    /// </summary>
    /// <param name="name">The name to sanitize.</param>
    /// <returns>A sanitized string where invalid characters are replaced with underscores.</returns>
    internal static string SanitizeName(this string name)
    {
        return name.Replace(':', '_').Replace('.', '_').Replace(' ', '_');
    }

    /// <summary>
    /// Determines whether the specified file name has an extension of ".L5X".
    /// </summary>
    /// <param name="file">The file name to evaluate.</param>
    /// <returns><c>true</c> if the file has an ".L5X" extension; otherwise, <c>false</c>.</returns>
    internal static bool IsL5XPath(this string file)
    {
        return file.EndsWith(".L5X", StringComparison.OrdinalIgnoreCase);
    }
}