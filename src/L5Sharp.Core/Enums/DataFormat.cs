using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the types of data formats produced by a L5X file.
/// </summary>
public class DataFormat : LogixEnum<DataFormat, string>
{
    private DataFormat(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a common verbose formatted data structure. 
    /// </summary>
    public static readonly DataFormat Decorated = new(nameof(Decorated), nameof(Decorated));

    /// <summary>
    /// Represents String formatted data structure.
    /// </summary>
    public static readonly DataFormat String = new(nameof(String), nameof(String));

    /// <summary>
    /// Represents Alarm formatted data structure.
    /// </summary>
    public static readonly DataFormat Alarm = new(nameof(Alarm), nameof(Alarm));

    /// <summary>
    /// Represents Message formatted data structure.
    /// </summary>
    public static readonly DataFormat Message = new(nameof(Message), nameof(Message));

    /// <summary>
    /// Represents L5K formatted data structure.
    /// </summary>
    public static readonly DataFormat L5K = new(nameof(L5K), nameof(L5K));

    /// <summary>
    /// Wraps the provided <see cref="LogixData"/> instance in the proper data formatted XML element based on the type
    /// of the data instance provided.
    /// </summary>
    /// <param name="data">The <see cref="LogixData"/> instance to be formatted into the XML element.</param>
    /// <param name="type">
    /// The element type that the data is formatting for. This is used to determine the name of the data element.
    /// <see cref="Parameter"/> and <see cref="LocalTag"/> must have name <c>DefaultData</c>, and everything else
    /// (<see cref="Tag"/>), will have the normal <c>Data</c> name.
    /// </param>
    /// <returns>An <see cref="XElement"/> representing the formatted data with the appropriate format attribute and child elements.</returns>
    public static XElement Format(LogixData? data, Type type)
    {
        //Determine the proper data element name from the specified element type.
        var name = type == typeof(Parameter) || type == typeof(LocalTag)
            ? L5XName.DefaultData
            : L5XName.Data;

        data ??= LogixType.Null;

        //First check for a string type since there is no child element (Data is the element in this case)
        if (data is StringData stringData)
            return stringData.Serialize();

        var format = data switch
        {
            AlarmData => Alarm,
            MessageData => Message,
            _ => Decorated
        };

        //All other format types are wrapped in a containing data element with a format attribute.
        var formatted = new XElement(name, new XAttribute(L5XName.Format, format));
        formatted.Add(data.Serialize());
        return formatted;
    }
}