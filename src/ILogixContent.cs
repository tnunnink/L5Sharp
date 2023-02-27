using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using L5Sharp.Components;

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
        Controller? Controller();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<DataType> DataTypes();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<AddOnInstruction> Instructions();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<Module> Modules();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<Program> Programs();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<Tag> Tags();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<Tag> Tags(string scope);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<Routine> Routines(string scope);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<TRoutine> Routines<TRoutine>(string scope) where TRoutine : Routine;
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILogixComponentCollection<Task> Tasks();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> Query<TEntity>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate);
    }
}