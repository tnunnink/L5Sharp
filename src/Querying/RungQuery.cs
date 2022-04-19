using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using L5Sharp.Comparers;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="Rung"/> elements
    /// within the L5X context. 
    /// </summary>
    public class RungQuery : LogixQuery<Rung>
    {
        /// <summary>
        /// Creates a new <see cref="RungQuery"/> with the provided source element to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public RungQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <summary>
        /// Expands the collection by adding embedded AOI rungs into the source collection for each AOI found. This call
        /// will also replace the AOI parameter names with operand references.
        /// </summary>
        /// <returns>A new <see cref="RungQuery"/> having each AOI instance replaced with it's embedded
        /// definition logic.</returns>
        /// <remarks>
        /// This method will replace the parameter names of the embedded logic with the operands of the instruction
        /// text found in each rung. This allows you get a complete nested hierarch of embedded logic into a flattened
        /// list of rungs, which can be very useful for determining where and how tags are referenced in embedded logic.
        /// </remarks>
        public RungQuery Flatten()
        {
            var results = new List<XElement>();

            foreach (var element in this)
            {
                var text = element.Element(L5XName.Text)?.Value.Parse<NeutralText>();

                if (text is null) continue;

                var instructions = text.Instructions();

                foreach (var instruction in instructions)
                {
                    var definition = element.Ancestors(L5XName.Controller).FirstOrDefault()
                        ?.Descendants(L5XName.AddOnInstructionDefinition)
                        .FirstOrDefault(e => instruction.Name == e.ComponentName());

                    if (definition is null)
                    {
                        //only add original if not an AOI. AOI is replaced by contained logic
                        results.Add(element);
                        continue;
                    }

                    //Skip first as it is always the aoi tag, which does not have corresponding parameter
                    var arguments = instruction.Operands.Select(o => o).Skip(1).ToList();

                    //Only required parameters are part of the instruction signature
                    var parameters = definition.Descendants(L5XName.Parameter)
                        .Where(e => bool.Parse(e.Attribute(L5XName.Required)?.Value!))
                        .Select(p => p.ComponentName());

                    var mapping = arguments.Zip(parameters, (a, p) => new { Argument = a, Parameter = p }).ToList();

                    var rungs = definition.Descendants(L5XName.Rung).ToList();

                    foreach (var rung in rungs)
                    {
                        var value = rung.Element(L5XName.Text)?.Value;

                        if (string.IsNullOrEmpty(value))
                            continue;

                        value = mapping.Aggregate(value,
                            (current, pair) =>
                            {
                                var replace = $@"(?<=[^.\]]){pair.Parameter}";
                                return Regex.Replace(current, replace, pair.Argument.ToString());
                            });

                        rung.Element(L5XName.Text)!.Value = value;

                        results.Add(rung);
                    }
                }
            }

            return new RungQuery(results);
        }

        /// <summary>
        /// Filters the collection to include only rungs in the specified program.
        /// </summary>
        /// <param name="programName">The name of the program.</param>
        /// <returns>A new <see cref="RungQuery"/> containing only rungs in the specified program.</returns>
        public RungQuery InProgram(string programName)
        {
            var results = this.Where(e =>
            {
                var program = e.Ancestors(L5XName.Program).FirstOrDefault()?.ComponentName();
                return program is not null && string.Equals(program, programName, StringComparison.OrdinalIgnoreCase);
            });

            return new RungQuery(results);
        }

        /// <summary>
        /// Filters the collection to include only rungs with number in the range from first to last.
        /// </summary>
        /// <param name="first">The number of the first rung to include in the filtered collection.</param>
        /// <param name="last">The number of the last rung to include in the filtered collection.</param>
        /// <returns>A new <see cref="RungQuery"/> containing only rungs in the specified range.</returns>
        public RungQuery InRange(int first, int last)
        {
            var results = this.Where(e =>
            {
                var number = int.Parse(e.Attribute(L5XName.Number)?.Value!);
                return number >= first && number <= last;
            });

            return new RungQuery(results);
        }

        /// <summary>
        /// Filters the collection to include only rungs in the specified routine.
        /// </summary>
        /// <param name="routineName">The name of the routine.</param>
        /// <returns>A new <see cref="RungQuery"/> containing only rungs in the specified routine.</returns>
        /// <remarks>
        /// Note that since each program can contain routines with the same name as other programs, this call
        /// will still return rungs from routines across programs, assuming the source collection contains any. You can
        /// call <see cref="InProgram"/> before or after this call to filter rungs by program as necessary.
        /// </remarks>
        public RungQuery InRoutine(string routineName)
        {
            var results = this.Where(e =>
            {
                var routine = e.Ancestors(L5XName.Routine).FirstOrDefault()?.ComponentName();
                return routine is not null && string.Equals(routine, routineName, StringComparison.OrdinalIgnoreCase);
            });
            
            return new RungQuery(results);
        }

        /// <summary>
        /// Filters the collection to include only rungs with having tags with the specified data type name. 
        /// </summary>
        /// <param name="instructionName">The name of the instruction to search.</param>
        /// <returns>
        /// A mew <see cref="RungQuery"/> containing only rungs having reference to a tag of the specified type.
        /// </returns>
        public RungQuery WithInstruction(string instructionName)
        {
            var results = this.Where(e =>
            {
                var text = e.Element(L5XName.Text)?.Value.Parse<NeutralText>();
                return text is not null && text.Instructions().Any(i => i.Name == instructionName);
            });

            return new RungQuery(results);
        }

        /// <summary>
        /// Filters the collection to include only rungs containing the specified tag name reference.
        /// </summary>
        /// <param name="tagName">The <see cref="TagName"/> value to search.</param>
        /// <returns>A new <see cref="RungQuery"/> containing only rungs with the specified tag name reference.</returns>
        public RungQuery WithTag(TagName tagName)
        {
            var results = this.Where(e =>
            {
                var text = e.Element(L5XName.Text)?.Value.Parse<NeutralText>();
                return text is not null &&
                       text.TagNames().Contains(tagName, TagNameComparer.FullName);
            });

            return new RungQuery(results);
        }

        /// <summary>
        /// Filters the collection to include only rungs containing the specified tag name reference using the provided
        /// <see cref="TagNameComparer"/> to check equality.
        /// </summary>
        /// <param name="tagName">The <see cref="TagName"/> value to search.</param>
        /// <param name="comparer">The <see cref="TagNameComparer"/> instance to use to evaluate the equality of the
        /// rung's tag name references.</param>
        /// <returns>A new <see cref="RungQuery"/> containing only rungs with the specified tag name reference.</returns>
        public RungQuery WithTag(TagName tagName, TagNameComparer comparer)
        {
            var results = this.Where(e =>
            {
                var text = e.Element(L5XName.Text)?.Value.Parse<NeutralText>();
                return text is not null &&
                       text.TagNames().Contains(tagName, comparer);
            });

            return new RungQuery(results);
        }
        
        /// <summary>
        /// Filters the collection to include only rungs containing the specified collection of tag name references.
        /// </summary>
        /// <param name="tagNames">The collection of <see cref="TagName"/> values to filter.</param>
        /// <returns>A new <see cref="RungQuery"/> containing only rungs with the specified tag name references.</returns>
        public RungQuery WithTags(IEnumerable<TagName> tagNames)
        {
            if (tagNames is null)
                throw new ArgumentNullException(nameof(tagNames));
            
            var results = this.Where(e =>
            {
                var text = e.Element(L5XName.Text)?.Value.Parse<NeutralText>();
                return text is not null && 
                       text.TagNames().Any(t => tagNames.Contains(t, TagNameComparer.FullName));
            });

            return new RungQuery(results);
        }
    }
}