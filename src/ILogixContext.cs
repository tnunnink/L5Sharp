using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Querying;

namespace L5Sharp
{
    /// <summary>
    /// The entry point for querying and manipulating an L5X file. Context is the 
    /// </summary>
    public interface ILogixContext
    {
        /// <summary>
        /// Gets the <see cref="IController"/> instance of the current <see cref="ILogixContext"/>.
        /// </summary>
        IController? Controller();

        /// <summary>
        /// Returns an <see cref="IComponentQuery{TComponent}"/> for querying and manipulating <see cref="IComplexType"/>
        /// components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IComplexType> DataTypes();

        /// <summary>
        /// Executes the provided <see cref="DataTypeQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IComplexType"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines the query to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IComplexType"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuerier{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IComplexType> DataTypes(Func<IDataTypeQuery, IDataTypeQuery> query); 

        /// <summary>
        /// Returns an <see cref="IModuleRepository"/> instance for querying and manipulating <see cref="IModule"/>
        /// components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IModule> Modules();

        /// <summary>
        /// Executes the provided <see cref="IModuleQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IModule"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines the query to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IModule"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuerier{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IModule> Modules(Func<IModuleQuery, IModuleQuery> query);

        /// <summary>
        /// Returns an <see cref="IInstructionRepository"/> instance for querying and manipulating
        /// <see cref="IAddOnInstruction"/> components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IAddOnInstruction> Instructions();
        
        /// <summary>
        /// Executes the provided <see cref="IInstructionQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IAddOnInstruction"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines the query to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IAddOnInstruction"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuerier{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IAddOnInstruction> Instructions(Func<IInstructionQuery, IInstructionQuery> query);

        /// <summary>
        /// Returns an <see cref="ITagRepository"/> instance for querying and manipulating <see cref="ITag{TDataType}"/>
        /// components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        /// <remarks>
        /// This repository operators over controller scoped tags only.
        /// To get a scoped repository, use the <see cref="Tags(ComponentName)"/> overload to specify the program scope.
        /// </remarks>
        IComponentQuery<ITag<IDataType>> Tags();

        /// <summary>
        /// Executes the provided <see cref="ITagQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="ITag{TDataType}"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines the query to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="ITag{TDataType}"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuerier{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<ITag<IDataType>> Tags(Func<ITagQuery, ITagQuery> query);

        /// <summary>
        /// Returns an <see cref="IProgramRepository"/> instance for querying and manipulating <see cref="IProgram"/>
        /// components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IProgram> Programs();

        /// <summary>
        /// Executes the provided <see cref="IProgramQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IProgram"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines the query to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IProgram"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuerier{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IProgram> Programs(Func<IProgramQuery, IProgramQuery> query);

        /// <summary>
        /// Returns an <see cref="IComponentQuerier{TComponent}"/> of  <see cref="ITask"/> components to perform basic
        /// queries oer the current <see cref="ILogixContext"/>.
        /// </summary>
        /// <remarks>
        /// Note that tasks do not have a repository, as they are not a mutable component. The reason is that tasks
        /// can not be imported into a Logix project.
        /// </remarks>
        IComponentQuery<ITask> Tasks();

        /// <summary>
        /// Executes the provided <see cref="ITaskQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="ITask"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines the query to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="ITask"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuerier{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<ITask> Tasks(Func<ITaskQuery, ITaskQuery> query);

        /// <summary>
        /// Executes the provided <see cref="IRungQuery"/> over the current <see cref="ILogixContext"/> returning
        /// a collection of filtered <see cref="Rung"/> objects.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Rung> Rungs(Func<IRungQuery, IRungQuery> query);

        /// <summary>
        /// Saves the content of the <see cref="ILogixContext"/> to the specified file name.
        /// </summary>
        /// <param name="fileName">The name of the full file name path to save the context.</param>
        void Save(string fileName);
    }
}