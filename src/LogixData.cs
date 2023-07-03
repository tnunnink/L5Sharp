using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;

namespace L5Sharp;

/// <summary>
/// A specialized static factory for deserializing <see cref="LogixType"/> objects from <see cref="XElement"/>.
/// </summary>
public static class LogixData
{
    /// <summary>
    /// Returns the singleton null <see cref="LogixType"/> object.
    /// </summary>
    public static LogixType Null => NullType.Instance;

    /// <summary>
    /// Deserializes an <see cref="XElement"/> into a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="element">The element to deserialize.</param>
    /// <returns>A <see cref="LogixType"/> representing the data value, structure, or array or the provided element.</returns>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="NotSupportedException"><c>element</c> has a name that is not supported for deserialization.</exception>
    public static LogixType Deserialize(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        return element.Name.ToString() switch
        {
            L5XName.Tag => DeserializeData(element),
            L5XName.LocalTag => DeserializeData(element),
            L5XName.Data => DeserializeFormatted(element),
            L5XName.DefaultData => DeserializeFormatted(element),
            L5XName.DataValue => DeserializeAtomic(element),
            L5XName.DataValueMember => DeserializeAtomic(element),
            L5XName.Element => DeserializeElement(element),
            L5XName.Array => DeserializeArray(element),
            L5XName.ArrayMember => DeserializeArray(element),
            L5XName.Structure => DeserializeStructure(element),
            L5XName.StructureMember => DeserializeStructure(element),
            L5XName.AlarmAnalogParameters => new ALARM_ANALOG(element),
            L5XName.AlarmDigitalParameters => new ALARM_DIGITAL(element),
            L5XName.MessageParameters => new MESSAGE(element),
            _ => throw new NotSupportedException(
                $"The element name '{element.Name}' is not able to be used to deserialize a logix type.")
        };
    }

    /// <summary>
    /// Handles deserializing the data element of the root tag. This method will forward call down the chain
    /// based on the format of the data structure.
    /// </summary>
    private static LogixType DeserializeData(XContainer element)
    {
        var supportedFormats = DataFormat.All().Where(f => f != DataFormat.L5K);
        
        //Don't specify element name to support both Data and DefaultData elements.
        //Just look for elements with supportable Format attribute.
        var data = element.Elements()
            .FirstOrDefault(e => e.Attribute(L5XName.Format) is not null
                                 && supportedFormats.Any(f => f.Value == e.Attribute(L5XName.Format)!.Value));

        return data is not null ? Deserialize(data) : Null;
    }

    /// <summary>
    /// Handles deserializing a formatted data element to a logix type. This method will forward call down the chain
    /// based on the format of the data structure.
    /// </summary>
    private static LogixType DeserializeFormatted(XElement element)
    {
        DataFormat.TryFromName(element.Attribute(L5XName.Format)?.Value, out var format);
        if (format is null) return Null;
        if (format == DataFormat.String) return DeserializeString(element);
        return element.FirstNode is XElement root ? Deserialize(root) : Null;
    }

    /// <summary>
    /// Handles deserializing an element to a atomic value type logix type.
    /// This method will call upon <see cref="Atomic"/> to generate the known concrete atomic type we need.
    /// This implementation ignores any specified Radix because we have found cases where the Radix does not match
    /// the actual format of the value, therefore, we always infer the format, and then parse.
    /// </summary>
    private static LogixType DeserializeAtomic(XElement element)
    {
        var name = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);
        var value = element.Attribute(L5XName.Value)?.Value ?? throw new L5XException(L5XName.Value, element);
        return Atomic.Parse(name, value);
    }

    /// <summary>
    /// Handles deserializing an array element to a <see cref="ArrayType"/>.
    /// </summary>
    private static LogixType DeserializeArray(XElement element) => new ArrayType(element);

    /// <summary>
    /// Handles deserializing an array index element to a logix type, either atomic, string, or structure,
    /// depending on the state of the element.
    /// </summary>
    private static LogixType DeserializeElement(XElement element)
    {
        var value = element.Attribute(L5XName.Value);
        var structure = element.Element(L5XName.Structure);
        var name = element.Parent?.Attribute(L5XName.DataType)?.Value ??
                   throw new L5XException(L5XName.DataType, element);

        return value is not null ? Atomic.Parse(name, value.Value) :
            structure is not null ? DeserializeStructure(structure) :
            throw new L5XException(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StructureType"/> logix type.
    /// Will check <see cref="LogixSerializer"/> for registered types to create the concrete type if available.
    /// Otherwise we resort to a more generic <see cref="StringType"/> of string or <see cref="ComplexType"/> for everything else.
    /// </summary>
    private static LogixType DeserializeStructure(XElement element)
    {
        var dataType = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);

        if (LogixSerializer.IsRegistered(dataType))
            return (LogixType)LogixSerializer.Deserialize(dataType, element);

        return HasStringStructure(element) ? new StringType(element) : new ComplexType(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StringType"/> logix type.
    /// Will check <see cref="LogixSerializer"/> for registered type to create the concrete type if available.
    /// Otherwise we resort to a more generic <see cref="StringType"/> object.
    /// </summary>
    private static LogixType DeserializeString(XElement element)
    {
        var dataType = element.Parent?.Attribute(L5XName.DataType)?.Value ??
                       throw new L5XException(L5XName.DataType, element);

        if (LogixSerializer.IsRegistered(dataType))
            return (LogixType)LogixSerializer.Deserialize(dataType, element);

        return new StringType(element);
    }

    /// <summary>
    /// Determines if the provided element has a structure that indicates it is a <see cref="StringType"/> structure.
    /// </summary>
    private static bool HasStringStructure(XElement element)
    {
        return element.Descendants(L5XName.DataValueMember).Any(e =>
            e?.Value is not null
            && e.Attribute(L5XName.Name)?.Value == "DATA"
            && e.Attribute(L5XName.DataType)?.Value == element.Attribute(L5XName.DataType)?.Value
            && e.Attribute(L5XName.Radix)?.Value == "ASCII");
    }
}