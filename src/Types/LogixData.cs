using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Types;

/// <summary>
/// A static factory class for creating <see cref="LogixType"/> objects.
/// </summary>
/// <remarks>
/// <para>
/// All atomic, predefined, and module defined
/// types are registered and able to be generated using this class. For user defined or other unsupported types, you
/// can <see cref="Register{TStructureType}()"/> them in order to have the <see cref="LogixSerializer"/> deserialize those
/// types as the concrete implementation (as opposed to a generic <see cref="StructureType"/>).
/// </para>
/// <para>
/// You can also call <see cref="Scan"/> or <see cref="ScanAll"/> to register all types in the current assemble or
/// app domain using reflection so to avoid calling register for each individual structure type you create.
/// </para>
/// </remarks>
public static class LogixData
{
    private static readonly Dictionary<string, Func<string, AtomicType>> Atomics = new()
    {
        { nameof(BOOL), BOOL.Parse },
        { "BIT", BOOL.Parse },
        { nameof(SINT), SINT.Parse },
        { nameof(INT), INT.Parse },
        { nameof(DINT), DINT.Parse },
        { nameof(LINT), LINT.Parse },
        { nameof(REAL), REAL.Parse },
        { nameof(USINT), USINT.Parse },
        { nameof(UINT), UINT.Parse },
        { nameof(UDINT), UDINT.Parse },
        { nameof(ULINT), ULINT.Parse }
    };

    private static readonly Dictionary<string, Type> Registered = new()
    {
        { nameof(STRING), typeof(STRING) },
        { nameof(TIMER), typeof(TIMER) },
        { nameof(COUNTER), typeof(COUNTER) },
        { nameof(MESSAGE), typeof(MESSAGE) },
        { nameof(CONTROL), typeof(CONTROL) },
        { nameof(PHASE), typeof(PHASE) },
        { nameof(PID), typeof(PID) },
        { nameof(ALARM), typeof(ALARM) },
        { nameof(ALARM_DIGITAL), typeof(ALARM_DIGITAL) },
        { nameof(ALARM_ANALOG), typeof(ALARM_ANALOG) },
    };

    /// <summary>
    /// Returns the singleton null <see cref="LogixType"/> object.
    /// </summary>
    public static LogixType Null => NullType.Instance;

    /// <summary>
    /// Creates a new <see cref="LogixType"/> from a given <see cref="XElement"/> data structure.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to parse.</param>
    /// <returns>A new <see cref="LogixType"/> created from the element.</returns>
    /// <exception cref="ArgumentException"><c>element</c> is null.</exception>
    /// <exception cref="NotSupportedException">The element name of <c>element</c> is not a recognized or supported
    /// element name for tag data structures.
    /// </exception>
    /// <remarks>
    /// This will return the most concrete type registered with the internal collection.
    /// This means type should be cast-able to the most derived type assuming the are registered.
    /// </remarks>
    public static LogixType Deserialize(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        return element.Name.ToString() switch
        {
            L5XName.Tag => CreateData(element),
            L5XName.Data => CreateFormatted(element),
            L5XName.DataValue => CreateAtomic(element),
            L5XName.DataValueMember => CreateAtomic(element),
            L5XName.Element => CreateElement(element),
            L5XName.Array => CreateArray(element),
            L5XName.ArrayMember => CreateArray(element),
            L5XName.Structure => CreateStructure(element),
            L5XName.StructureMember => CreateStructure(element),
            L5XName.AlarmAnalogParameters => new ALARM_ANALOG(element),
            L5XName.AlarmDigitalParameters => new ALARM_DIGITAL(element),
            L5XName.MessageParameters => new MESSAGE(element),
            _ => throw new NotSupportedException(
                $"The element name '{element.Name}' is not able to be used to create a logix type.")
        };
    }

    /// <summary>
    /// Determines if the logix type with the provided name is registered in the <see cref="LogixData"/> factory class.
    /// </summary>
    /// <param name="name">The logix type name to find.</param>
    /// <returns><c>true</c> if the type is registered in the factory class.</returns>
    public static bool IsRegistered(string name) => Registered.ContainsKey(name);

    /// <summary>
    /// Determines if the logix type with the provided name is registered in the <see cref="LogixData"/> factory class.
    /// </summary>
    /// <returns><c>true</c> if the type is registered in the factory class.</returns>
    public static bool IsRegistered<TStructureType>() where TStructureType : StructureType =>
        Registered.ContainsValue(typeof(TStructureType));

    /// <summary>
    /// Determines whether the specified type name is an <see cref="AtomicType"/> logix type.
    /// </summary>
    /// <param name="name">The name of the type.</param>
    /// <returns><c>true</c> if <c>typeName</c> represents the name of an atomic type; otherwise, <c>false</c>.</returns>
    public static bool IsAtomic(string name) => Atomics.ContainsKey(name);

    /// <summary>
    /// Register a custom type with the static <see cref="LogixData"/> factory to be used for created of the type during
    /// deserialization of a L5X. 
    /// </summary>
    /// <typeparam name="TStructureType">The type of the logix type.</typeparam>
    /// <returns><c>true</c> if the type was registered successfully; otherwise, false.</returns>
    public static void Register<TStructureType>() where TStructureType : StructureType, new()
    {
        var type = typeof(TStructureType);
        Registered.TryAdd(type.Name, typeof(TStructureType));
    }

    /// <summary>
    /// Register a custom type with the static <see cref="LogixData"/> factory to be used for created of the type during
    /// deserialization of a L5X. 
    /// </summary>
    /// <typeparam name="TStructureType">The type of the logix type.</typeparam>
    /// <returns><c>true</c> if the type was registered successfully; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    public static void Register<TStructureType>(string name) where TStructureType : StructureType, new()
    {
        Registered.TryAdd(name, typeof(TStructureType));
    }

    /// <summary>
    /// Scans all assemblies in the current app domain for types inheriting from <see cref="StructureType"/> and
    /// registers them using the defined type name. 
    /// </summary>
    /// <remarks>
    /// This makes registration of custom types simpler since you don't have to call
    /// <see cref="Register{TStructureType}()"/> for each one, but rather register all at once using reflection.
    /// Doing this will allow the user and predefined types classes you have creates to be
    /// instantiated upon deserialization.
    /// </remarks>
    public static void ScanAll() => AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(ScanAssembly);

    /// <summary>
    /// Scans the specified assembly for types inheriting from <see cref="StructureType"/> and
    /// registers them using the defined type name. 
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to scan</param>
    /// <remarks>
    /// This make registration of custom types simpler since you don't have to call
    /// <see cref="Register{TStructureType}()"/> for each one, but rather register all in a specific assembly at once
    /// using reflection. Doing this will allow the user and predefined types classes you have creates to be
    /// instantiated upon deserialization.
    /// </remarks>
    public static void Scan(Assembly assembly) => ScanAssembly(assembly);

    #region Internal

    private static void ScanAssembly(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes()
                     .Where(t => t.IsSubclassOf(typeof(StructureType))
                                 && t is { IsAbstract: false, IsPublic: true }))
            Registered.TryAdd(type.Name, type);
    }

    private static LogixType CreateData(XContainer element)
    {
        var data = element.Elements(L5XName.Data)
            .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value != DataFormat.L5K);

        return data is not null ? Deserialize(data) : Null;
    }

    private static LogixType CreateFormatted(XElement element)
    {
        DataFormat.TryFromName(element.Attribute(L5XName.Format)?.Value, out var format);

        if (format is null) return Null;

        if (format == DataFormat.Decorated || format == DataFormat.Alarm || format == DataFormat.Message)
        {
            var root = element.Elements().FirstOrDefault();
            return root is not null ? Deserialize(root) : Null;
        }
        
        if (format == DataFormat.String)
            return CreateString(element);

        throw new NotSupportedException($"The tag data format {format} is not supported for deserialization");
    }

    private static AtomicType CreateAtomic(XElement element)
    {
        var name = element.Attribute(L5XName.DataType)?.Value
                   ?? throw new L5XException(L5XName.DataType, element);

        if (!Atomics.ContainsKey(name))
            throw new InvalidOperationException($"The type name '{name}' is not a valid {typeof(AtomicType)}");

        var value = element.Attribute(L5XName.Value)?.Value ?? throw new L5XException(L5XName.Value, element);

        return Atomics[name].Invoke(value);
    }

    private static LogixType CreateElement(XElement element)
    {
        if (element.Attribute(L5XName.Value) is not null)
        {
            var name = element.Parent?.Attribute(L5XName.DataType)?.Value
                       ?? throw new L5XException(L5XName.DataType, element);

            if (!Atomics.ContainsKey(name))
                throw new InvalidOperationException($"The type name '{name}' is not a valid {typeof(AtomicType)}");

            var value = element.Attribute(L5XName.Value)?.Value ?? throw new L5XException(L5XName.Value, element);

            return Atomics[name].Invoke(value);
        }

        var structure = element.Element(L5XName.Structure);
        return structure is not null ? CreateStructure(structure) : Null;
    }

    private static ArrayType<LogixType> CreateArray(XElement element) => new(element);

    private static StructureType CreateStructure(XElement element)
    {
        var dataType = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);

        // Not all structure types can be known ahead of time,
        // but we can look to see if it is registered and create the concrete type if so.
        if (Registered.TryGetValue(dataType, out var type))
            return (StructureType)LogixSerializer.Deserialize(type, element);

        // Rockwell is dumb. A string type embedded within a structure looks like a structure type but has a
        // non-atomic value member, so it needs special treatment.
        // Basically we just try to detect the "string structure" and create it if found.
        return element.HasLogixStringStructure() ? new StringType(element) : new StructureType(element);
    }

    private static StringType CreateString(XElement element)
    {
        // string structure tags don't have a data type name on the data element so we have to rely on the parent tag.
        var dataType = element.Ancestors(L5XName.Tag).FirstOrDefault()?.Attribute(L5XName.DataType)?.Value
               ?? throw new L5XException(L5XName.DataType, element);
        
        // check if this string type is registered first. otherwise we result to generic sting type.
        if (Registered.TryGetValue(dataType, out var type))
            return (StringType)LogixSerializer.Deserialize(type, element);

        return new StringType(element);
    }

    /// <summary>
    /// Builds a string structure element needed to update generate a string member index element.
    /// </summary>
    /// <param name="type">The string type instance.</param>
    /// <param name="name"></param>
    /// <returns>A <see cref="XElement"/> representing the serialized structure of the type.</returns>
    /// <remarks>This is really the only place (other than Member) that string types act or serialize string structure types.</remarks>
    internal static XElement StringMember(StringType type, string? name = null)
    {
        name ??= L5XName.Structure;
        
        var element = new XElement(name);
        element.Add(new XAttribute(L5XName.DataType, type.Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(type.LEN)));
        len.Add(new XAttribute(L5XName.DataType, type.LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal.Value));
        len.Add(new XAttribute(L5XName.Value, type.LEN.ToString()));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(type.DATA)));
        data.Add(new XAttribute(L5XName.DataType, type.Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
        data.Add(new XCData(type.DATA.AsString()));
        element.Add(data);

        return element;
    }

    #endregion
}