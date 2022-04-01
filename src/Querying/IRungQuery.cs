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
        /// Expands the collection by adding embedded AOI rungs into the source collection for each AOI found. This call
        /// will also replace the AOI parameter names with operand references.
        /// </summary>
        /// <returns>A new <see cref="IRungQuery"/> having each AOI instance replaced with it's embedded
        /// definition logic.</returns>
        /// <remarks>
        /// This method will replace the parameter names of the embedded logic with the operands of the instruction
        /// text found in each rung. This allows you get a complete nested hierarch of embedded logic into a flattened
        /// list of rungs, which can be very useful for determining where and how tags are referenced in embedded logic.
        /// </remarks>
        IRungQuery Flatten();

        /// <summary>
        /// Filters the collection to include only rungs in the specified program.
        /// </summary>
        /// <param name="programName">The name of the program.</param>
        /// <returns>A new <see cref="IRungQuery"/> containing only rungs in the specified program.</returns>
        IRungQuery InProgram(string programName);

        /// <summary>
        /// Filters the collection to include only rungs with number in the range from first to last.
        /// </summary>
        /// <param name="first">The number of the first rung to include in the filtered collection.</param>
        /// <param name="last">The number of the last rung to include in the filtered collection.</param>
        /// <returns>A new <see cref="IRungQuery"/> containing only rungs in the specified range.</returns>
        IRungQuery InRange(int first, int last);

        /// <summary>
        /// Filters the collection to include only rungs in the specified routine.
        /// </summary>
        /// <param name="routineName">The name of the routine.</param>
        /// <returns>A new <see cref="IRungQuery"/> containing only rungs in the specified routine.</returns>
        /// <remarks>
        /// Note that since each program can contain routines with the same name as other programs, this call
        /// will still return rungs from routines across programs, assuming the source collection contains any. You can
        /// call <see cref="InProgram"/> before or after this call to filter rungs by program as necessary.
        /// </remarks>
        IRungQuery InRoutine(ComponentName routineName);

        /// <summary>
        /// Filters the collection to include only rungs with having tags with the specified data type name. 
        /// </summary>
        /// <param name="typeName">The name of the data type to search.</param>
        /// <returns>
        /// A mew <see cref="IRungQuery"/> containing only rungs having reference to a tag of the specified type.
        /// </returns>
        IRungQuery WithDataType(string typeName);
        
        /// <summary>
        /// Filters the collection to include only rungs with having tags with the specified data type name. 
        /// </summary>
        /// <param name="instructionName">The name of the instruction to search.</param>
        /// <returns>
        /// A mew <see cref="IRungQuery"/> containing only rungs having reference to a tag of the specified type.
        /// </returns>
        IRungQuery WithInstruction(string instructionName);

        /// <summary>
        /// Filters the collection to include only rungs containing the specified tag name reference.
        /// </summary>
        /// <param name="tagName">The <see cref="TagName"/> value to search.</param>
        /// <returns>A new <see cref="IRungQuery"/> containing only rungs with the specified tag name reference.</returns>
        IRungQuery WithTag(TagName tagName);
    }
}