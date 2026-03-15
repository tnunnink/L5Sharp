using System;
using System.IO;
using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Tests.Samples;

/// <summary>
/// Provides utility methods and properties for accessing and loading sample L5X files
/// used in tests within the L5Sharp.Tests.Samples namespace.
/// </summary>
public static class TestContent
{
    /// <summary>
    /// The base directory of the current application domain, used to locate test sample files.
    /// </summary>
    private static readonly string BaseDirectory = AppContext.BaseDirectory;

    /// <summary>
    /// Represents an L5X project resource loaded from the predefined local example file path.
    /// Provides access to query, manipulate, and manage Logix components and entities.
    /// </summary>
    public static L5X Example => Test;

    /// <summary>
    /// Gets the L5X instance representing the "Test.L5X" embedded project resource.
    /// Enables access to components, elements, and functionalities defined within the "Test.L5X" resource.
    /// </summary>
    public static L5X Test => Load(TestFiles.Projects.Test);

    /// <summary>
    /// Gets the L5X instance representing the "Empty.L5X" embedded project resource.
    /// Provides access to an empty L5X project structure with minimal or no configured components.
    /// </summary>
    public static L5X Empty => Load(TestFiles.Projects.Empty);

    /// <summary>
    /// Gets the L5X instance representing the "Simple.L5X" embedded project resource.
    /// Provides access to a simple L5X project with basic configuration and components.
    /// </summary>
    public static L5X Simple => Load(TestFiles.Projects.Simple);

    /// <summary>
    /// Loads an L5X instance from a specified relative file path.
    /// </summary>
    /// <param name="relativePath">The relative path to the L5X file to load.</param>
    /// <returns>An instance of the <see cref="L5X"/> class loaded from the specified file.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the file at the specified relative path is not found.</exception>
    public static L5X Load(string relativePath)
    {
        var path = Path.Combine(BaseDirectory, relativePath);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                $"Test file missing: {path} " +
                "Ensure 'L5Sharp.Tests.Samples' is referenced and files are set to 'Copy to Output'.");
        }

        Console.WriteLine($"Loading test file from: {path}");
        return L5X.Load(path);
    }
}