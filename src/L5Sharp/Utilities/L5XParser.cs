using System;
using System.Collections.Generic;
using System.ComponentModel;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Utilities;

/// <summary>
/// Static class containing mappings for converting string values (XML typed value) to the strongly type object.
/// </summary>
public static class L5XParser
{
    private static readonly Dictionary<Type, Func<string, object>> Parsers = new()
    {
        { typeof(BOOL), BOOL.Parse},
        { typeof(SINT), SINT.Parse},
        { typeof(INT), INT.Parse},
        { typeof(DINT), DINT.Parse},
        { typeof(LINT), LINT.Parse},
        { typeof(REAL), REAL.Parse},
        { typeof(LREAL), LREAL.Parse},
        { typeof(USINT), USINT.Parse},
        { typeof(UINT), UINT.Parse},
        { typeof(UDINT), UDINT.Parse},
        { typeof(ULINT), ULINT.Parse},
        { typeof(AtomicType), Atomic.Parse},
        { typeof(STRING), s => new STRING(s)},
        { typeof(DateTime), s => DateTime.Parse(s) },
        { typeof(ConnectionPriority), ConnectionPriority.FromValue },
        { typeof(ConnectionType), ConnectionType.FromValue },
        { typeof(DataTypeClass), DataTypeClass.FromValue },
        { typeof(DataTypeFamily), DataTypeFamily.FromValue },
        { typeof(ElectronicKeying), ElectronicKeying.FromValue },
        { typeof(ExternalAccess), ExternalAccess.FromValue },
        { typeof(Keyword), Keyword.FromValue },
        { typeof(OnlineEditType), OnlineEditType.FromValue },
        { typeof(Operator), Operator.FromValue },
        { typeof(PassThroughOption), PassThroughOption.FromValue },
        { typeof(PenType), PenType.FromValue },
        { typeof(ProductionTrigger), ProductionTrigger.FromValue },
        { typeof(ProgramType), ProgramType.FromValue },
        { typeof(Radix), Radix.FromValue },
        { typeof(RoutineType), RoutineType.FromValue },
        { typeof(RungType), RungType.FromValue },
        { typeof(SamplesType), SamplesType.FromValue },
        { typeof(Scope), Scope.FromValue },
        { typeof(SFCExecutionControl), SFCExecutionControl.FromValue },
        { typeof(SFCRestartPosition), SFCRestartPosition.FromValue },
        { typeof(SFCLastScan), SFCLastScan.FromValue },
        { typeof(SheetOrientation), SheetOrientation.FromValue },
        { typeof(SheetSize), SheetSize.FromValue },
        { typeof(TagType), TagType.FromValue },
        { typeof(TagUsage), TagUsage.FromValue },
        { typeof(TaskEventTrigger), TaskEventTrigger.FromValue },
        { typeof(TaskType), TaskType.FromValue },
        { typeof(TransmissionType), TransmissionType.FromValue },
        { typeof(TriggerOperation), TriggerOperation.Parse },
        { typeof(TriggerTargetType), TriggerTargetType.FromValue },
        { typeof(TriggerType), TriggerType.FromValue },
        { typeof(Use), s => Use.FromName(s) },
        { typeof(Dimensions), Dimensions.Parse },
        { typeof(Revision), s => new Revision(s) },
        { typeof(TagName), s => new TagName(s) },
        { typeof(NeutralText), s => new NeutralText(s) },
        { typeof(TaskPriority), s => TaskPriority.Parse(s) },
        { typeof(ScanRate), s => ScanRate.Parse(s) },
        { typeof(ScanRate?), s => ScanRate.Parse(s) },
        { typeof(Watchdog), s => Watchdog.Parse(s) },
        { typeof(Vendor), Vendor.Parse },
        { typeof(ProductType), ProductType.Parse },
        { typeof(Address), s => new Address(s) }
    };

    /// <summary>
    /// Parses the provided string input to the specified type using the predefined L5X parser functions.
    /// </summary>
    /// <param name="input">The string input to parse.</param>
    /// <typeparam name="T">The type of property to return.</typeparam>
    /// <returns>The resulting parsed value.</returns>
    /// <exception cref="InvalidOperationException">When a parser was not found to the specified type.</exception>
    public static T Parse<T>(this string input)
    {
        var parser = GetParser(typeof(T));

        var value = parser(input);

        if (value is not T typed)
            throw new ArgumentException($"The input '{input}' could not be parsed to type '{typeof(T)}");

        return typed;
    }

    /// <summary>
    /// Gets a parser function for the specified type.
    /// </summary>
    /// <remarks>
    /// Simply looks to predefined parser functions and returns one if found for the specified type.
    /// Otherwise will use the <see cref="TypeDescriptor"/> of the current type and return the ConvertFrom function
    /// if is capable of converting from a string type. If not, it returns a default delegate that returns the current value.
    /// </remarks>
    /// <param name="type">The type to parse.</param>
    /// <returns>
    /// A func that can convert from a string to an object for the provided type is the func is defined; otherwise, null.
    /// </returns>
    private static Func<string, object> GetParser(Type type)
    {
        if (Parsers.TryGetValue(type, out var parser))
            return parser;

        var converter = TypeDescriptor.GetConverter(type);

        if (converter.CanConvertFrom(typeof(string)))
            return s => converter.ConvertFrom(s);

        throw new InvalidOperationException($"No parse function has been defined for type '{type}'");
    }
}