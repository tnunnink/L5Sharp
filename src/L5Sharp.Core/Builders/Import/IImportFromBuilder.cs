namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
public interface IImportFromBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    IImportConflictBuilder From(string fileName);
}