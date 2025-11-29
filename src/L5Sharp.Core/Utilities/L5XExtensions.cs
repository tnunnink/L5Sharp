using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Extensions methods that help with the base functionality of the library.
/// </summary>
internal static class L5XExtensions
{
    /// <summary>
    /// Determines if the current string is equal to string.Empty.
    /// </summary>
    /// <param name="value">The string input to analyze.</param>
    /// <returns>True if the string is empty. Otherwise, false.</returns>
    internal static bool IsEmpty(this string value) => value.Equals(string.Empty);

    /// <summary>
    /// Determines if this string is the same as, meaning equal to regardless of case, another string.
    /// </summary>
    /// <param name="value">The string value to compare.</param>
    /// <param name="other">The other string to compare.</param>
    /// <returns><c>true</c> if the strings are equal using the <see cref="StringComparer.OrdinalIgnoreCase"/>
    /// equality comparer, Otherwise <c>false</c>.</returns>
    /// <remarks>This is a simplified way of calling the string comparer equals method since it is a little verbose.
    /// This could be used a lot since Logix naming is case agnostic.</remarks>
    internal static bool IsEquivalent(this string value, string? other) =>
        StringComparer.OrdinalIgnoreCase.Equals(value, other);

    /// <summary>
    /// Creates and configures a <see cref="InvalidOperationException"/> to be thrown for required properties of complex
    /// types that do not exist for the current element object.
    /// </summary>
    /// <param name="element">The element for which the exception is occuring.</param>
    /// <param name="name">The name of the attribute or child element that is missing.</param>
    /// <returns>A <see cref="InvalidOperationException"/> object configured with standard message and exception
    /// data for troubleshooting purposes.</returns>
    /// <remarks>This is a helper so to avoid reconfiguring this exception every time it needed to be thrown.
    /// Aside from setting the standard error message, this will add the target attribute name, element line number,
    /// and element object as data to the exception.</remarks>
    internal static InvalidOperationException L5XError(this XElement element, XName name)
    {
        var message = $"The required attribute or child element '{name}' does not exist for {element.Name}.";
        var line = ((IXmlLineInfo)element).HasLineInfo() ? ((IXmlLineInfo)element).LineNumber : -1;
        var exception = new InvalidOperationException(message);
        exception.Data.Add("target", name);
        exception.Data.Add("line", line);
        exception.Data.Add("element", element.ToString());
        return exception;
    }

    /// <summary>
    /// Retrieves the full Logix path of the specified XML element within an L5X file hierarchy.
    /// This path will always start from the controller element and descend to the current element specified.
    /// This path resembles the XPath syntax required to get an element relative to the controller.
    /// </summary>
    /// <param name="element">The XML element for which to retrieve the Logix path.</param>
    /// <returns>A string representing the full Logix path for the specified XML element.</returns>
    internal static string LogixPath(this XElement element)
    {
        //Handle module tags specifically we need to view these as controller tags.
        if (element.IsModuleTagElement())
        {
            return $"Controller/Tags/Tag[@Name='{element.ModuleTagName()}']";
        }

        var builder = new StringBuilder();

        var elements = element.AncestorsAndSelf()
            .TakeWhile(x => x.Name.LocalName != L5XName.RSLogix5000Content)
            .ToArray();

        //Iterate in reverse to build the path from root down to this element.
        for (var i = elements.Length - 1; i >= 0; i--)
        {
            if (i < elements.Length - 1) builder.Append('/');
            var identifier = elements[i].Identifier();
            builder.Append(identifier);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the <c>Name</c> attribute value for the current <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> instance.</param>
    /// <returns>A <see cref="string"/> representing the name value if found; Otherwise, <c>empty</c>.</returns>
    /// <remarks>
    /// This is a helper since we access and use the name attribute, so often I just wanted to make
    /// the code more concise.
    /// </remarks>
    internal static string LogixName(this XElement element)
    {
        return element.Attribute(L5XName.Name)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Gets the name value of the current member <see cref="XElement"/> object. This can be either the <c>Name</c> or
    /// <c>Index</c> attribute depending on what member type it is.
    /// </summary>
    /// <param name="element">The element object for which to get the name.</param>
    /// <returns>A <see cref="string"/> containing the value of the name or index attribute. If neither attribute is
    /// found, then this returns an empty string.</returns>
    internal static string MemberName(this XElement element)
    {
        var name = element.Attribute(L5XName.Name)?.Value;
        if (name is not null) return name;

        var index = element.Attribute(L5XName.Index)?.Value;
        return index ?? string.Empty;
    }

    /// <summary>
    /// Gets the <c>DataType</c> attribute value for the provided element, or it's a parent element, whichever value is
    /// found first.
    /// </summary>
    /// <param name="element">The element to retrieve the data type for.</param>
    /// <returns>A <see cref="string"/> indicating the value of the data type property.</returns>
    /// <remarks>
    /// This is a helper for deserializing data structures. Most data elements have the data type we need
    /// to determine which object to construct, but some don't, and we need to look at its parent element
    /// to find out. Obviously, if we can't find the <c>DataType</c> value then we can't deserialize the type.
    /// </remarks>
    internal static string? DataType(this XElement element)
    {
        //first check the provided element and return if found.
        var local = element.Attribute(L5XName.DataType)?.Value;
        if (local is not null) return local;

        //then check the parent element and return if found.
        var parent = element.Parent?.Attribute(L5XName.DataType)?.Value;
        return parent ?? null;
    }

    /// <summary>
    /// Gets the full <see cref="TagName"/> path for the provided element. 
    /// </summary>
    /// <param name="element">The element for which to determine the tag name.
    /// This can be a tag element or nested data member element.</param>
    /// <returns>A <see cref="TagName"/> that represents the full path to the element.</returns>
    /// <remarks>
    /// This is handy since it will determine the "TagName" for any deeply nested data member. This also
    /// supports the LocalTag and Module tag elements.
    /// </remarks>
    internal static TagName TagName(this XElement element)
    {
        //Ensures we are or in a tag element. If not return empty.
        var root = element.AncestorsAndSelf().FirstOrDefault(e => e.IsTagElement());
        if (root is null)
            return Core.TagName.Empty;

        //Handles special case module tag elements which need to be built from parent module names and slot number.
        if (root.IsModuleTagElement())
        {
            return root.ModuleTagName();
        }

        var tagName = new TagName(root.LogixName());

        //Gets ancestors from here up to right before the root tag element.
        //If this is the tag element, then should not perform iteration, and we return what we have.
        var members = element.AncestorsAndSelf()
            .Where(e => !e.MemberName().IsEmpty())
            .TakeWhile(e => !e.IsTagElement())
            .ToList();

        for (var i = members.Count - 1; i >= 0; i--)
        {
            var member = members[i].MemberName();
            tagName = Core.TagName.Concat(tagName, member);
        }

        return tagName;
    }

    /// <summary>
    /// Determines the tag name for a given <see cref="XElement"/> representing a module IO tag.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> representing the module defined IO tag
    /// (InputTag, OutputTag, or ConfigTag, InAliasTag, or OutAliasTag).</param>
    /// <returns>A <see cref="TagName"/> representing the name of the module IO tag.</returns>
    /// <remarks>
    /// This is a helper extension since the logic is somewhat complex and used in more than one class.
    /// We look up the L5X tree for module name and parent name, as well as back down to find the potential slot of the module.
    /// </remarks>
    internal static TagName ModuleTagName(this XElement element)
    {
        var suffix = DetermineModuleSuffix(element);
        var module = element.Ancestors(L5XName.Module).FirstOrDefault();
        var moduleName = module?.Attribute(L5XName.Name)?.Value;
        var parentName = module?.Attribute(L5XName.ParentModule)?.Value;

        var slot = module?.Descendants(L5XName.Port)
            .Where(p => bool.TryParse(p.Attribute(L5XName.Upstream)?.Value, out var upstream) && upstream
                && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
            .Select(p => p.Attribute(L5XName.Address)?.Value)
            .FirstOrDefault();

        return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";

        string DetermineModuleSuffix(XElement el)
        {
            return el.Name.LocalName switch
            {
                L5XName.InputTag or L5XName.InAliasTag => el.Parent?.Attribute(L5XName.InputTagSuffix)?.Value ?? "I",
                L5XName.OutputTag or L5XName.OutAliasTag => el.Parent?.Attribute(L5XName.OutputTagSuffix)?.Value ?? "O",
                L5XName.ConfigTag => "C",
                _ => throw new NotSupportedException($"Module tag element not supported for {el.Name.LocalName}")
            };
        }
    }

    /// <summary>
    /// Attempts to retrieve the last supported (non-L5K) formatted data element from the specified XElement.
    /// </summary>
    /// <param name="element">The XElement from which to extract the formatted data.</param>
    /// <param name="data">When this method returns, contains the formatted data if found; otherwise, null.</param>
    /// <returns>True if formatted data is successfully found and assigned to the out parameter; otherwise, false.</returns>
    internal static bool TryGetFormattedData(this XElement element, out XElement data)
    {
        //We get the last data element found since Rockwell documentation states that data is applied in document order.
        //L5K is not a supported format.
        var formatted = element.Elements().LastOrDefault(e =>
            e.Name.LocalName is L5XName.Data or L5XName.DefaultData &&
            e.Attribute(L5XName.Format) is not null &&
            e.Attribute(L5XName.Format)?.Value != DataFormat.L5K
        );

        if (formatted is not null)
        {
            data = formatted;
            return true;
        }

        data = null!;
        return false;
    }

    /// <summary>
    /// Generates an identifier for the specified XML element based on its attributes and type.
    /// </summary>
    /// <param name="element">The XML element for which the identifier is generated.</param>
    /// <returns>A string representing the identifier for the XML element.</returns>
    private static string Identifier(this XElement element)
    {
        if (element.IsTagElement() || element.IsDataMemberElement())
        {
            return $"Tag[@Name='{element.TagName()}']";
        }

        if (element.IsComponentElement())
        {
            return $"{element.Name.LocalName}[@Name='{element.LogixName()}']";
        }

        if (element.IsCodeElement())
        {
            return $"{element.Name.LocalName}[@Number='{element.Attribute(L5XName.Number)?.Value ?? string.Empty}']";
        }

        if (element.Attribute(L5XName.ID) is not null)
        {
            return $"{element.Name.LocalName}[@ID='{element.Attribute(L5XName.ID)?.Value ?? string.Empty}']";
        }

        return element.Name.LocalName;
    }

    /// <summary>
    /// Determines if the specified string represents a toplevel container element in the L5X schema.
    /// </summary>
    /// <param name="name">The string to evaluate as a container element.</param>
    /// <returns>True if the string is a recognized container element name; otherwise, false.</returns>
    internal static bool IsContainerName(this string name)
    {
        return name
            is L5XName.DataTypes
            or L5XName.Modules
            or L5XName.AddOnInstructionDefinitions
            or L5XName.Tags
            or L5XName.Programs
            or L5XName.Tasks
            or L5XName.ParameterConnections
            or L5XName.Trends
            or L5XName.QuickWatchList;
    }

    /// <summary>
    /// Determines whether the given XElement represents a component element in the L5X file.
    /// </summary>
    /// <param name="element">The XElement to check.</param>
    /// <returns>
    /// True if the XElement is a component element, such as a DataType, Module, AddOnInstructionDefinition,
    /// Tag, Program, Routine, Task, Trend, or QuickWatchList. Otherwise, false.
    /// </returns>
    internal static bool IsComponentElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.DataType
            or L5XName.Module
            or L5XName.AddOnInstructionDefinition
            or L5XName.Tag
            or L5XName.Program
            or L5XName.Routine
            or L5XName.Task
            or L5XName.Trend
            or L5XName.QuickWatchList;
    }

    /// <summary>
    /// Determines if the specified XML element represents a code element such as a Rung, Line, or Sheet.
    /// </summary>
    /// <param name="element">The XML element to evaluate.</param>
    /// <returns>True if the element is a code element. Otherwise, false.</returns>
    internal static bool IsCodeElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.Rung
            or L5XName.Line
            or L5XName.Sheet;
    }

    /// <summary>
    /// Determines if the current element represents an element, we would deserialize as a <see cref="Tag"/> component.
    /// </summary>
    /// <param name="element">The element to check.</param>
    /// <returns><c>true</c> if the element name is a tag element; otherwise, <c>false</c></returns>
    internal static bool IsTagElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.Tag
            or L5XName.LocalTag
            or L5XName.ConfigTag
            or L5XName.InputTag
            or L5XName.OutputTag
            or L5XName.InAliasTag
            or L5XName.OutAliasTag;
    }

    /// <summary>
    /// Determines if the current element represents an element, we would deserialize as a <see cref="Tag"/> component.
    /// </summary>
    /// <param name="element">The element to check.</param>
    /// <returns><c>true</c> if the element name is a tag element; otherwise, <c>false</c></returns>
    internal static bool IsModuleTagElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.ConfigTag
            or L5XName.InputTag
            or L5XName.OutputTag
            or L5XName.InAliasTag
            or L5XName.OutAliasTag;
    }

    /// <summary>
    /// Determines if the current element represents an element, we would deserialize as a <see cref="Tag"/> component.
    /// </summary>
    /// <param name="element">The element to check.</param>
    /// <returns><c>true</c> if the element name is a tag element; otherwise, <c>false</c></returns>
    private static bool IsDataMemberElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.DataValueMember
            or L5XName.ArrayMember
            or L5XName.StructureMember;
    }

    /// <summary>
    /// Determines if the specified XElement represents a block element within the L5X structure.
    /// </summary>
    /// <param name="element">The XElement to analyze.</param>
    /// <returns>True if the XElement is a block element. Otherwise, false.</returns>
    internal static bool IsBlockElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.IRef
            or L5XName.ORef
            or L5XName.ICon
            or L5XName.OCon
            or L5XName.Block
            or L5XName.Function
            or L5XName.AddOnInstruction
            or L5XName.JSR
            or L5XName.SBR
            or L5XName.RET;
    }

    /// <summary>
    /// Retrieves the block operand value from the given XML element.
    /// </summary>
    /// <param name="element">The XML element from which to extract the block operand.</param>
    /// <returns>A string representing the block operand. Returns an empty string if the operand is not found.</returns>
    internal static Argument GetBlockOperand(this XElement element)
    {
        var name = element.Name.LocalName switch
        {
            L5XName.ICon or L5XName.OCon => L5XName.Name,
            L5XName.JSR or L5XName.SBR or L5XName.RET => L5XName.Routine,
            _ => L5XName.Operand
        };

        return element.Attribute(name)?.Value ?? Argument.Empty;
    }

    /// <summary>
    /// Determines whether the specified XML element is identifiable, based on its attributes or type.
    /// </summary>
    /// <param name="element">The XML element to be evaluated.</param>
    /// <returns>True if the element has a 'Name' or 'Number' attribute or is a module tag element. Otherwise, false.</returns>
    internal static bool IsIdentifiable(this XElement element)
    {
        return element.Attribute(L5XName.Name) is not null
               || element.Attribute(L5XName.Number) is not null
               || element.Attribute(L5XName.ID) is not null
               || element.IsModuleTagElement();
    }

    /// <summary>
    /// Combines the collection of string values into a single string separated by the provided character.
    /// </summary>
    /// <param name="enumerable">The collection to combine.</param>
    /// <param name="separator">The character to separate the items of the collection.</param>
    /// <returns>A <see cref="string"/> containing all the items of the collection separated by the provided character.</returns>
    /// <remarks>This was added to assign with supporting .net standard and making the syntax nicer than using string.Join.</remarks>
    internal static string Combine(this IEnumerable<object>? enumerable, char separator)
    {
        return enumerable is not null ? string.Join(separator.ToString(), enumerable) : string.Empty;
    }

    /// <summary>
    /// Removes a single instance of the provided character from the start and end of the current string. 
    /// </summary>
    /// <param name="value">The string to update.</param>
    /// <param name="character">The character to remove.</param>
    /// <returns>A new <see cref="string"/> with the first and last instance of the provided character.</returns>
    internal static string TrimSingle(this string value, char character)
    {
        if (value.StartsWith(character) && value.EndsWith(character))
            return value.Substring(1, value.Length - 2);

        if (value.StartsWith(character))
            return value.Substring(1, value.Length - 1);

        // ReSharper disable once ReplaceSubstringWithRangeIndexer can't do this with .NET Standard 2.0
        return value.EndsWith(character) ? value.Substring(0, value.Length - 1) : value;
    }

    /// <summary>
    /// Finds and replaces occurrences of a specified string within the attributes, text, and CDATA sections
    /// of the given XML element and its descendants.
    /// </summary>
    /// <param name="element">The XML element where the search and replace operation is performed.</param>
    /// <param name="find">The string to search for within the XML element and its children.</param>
    /// <param name="replace">The string to replace the found occurrences with.</param>
    /// <param name="targets">
    /// An array of target element or attribute names where the replace operation should be restricted.
    /// If empty, all elements and attributes will be included in the operation.
    /// </param>
    internal static void FindAndReplace(this XElement element, string find, string replace, string[] targets)
    {
        foreach (var attribute in element.Attributes())
        {
            TryReplaceAttribute(attribute);
        }

        foreach (var node in element.Nodes().ToList())
        {
            switch (node)
            {
                case XCData data:
                    TryReplaceData(node, data);
                    break;
                case XText text:
                    TryReplaceText(node, text);
                    break;
                case XElement child:
                    child.FindAndReplace(find, replace, targets);
                    break;
            }
        }

        return;

        void TryReplaceAttribute(XAttribute attribute)
        {
            if ((targets.Length > 0 && !targets.Contains(attribute.Name.LocalName)) || !attribute.Value.Contains(find))
                return;

            attribute.Value = attribute.Value.Replace(find, replace);
        }

        void TryReplaceData(XNode node, XCData data)
        {
            if ((targets.Length > 0 && !targets.Contains(node.Parent?.Name.LocalName)) || !data.Value.Contains(find))
                return;

            data.ReplaceWith(new XCData(data.Value.Replace(find, replace)));
        }

        void TryReplaceText(XNode node, XText text)
        {
            if ((targets.Length > 0 && !targets.Contains(node.Parent?.Name.LocalName)) || !text.Value.Contains(find))
                return;

            text.Value = text.Value.Replace(find, replace);
        }
    }

    /// <summary>
    /// Filters a sequence of values by a specific key selector, ensuring that only distinct elements, based on the key, are returned.
    /// </summary>
    /// <param name="source">The source collection to process.</param>
    /// <param name="selector">A function that projects a key for each element in the source collection.</param>
    /// <typeparam name="TSource">The type of elements in the source collection.</typeparam>
    /// <typeparam name="TKey">The type of key used to distinguish elements in the source collection.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains distinct elements from the source collection based on the specified key.</returns>
    internal static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
    {
        var seen = new HashSet<TKey>();

        foreach (var element in source)
        {
            if (seen.Add(selector(element)))
            {
                yield return element;
            }
        }
    }

    /// <summary>
    /// Appends the specified value to the string builder if the provided predicate evaluates to true.
    /// </summary>
    /// <param name="builder">The StringBuilder instance to append to.</param>
    /// <param name="value">The string value to conditionally append.</param>
    /// <param name="predicate">The predicate function used to determine if the value should be appended.</param>
    /// <returns>The original StringBuilder instance, with the value appended if the predicate evaluates to true.</returns>
    public static StringBuilder AppendIf(this StringBuilder builder, string value, Func<bool> predicate)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        if (predicate.Invoke())
        {
            builder.Append(value);
        }

        return builder;
    }

    /// <summary>
    /// Determines whether the specified element is null.
    /// </summary>
    /// <typeparam name="T">The type of the element.</typeparam>
    /// <param name="element">The element to evaluate.</param>
    /// <param name="result">The output parameter that will contain the value of the element if it is not null.</param>
    /// <returns>True if the element is not null; otherwise, false.</returns>
    internal static bool IsNull<T>(this T? element, out T result) where T : class
    {
        if (element is null)
        {
            result = null!;
            return false;
        }

        result = element;
        return true;
    }

    /// <summary>
    /// Determines whether the specified string contains a specific character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to search for.</param>
    /// <returns>
    /// <c>true</c> if the specified string contains the character; otherwise, <c>false</c>.
    /// </returns>
    internal static bool Contains(this string value, char character)
    {
        return value.IndexOf(character) != -1;
    }

    /// <summary>
    /// Determines whether the string starts with the specified character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to compare.</param>
    /// <returns>
    /// true if the string starts with the specified character; otherwise, false.
    /// </returns>
    internal static bool StartsWith(this string value, char character)
    {
        return value.Length != 0 && value[0] == character;
    }

    /// <summary>
    /// Determines whether a string ends with a specified character.
    /// </summary>
    /// <param name="value">The string to search.</param>
    /// <param name="character">The character to compare.</param>
    /// <returns>true if <paramref name="value"/> ends with <paramref name="character"/>; otherwise, false.</returns>
    internal static bool EndsWith(this string value, char character)
    {
        return value.Length != 0 && value[value.Length - 1] == character;
    }

    /// <summary>
    /// Inserts a specified separator character in the string at every defined interval.
    /// </summary>
    /// <param name="value">The original string to be processed.</param>
    /// /// <param name="separator">The character to insert as a separator.</param>
    /// <param name="count">The number of characters between each separator.</param>
    /// <returns>A new string with the separator inserted at the specified intervals.</returns>
    public static string SeparateWith(this string value, string separator, int count)
    {
        if (count == 0 || value.Length < count)
            return value;

        var builder = new StringBuilder();

        for (var i = value.Length - 1; i >= 0; i--)
        {
            builder.Append(value[i]);

            if ((value.Length - i) % count == 0 && i > 0)
                builder.Append(separator);
        }

        return new string(builder.ToString().Reverse().ToArray());
    }

    /// <summary>
    /// Splits a string into substrings based on a specified separator and removal options.
    /// </summary>
    /// <param name="value">The string to split.</param>
    /// <param name="separator">A character used to specify the delimiter for splitting the string.
    /// The separator itself is not included in the resulting substrings.</param>
    /// <param name="options">Specifies whether empty substrings should be removed from the resulting array.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of strings that contains the substrings in the input string that are delimited by the separator.</returns>
    internal static string[] Split(this string value, char separator, StringSplitOptions options)
    {
        return value.Split([separator], options);
    }
}