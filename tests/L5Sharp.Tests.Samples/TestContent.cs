using System.IO;
using System.Reflection;
using L5Sharp.Core;

namespace L5Sharp.Tests.Samples;

/// <summary>
/// Provides utility methods to manage and load embedded L5X project content.
/// </summary>
public static class TestContent
{
    /// <summary>
    /// Represents the namespace associated with the embedded L5X file resources within the application.
    /// </summary>
    private const string NameSpace = "L5Sharp.Tests.Samples";

    /// <summary>
    /// Represents the file path of a local example L5X file used in the application.
    /// </summary>
    private const string LocalExample = @"C:\Users\tnunn\Documents\L5X\Example.L5X";

    /// <summary>
    /// Represents an L5X project resource loaded from the predefined local example file path.
    /// Provides access to query, manipulate, and manage Logix components and entities.
    /// </summary>
    public static L5X Example => L5X.Load(LocalExample);

    /// <summary>
    /// Gets the L5X instance representing the "Test.L5X" embedded project resource.
    /// Enables access to components, elements, and functionalities defined within the "Test.L5X" resource.
    /// </summary>
    public static L5X Test => OpenResource("Projects.Test.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "Empty.L5X" embedded project resource.
    /// Provides access to an empty L5X project structure with minimal or no configured components.
    /// </summary>
    public static L5X Empty => OpenResource("Projects.Empty.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "Simple.L5X" embedded project resource.
    /// Provides access to a simple L5X project with basic configuration and components.
    /// </summary>
    public static L5X Simple => OpenResource("Projects.Simple.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "LotOfTags.L5X" embedded project resource.
    /// Provides access to an L5X project containing a large number of tag definitions for testing purposes.
    /// </summary>
    public static L5X LotOfTags => OpenResource("Projects.LotOfTags.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "aoiSigned_AOI.L5X" embedded Add-On Instruction export resource.
    /// Provides access to a signed Add-On Instruction definition exported from a Logix project.
    /// </summary>
    public static L5X AoiSignedExport => OpenResource("Instructions.aoiSigned_AOI.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "TestCard.L5X" embedded module export resource.
    /// Provides access to a module configuration exported from a Logix project.
    /// </summary>
    public static L5X ModuleExport => OpenResource("Modules.TestCard.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "RackIO.L5X" embedded rack I/O module export resource.
    /// Provides access to a rack-based I/O module configuration exported from a Logix project.
    /// </summary>
    public static L5X ModuleRackIoExport => OpenResource("Modules.RackIO.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "ComplexType.L5X" embedded data type export resource.
    /// Provides access to a user-defined data type definition exported from a Logix project.
    /// </summary>
    public static L5X DataTypeExport => OpenResource("DataTypes.ComplexType.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "TestProgram.L5X" embedded program export resource.
    /// Provides access to a program definition exported from a Logix project.
    /// </summary>
    public static L5X ProgramExport => OpenResource("Programs.TestProgram.L5X");

    /// <summary>
    /// Gets the L5X instance representing the "Main.L5X" embedded routine export resource.
    /// Provides access to a routine definition exported from a Logix project.
    /// </summary>
    public static L5X RoutineExport => OpenResource("Routines.Main.L5X");

    /// <summary>
    /// Loads an embedded L5X project by name from the assembly's resources.
    /// </summary>
    /// <param name="resource">The name of the embedded L5X project to load.</param>
    /// <returns>An <see cref="L5X"/> instance representing the parsed project.</returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the specified embedded project resource is not found in the assembly.
    /// </exception>
    private static L5X OpenResource(string resource)
    {
        var resourceName = $"{NameSpace}.{resource}";
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream is null)
            throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");

        using var reader = new StreamReader(stream);
        var xml = reader.ReadToEnd();
        return L5X.Parse(xml);
    }
}