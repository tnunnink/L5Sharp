using System.Collections.Generic;

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
    /// A list of all data formats that are supported for deserialization by this library (everything but L5K).
    /// </summary>
    public static readonly IEnumerable<DataFormat> Supported = new List<DataFormat>
        { Decorated, String, Alarm, Message };

    /// <summary>
    /// Returns the corresponding <see cref="DataFormat"/> for the provided <see cref="LogixData"/>.
    /// </summary>
    /// <param name="data">The <see cref="LogixData"/> to get the data format for.</param>
    /// <returns>The <see cref="DataFormat"/> option indicating how the type's data is formatted in the L5X.</returns>
    public static DataFormat FromData(LogixData data)
    {
        return data switch
        {
            StringData => String,
            ALARM_ANALOG => Alarm,
            ALARM_DIGITAL => Alarm,
            MESSAGE => Message,
            _ => Decorated
        };
    }
}