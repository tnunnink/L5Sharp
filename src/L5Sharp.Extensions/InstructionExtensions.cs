using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;

namespace L5Sharp.Extensions;

/// <summary>
/// A class containing extensions for the <see cref="AddOnInstruction"/> component.
/// </summary>
public static class InstructionExtensions
{
    /// <summary>
    /// Returns the AOI instruction logic with the parameters tag names replaced with the operand tag names of the
    /// provided neutral text signature.
    /// </summary>
    /// <param name="instruction">The <see cref="AddOnInstruction"/> component.</param>
    /// <param name="text">The text signature of the instruction arguments.</param>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> representing all the instruction's
    /// logic, with each instruction parameter tag name replaced with the arguments from the provided text.
    /// </returns>
    /// <remarks>
    /// This is helpful when trying to perform deep analysis on logic. By "flattening" the logic we can
    /// reason or evaluate it as if it was written in line. Currently only supports <see cref="Rung"/>
    /// content or code type.
    /// </remarks>
    public static IEnumerable<NeutralText> Logic(this AddOnInstruction instruction, NeutralText text)
    {
        if (text is null)
            throw new ArgumentNullException(nameof(text));

        // All instructions primary logic is contained in the routine named 'Logic'
        var logic = instruction.Routines.FirstOrDefault(r => r.Name == "Logic");

        var rungs = logic?.Content<Rung>();
        if (rungs is null) return Enumerable.Empty<NeutralText>();

        //Skip first operand as it is always the AOI tag, which does not have corresponding parameter within the logic.
        var arguments = text.Operands().Select(o => o.ToString()).Skip(1).ToList();

        //Only required parameters are part of the instruction signature
        var parameters = instruction.Parameters.Where(p => p.Required is true).Select(p => p.Name).ToList();

        //Deserialize a mapping of the provided text operand arguments to instruction parameter names.
        var mapping = arguments.Zip(parameters, (a, p) => new { Argument = a, Parameter = p }).ToList();

        //Replace all parameter names with argument names in the instruction logic text, and return the results.
        return rungs.Select(r => r.Text)
            .Select(t => mapping.Aggregate(t, (current, pair) =>
            {
                if (!pair.Argument.IsTagName()) return current;
                var replace = $@"(?<=[^.]){pair.Parameter}\b";
                return Regex.Replace(current, replace, pair.Argument.ToString());
            }))
            .ToList();
    }
}