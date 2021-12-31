using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInstruction
    {
        /// <summary>
        /// Gets the name of the current <see cref="IInstruction"/> instance.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the collection of <see cref="Argument"/> for the current <c>IInstruction</c> instance.
        /// </summary>
        IEnumerable<Argument> Arguments { get; }

        /// <summary>
        /// Generates the <see cref="NeutralText"/> instance for the current <c>IInstruction</c>.
        /// </summary>
        /// <returns>A new <see cref="NeutralText"/> object that represents the instruction text.</returns>
        NeutralText ToText();

        /// <summary>
        /// Generates a new <see cref="IInstruction"/> instance with the provided string arguments.
        /// </summary>
        /// <param name="arguments">Set of string arguments to instantiate the <see cref="IInstruction"/> with.</param>
        /// <returns>A new <see cref="IInstruction"/> with the provided arguments.</returns>
        IInstruction Of(params string[] arguments);
    }
}