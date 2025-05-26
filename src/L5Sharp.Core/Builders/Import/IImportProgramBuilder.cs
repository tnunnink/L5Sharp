using System;

namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
public interface IImportProgramBuilder
{
    IImportProgramBuilder Routine(Scope scope);

    IImportProgramBuilder Routines(Func<Routine, bool> predicate);
}