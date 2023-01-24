using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogixScopedContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TScoped"></typeparam>
        void Add<TScoped>(TScoped component) where TScoped : ILogixScopedComponent;

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
        bool Count<TEntity>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TScoped"></typeparam>
        /// <returns></returns>
        bool Contains<TScoped>(string name) where TScoped : class, ILogixScopedComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TScoped"></typeparam>
        /// <returns></returns>
        TScoped? Find<TScoped>(string name) where TScoped : class, ILogixScopedComponent;

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
        /// <typeparam name="TScoped"></typeparam>
        /// <returns></returns>
        TScoped Get<TScoped>(string name) where TScoped : class, ILogixScopedComponent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TScoped"></typeparam>
        /// <returns></returns>
        bool Remove<TScoped>(string name) where TScoped : class, ILogixScopedComponent;
    }
}