using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Components;
using L5Sharp.Core;

namespace L5Sharp.Extensions;

/// <summary>
/// A collection of built in extensions for the <see cref="AddOnInstruction"/> component.
/// </summary>
public static class AddOnInstructionExtensions
{
    /// <summary>
    /// Returns the AOI instruction logic with the parameters of the instruction replaced with the provided neutral
    /// text signature arguments.
    /// </summary>
    /// <param name="instruction">The <see cref="AddOnInstruction"/> instance.</param>
    /// <param name="text">The text signature of the instruction arguments.</param>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> representing all the instruction's
    /// logic, with each instruction parameter tag name replaced with the arguments from the provided text.
    /// </returns>
    /// <remarks>
    /// This is helpful when trying to perform deep analysis on logic. By "flattening" the logic we can
    /// reason or evaluate it as if it was written in line. Currently only support <see cref="RllRoutine"/>
    /// types.
    /// </remarks>
    public static IEnumerable<NeutralText> GetLogic(this AddOnInstruction instruction, NeutralText text)
    {
        if (text is null)
            throw new ArgumentNullException(nameof(text));
        
        // All instructions primary logic is contained in the routine names 'Logic'
        var logic = instruction.Routines.FirstOrDefault(r => r.Name == "Logic");

        if (logic is not RllRoutine rll)
            return Enumerable.Empty<NeutralText>();

        //Skip first operand as it is always the aoi tag, which does not have corresponding parameter within the logic.
        var arguments = text.Operands().Select(o => o.ToString()).Skip(1).ToList();

        //Only required parameters are part of the instruction signature
        var parameters = instruction.Parameters.Where(p => p.Required).Select(p => p.Name).ToList();

        //Create a mapping of the provided text operand arguments to instruction parameter names.
        var mapping = arguments.Zip(parameters, (a, p) => new { Argument = a, Parameter = p }).ToList();

        //Replace all parameter names with argument names in the instruction logic text, and return the results.
        return rll.Content.Select(r => r.Text)
            .Select(t => mapping.Aggregate(t, (current, pair) =>
            {
                var replace = $@"(?<=[^.\]]){pair.Parameter}";
                return Regex.Replace(current, replace, pair.Argument.ToString());
            }))
            .ToList();
    }
}