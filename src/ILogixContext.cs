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
        /// Returns a <see cref="IComponentQuery{TComponent}"/> that provides basic querying for
        /// <see cref="IComplexType"/> components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IComplexType> DataTypes();

        /// <summary>
        /// Executes the provided <see cref="DataTypeQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IComplexType"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines a query (or queries) to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IComplexType"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuery{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IComplexType> DataTypes(Func<DataTypeQuery, DataTypeQuery> query);

        /// <summary>
        /// Returns a <see cref="IComponentQuery{TComponent}"/> that provides basic querying for
        /// <see cref="IModule"/> components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IModule> Modules();

        /// <summary>
        /// Executes the provided <see cref="ModuleQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IComplexType"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines a query (or queries) to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IComplexType"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuery{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IModule> Modules(Func<ModuleQuery, ModuleQuery> query);

        /// <summary>
        /// Returns a <see cref="IComponentQuery{TComponent}"/> that provides basic querying for
        /// <see cref="IAddOnInstruction"/> components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IAddOnInstruction> Instructions();
        
        /// <summary>
        /// Executes the provided <see cref="InstructionQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IComplexType"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines a query (or queries) to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IComplexType"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuery{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IAddOnInstruction> Instructions(Func<InstructionQuery, InstructionQuery> query);

        /// <summary>
        /// Returns a <see cref="IComponentQuery{TComponent}"/> that provides basic querying for
        /// <see cref="ITag{TDataType}"/> components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<ITag<IDataType>> Tags();

        /// <summary>
        /// Executes the provided <see cref="TagQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IComplexType"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines a query (or queries) to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IComplexType"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuery{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<ITag<IDataType>> Tags(Func<TagQuery, TagQuery> query);

        /// <summary>
        /// Returns a <see cref="IComponentQuery{TComponent}"/> that provides basic querying for
        /// <see cref="IProgram"/> components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<IProgram> Programs();

        /// <summary>
        /// Executes the provided <see cref="ProgramQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="IComplexType"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines a query (or queries) to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="IComplexType"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuery{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<IProgram> Programs(Func<ProgramQuery, ProgramQuery> query);

        /// <summary>
        /// Returns a <see cref="IComponentQuery{TComponent}"/> that provides basic querying for
        /// <see cref="ITask"/> components in the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentQuery<ITask> Tasks();

        /// <summary>
        /// Executes the provided <see cref="TaskQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="ITask"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines a query (or queries) to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="ITask"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build up a more
        /// complex or advanced query than is otherwise not available on the standard
        /// <see cref="IComponentQuery{TComponent}"/> API.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<ITask> Tasks(Func<TaskQuery, TaskQuery> query);

        /// <summary>
        /// Executes the provided <see cref="TaskQuery"/> over the current <see cref="ILogixContext"/>, returning
        /// a collection of filtered <see cref="ITask"/> objects.  
        /// </summary>
        /// <param name="query">The function delegate that defines a query (or queries) to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the queried collection of <see cref="ITask"/>
        /// components.</returns>
        /// <remarks>
        /// This overload allows the user to chain multiple calls in a fluent fashion in order to build
        /// complex or advanced queries. This query also cuts across component boundaries, meaning it will query
        /// against all <see cref="Rung"/> objects in the entire <see cref="ILogixContext"/>.
        /// These query objects can also be extended with custom queries using extensions methods.
        /// </remarks>
        IEnumerable<Rung> Rungs(Func<RungQuery, RungQuery> query);

        /// <summary>
        /// Saves the content of the <see cref="ILogixContext"/> to the specified file name.
        /// </summary>
        /// <param name="fileName">The name of the full file name path to save the context.</param>
        void Save(string fileName);
    }
}