using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public interface ILogixComponentCollection<TComponent> : IEnumerable<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TComponent"></typeparam>
        void Add(TComponent component);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        bool Contains(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        TComponent? Find(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        TComponent Get(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        bool Remove(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        bool Replace(TComponent component);
    }
}