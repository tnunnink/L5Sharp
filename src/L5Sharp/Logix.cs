namespace L5Sharp.Core;

/// <summary>
/// A static factory class for the library. I wanted a nicer name that L5X for loading files
/// </summary>
public static class Logix
{
    /// <summary>
    /// Loads an L5X file and returns its content.
    /// </summary>
    /// <param name="fileName">The path to the L5X file.</param>
    /// <returns>The content of the L5X file as an <see cref="L5X"/> object.</returns>
    public static L5X Load(string fileName) => L5X.Load(fileName);
}