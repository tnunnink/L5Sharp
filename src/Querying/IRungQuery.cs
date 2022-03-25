using L5Sharp.Core;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="Rung"/> elements
    /// within the L5X context. 
    /// </summary>
    public interface IRungQuery : ILogixQuery<Rung>
    {
        /// <summary>
        /// Filters the collection to include only rungs with number in the range from first to last.
        /// </summary>
        /// <param name="first">The number of the first rung to include in the filtered collection.</param>
        /// <param name="last">The number of the last rung to include in the filtered collection.</param>
        /// <returns>A new <see cref="IRungQuery"/> containing the filtered element collection.</returns>
        IRungQuery InRange(int first, int last);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRungQuery IncludeAddOns();
        
        IRungQuery InRoutine(ComponentName routineName);
        IRungQuery WithTag(TagName tagName);
        IRungQuery WithTag(TagName tagName, TagQueryOptions options);
        IRungQuery WithDataType(string typeName);
        IRungQuery WithComment(string comment);
        IRungQuery WithInstruction(string name);
        IRungQuery WithInstruction(Instruction instruction);
    }
}