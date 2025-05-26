namespace L5Sharp.Core;


public interface IImportConflictBuilder
{
    
    IImportUpdateBuilder UseExisting();

    /// <summary>
    /// Specifies that the Logix object should overwrite the existing object
    /// during the import operation when encountering a conflict.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> for further configuration of the import process.
    /// </returns>
    IImportUpdateBuilder Overwrite();

    /// <summary>
    /// Specifies that the import operation should be canceled when encountering a conflict,
    /// preventing any further action with the conflicting Logix object.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IImportUpdateBuilder{TObject}"/> for further configuration of the import process.
    /// </returns>
    IImportUpdateBuilder Abort();
}