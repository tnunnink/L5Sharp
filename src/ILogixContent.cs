using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogixContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TComponent"></typeparam>
        void Add<TComponent>(TComponent component) where TComponent : ILogixComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        bool Any<TEntity>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        int Count<TEntity>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        bool Contains<TComponent>(string name) where TComponent : class, ILogixComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        TComponent? Find<TComponent>(string name) where TComponent : class, ILogixComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll<TEntity>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        TComponent Get<TComponent>(string name) where TComponent : class, ILogixComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        void Remove<TComponent>(string name) where TComponent : class, ILogixComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scopeName"></param>
        /// <returns></returns>
        ILogixScopedContent IsScope(Scope scope, string? scopeName = null);
        
        ILogixScopedContent InProgram(string programName);
        
        ILogixScopedContent InRoutine(string routineName, string? programName);
    }
}