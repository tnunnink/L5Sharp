namespace L5Sharp.Core;

/// <summary>
/// Represents a builder interface for importing sources into an application or system.
/// Provides methods to specify the source file for the import process.
/// </summary>
public interface IImportSourceBuilder
{
    /// <summary>
    /// Specifies the source file from which data will be imported.
    /// </summary>
    /// <param name="fileName">The full path or name of the file to be used as the import source.</param>
    /// <returns>An instance of <see cref="IImportTypeBuilder"/> to allow further configuration of the import process.</returns>
    IImportTypeBuilder From(string fileName);

    /// <summary>
    /// Specifies the L5X from which data will be imported.
    /// </summary>
    /// <param name="content">The L5X content object to be used as the import source.</param>
    /// <returns>An instance of <see cref="IImportTypeBuilder"/> to allow further configuration of the import process.</returns>
    IImportTypeBuilder From(L5X content);
}