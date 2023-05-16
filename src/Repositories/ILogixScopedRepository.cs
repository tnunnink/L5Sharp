using System.Collections.Generic;

namespace L5Sharp.Repositories;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TScoped"></typeparam>
public interface ILogixScopedRepository<TScoped> : IEnumerable<TScoped>
    where TScoped : ILogixComponent, ILogixScoped
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="container"></param>
    /// <returns></returns>
    ILogixComponentRepository<TScoped> In(string container);
}