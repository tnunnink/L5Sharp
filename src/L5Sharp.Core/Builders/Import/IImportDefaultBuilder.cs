namespace L5Sharp.Core;

/// <summary>
/// Provides functionality to build and configure default import settings for components within the system.
/// This interface is used to customize the behavior of the import process, including options such as
/// renaming, modifying, overwriting, or managing components during the import operation.
/// </summary>
public interface IImportDefaultBuilder : IImportConfigBuilder<IImportDefaultBuilder>
{
}