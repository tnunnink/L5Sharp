namespace L5Sharp.Core;

/// <summary>
/// Represents a contract for executing an import operation within a system or application.
/// Provides a method to execute the import process, ensuring proper integration of data or components.
/// </summary>
public interface IImportRunner
{
    /// <summary>
    /// Executes the import process, integrating the required data or components based on the configured settings.
    /// This method is responsible for ensuring that all target elements and their dependencies are properly processed
    /// during the import operation.
    /// </summary>
    void Run();
}