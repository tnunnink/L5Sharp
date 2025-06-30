namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
public interface IImportRoutineBuilder : IImportConfigBuilder<IImportRoutineBuilder>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="programName"></param>
    /// <returns></returns>
    IImportRoutineBuilder InProgram(string programName);
    
    
}