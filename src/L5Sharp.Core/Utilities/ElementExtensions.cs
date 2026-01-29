using System;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Provides a set of extension methods for working with XML elements in the context of L5X files.
/// </summary>
public static class ElementExtensions
{
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
    /// Attempts to retrieve the value of the specified attribute from the given XML element.
    /// </summary>
    /// <param name="element">The XML element to search for the attribute.</param>
    /// <param name="name">The name of the attribute to look for.</param>
    /// <param name="value">The output parameter that will contain the value of the attribute if found, or null if not found.</param>
    /// <returns>True if the attribute is found and the value is retrieved; otherwise, false.</returns>
    public static bool TryGetAttribute(this XElement element, XName name, out string value)
    {
        var attribute = element.Attribute(name);

        if (attribute is null)
        {
            value = null!;
            return false;
        }

        value = attribute.Value;
        return true;
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
    public static string LogixName(this XElement element)
    {
        return element.Attribute(L5XName.Name)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Retrieves the Logix identifier associated with the specified XElement.
    /// </summary>
    /// <param name="element">The XElement from which to extract the identifier.</param>
    /// <returns>A string containing the identifier if found, or an empty string if no identifier exists.</returns>
    /// <remarks>This is the value of either the name, number, or ID attribute, which ever is found first (in that order).</remarks>
    public static string LogixId(this XElement element)
    {
        if (element.Attribute(L5XName.Name) is not null)
            return element.Attribute(L5XName.Name)!.Value;

        if (element.Attribute(L5XName.Number) is not null)
            return element.Attribute(L5XName.Number)!.Value;

        if (element.Attribute(L5XName.ID) is not null)
            return element.Attribute(L5XName.ID)!.Value;

        return string.Empty;
    }

    /// <summary>
    /// Retrieves the name of a member from the specified <see cref="XObject"/>.
    /// </summary>
    /// <param name="obj">The <see cref="XObject"/> representing the XML element or attribute.</param>
    /// <returns>
    /// The name of the member as a string if it exists. If the object is an <see cref="XElement"/>,
    /// it retrieves the "Name" or "Index" attribute value if present, otherwise an empty string.
    /// For an <see cref="XAttribute"/>, it returns the local name of the attribute.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided <see cref="XObject"/> is neither an <see cref="XElement"/> nor an <see cref="XAttribute"/>.
    /// </exception>
    public static string MemberName(this XObject obj)
    {
        return obj switch
        {
            XElement e => e.Attribute(L5XName.Name)?.Value ?? e.Attribute(L5XName.Index)?.Value ?? string.Empty,
            XAttribute a => a.Name.LocalName,
            _ => throw new ArgumentException($"the provided object is not a valid XObject: '{obj.GetType()}'.")
        };
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
    public static string? DataType(this XElement element)
    {
        return element.Attribute(L5XName.DataType)?.Value
               ?? element.Parent?.Attribute(L5XName.DataType)?.Value
               ?? null;
    }

    /// <summary>
    /// Retrieves the tag name path from the specified XElement, constructing a hierarchical path
    /// by traversing the element and its ancestors.
    /// </summary>
    /// <param name="element">The XElement from which to retrieve the tag name path.</param>
    /// <returns>A TagName instance representing the hierarchical path of the tag.</returns>
    public static TagName GetTagNamePath(this XElement element)
    {
        var tagName = TagName.Empty;
        var current = element;

        while (current is not null)
        {
            var memberName = current.MemberName();

            if (!memberName.IsEmpty())
                tagName = TagName.Concat(memberName, tagName);

            if (current.Name.LocalName is L5XName.Tag) break;
            current = current.Parent;
        }

        return tagName;
    }

    /// <summary>
    /// Determines the tag name for a given <see cref="XElement"/> representing a module IO tag.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> representing the module defined IO tag
    /// (InputTag, OutputTag, or ConfigTag, InAliasTag, or OutAliasTag).</param>
    /// <returns>A <see cref="Core.TagName"/> representing the name of the module IO tag.</returns>
    /// <remarks>
    /// This is a helper extension since the logic is somewhat complex and used in more than one class.
    /// We look up the L5X tree for module name and parent name, as well as back down to find the potential slot of the module.
    /// </remarks>
    public static TagName ModuleTagName(this XElement element)
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
    /// Attempts to find an ancestor XElement that matches the specified predicate.
    /// </summary>
    /// <param name="element">The starting XElement for the search.</param>
    /// <param name="predicate">A function to test each ancestor for a condition.</param>
    /// <param name="ancestor">The first ancestor element that matches the predicate if found.</param>
    /// <returns>True if an ancestor matching the predicate is found, otherwise false.</returns>
    public static bool TryGetAncestor(this XElement element, Func<XElement, bool> predicate, out XElement ancestor)
    {
        var current = element.Parent;

        while (current is not null)
        {
            if (predicate(current))
            {
                ancestor = current;
                return true;
            }

            current = current.Parent;
        }

        ancestor = null!;
        return false;
    }

    /// <summary>
    /// Attempts to retrieve the last supported (non-L5K) formatted data element from the specified XElement.
    /// </summary>
    /// <param name="element">The XElement from which to extract the formatted data.</param>
    /// <param name="data">When this method returns, contains the formatted <see cref="LogixData"/> if found; otherwise, null.</param>
    /// <returns>True if formatted data is successfully found and assigned to the out parameter; otherwise, false.</returns>
    public static bool TryGetFormattedData(this XElement element, out LogixData data)
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
            data = formatted.Deserialize<LogixData>();
            return true;
        }

        data = null!;
        return false;
    }

    /// <summary>
    /// Determines if the specified string represents a toplevel container element in the L5X schema.
    /// </summary>
    /// <param name="name">The string to evaluate as a container element.</param>
    /// <returns>True if the string is a recognized container element name; otherwise, false.</returns>
    public static bool IsContainerName(this string name)
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
    public static bool IsComponentElement(this XElement element)
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
    public static bool IsCodeElement(this XElement element)
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
    public static bool IsTagElement(this XElement element)
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
    public static bool IsModuleTagElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.ConfigTag
            or L5XName.InputTag
            or L5XName.OutputTag
            or L5XName.InAliasTag
            or L5XName.OutAliasTag;
    }

    /// <summary>
    /// Retrieves the block operand value from the given XML element.
    /// </summary>
    /// <param name="element">The XML element from which to extract the block operand.</param>
    /// <returns>A string representing the block operand. Returns an empty string if the operand is not found.</returns>
    public static Argument GetBlockOperand(this XElement element)
    {
        var name = element.Name.LocalName switch
        {
            L5XName.ICon or L5XName.OCon => L5XName.Name,
            L5XName.JSR or L5XName.SBR or L5XName.RET => L5XName.Routine,
            _ => L5XName.Operand
        };

        return element.Attribute(name)?.Value ?? Argument.Empty;
    }
}