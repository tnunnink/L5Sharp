namespace L5Sharp.Core;

/// <summary>
/// Defines methods for building import configurations for various types within an L5X file.
/// </summary>
public interface IImportTypeBuilder
{
    /// <summary>
    /// Specifies the data type to be imported, with an optional name.
    /// </summary>
    /// <param name="name">The name of the data type to be imported. If null, the import process will use the
    /// target name from the source L5X file.</param>
    /// <returns>An <see cref="IImportBaseBuilder"/> instance used to configure the remaining import operations.</returns>
    IImportBaseBuilder DataType(string? name = null);

    /// <summary>
    /// Specifies the instruction type to be imported, with an optional name.
    /// </summary>
    /// <param name="name">The name of the instruction to be imported. If null, the import process will use the
    /// target name from the source L5X file.</param>
    /// <returns>An <see cref="IImportBaseBuilder"/> instance used to configure the remaining import operations.</returns>
    IImportBaseBuilder Instruction(string? name = null);

    /// <summary>
    /// Configures the builder to import a module with the specified name if provided. If no name is provided, then will
    /// default to the target component of the import file. 
    /// </summary>
    /// <param name="name">The name of the module to be imported. If null, the import process will se the
    /// target name from the source L5X file.</param>
    /// <returns>An <see cref="IImportModuleBuilder"/> instance used to configure the remaining import operations.</returns>
    IImportModuleBuilder Module(string? name = null);

    /// <summary>
    /// Specifies the program to be imported, with an optional name.
    /// </summary>
    /// <param name="name">The name of the program to be imported. If null, the import process will use the
    /// target name of the source L5X.</param>
    /// <returns>An <see cref="IImportProgramBuilder"/> instance used to configure the remaining import operations.</returns>
    IImportProgramBuilder Program(string? name = null);

    /// <summary>
    /// Specifies the routine to be imported, optionally scoped by the provided object.
    /// </summary>
    /// <param name="scope">The scope in which the routine resides. If null, the routine will be treated as global.</param>
    /// <returns>An <see cref="IImportRoutineBuilder"/> instance used to configure the remaining import operations.</returns>
    IImportRoutineBuilder Routine(Scope? scope = null);
}