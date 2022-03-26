using System;
using System.Collections.Generic;
using System.ComponentModel;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.L5X
{
    /// <summary>
    /// Static class containing mappings for converting string values (XML typed value) to the strongly type object.
    /// </summary>
    internal static class L5XParser
    {
        private static readonly Dictionary<Type, Func<string, object>> Parsers = new()
        {
            { typeof(DateTime), s => DateTime.Parse(s)},
            { typeof(IAtomicType), Radix.ParseValue },
            { typeof(ConnectionPriority), ConnectionPriority.FromValue },
            { typeof(ConnectionType), ConnectionType.FromValue },
            { typeof(DataTypeClass), DataTypeClass.FromValue },
            { typeof(DataTypeFamily), DataTypeFamily.FromValue },
            { typeof(ExternalAccess), s => ExternalAccess.FromName(s) },
            { typeof(KeyingState), KeyingState.FromValue },
            { typeof(ProductionTrigger), ProductionTrigger.FromValue },
            { typeof(ProgramType), ProgramType.FromValue },
            { typeof(Radix), Radix.FromValue },
            { typeof(RoutineType), RoutineType.FromValue },
            { typeof(RungType), RungType.FromValue },
            { typeof(Scope), Scope.FromValue },
            { typeof(TagType), TagType.FromValue },
            { typeof(TagUsage), TagUsage.FromValue },
            { typeof(TaskEventTrigger), TaskEventTrigger.FromValue },
            { typeof(TaskType), TaskType.FromValue },
            { typeof(TransmissionType), TransmissionType.FromValue },
            { typeof(Use), s => Use.FromName(s) },
            { typeof(Dimensions), Dimensions.Parse },
            { typeof(Revision), Revision.Parse },
            { typeof(TagName), s => new TagName(s) },
            { typeof(NeutralText), s => new NeutralText(s) },
            { typeof(TaskPriority), s => TaskPriority.Parse(s) },
            { typeof(ScanRate), s => ScanRate.Parse(s) },
            { typeof(Watchdog), s => Watchdog.Parse(s) },
            { typeof(CatalogNumber), s => new CatalogNumber(s) },
            { typeof(Vendor), Vendor.Parse },
            { typeof(ProductType), ProductType.Parse }
        };
        
        private static readonly Dictionary<Type, Func<string, object?>> TryParsers = new()
        {
            { typeof(bool), s => bool.TryParse(s, out var value) ? value : default },
            { typeof(sbyte), s => sbyte.TryParse(s, out var value) ? value : default },
            { typeof(byte), s => byte.TryParse(s, out var value) ? value : default },
            { typeof(short), s => short.TryParse(s, out var value) ? value : default },
            { typeof(ushort), s => ushort.TryParse(s, out var value) ? value : default },
            { typeof(int), s => int.TryParse(s, out var value) ? value : default },
            { typeof(uint), s => uint.TryParse(s, out var value) ? value : default },
            { typeof(long), s => long.TryParse(s, out var value) ? value : default },
            { typeof(ulong), s => ulong.TryParse(s, out var value) ? value : default },
            { typeof(float), s => float.TryParse(s, out var value) ? value : default },
            { typeof(DateTime), s => DateTime.TryParse(s, out var value) ? value : default },
            { typeof(IAtomicType), s => Radix.TryParseValue(s, out var value) ? value : default },
            { typeof(ConnectionPriority), s => ConnectionPriority.TryFromValue(s, out var value) ? value : default },
            { typeof(ConnectionType), s => ConnectionType.TryFromValue(s, out var value) ? value : default },
            { typeof(DataFormat), s => DataFormat.TryFromValue(s, out var value) ? value : default },
            { typeof(DataTypeClass), s => DataTypeClass.TryFromValue(s, out var value) ? value : default },
            { typeof(DataTypeFamily), s => DataTypeFamily.TryFromValue(s, out var value) ? value : default },
            { typeof(ExternalAccess), s => ExternalAccess.TryFromName(s, out var value) ? value : default },
            { typeof(KeyingState), s => KeyingState.TryFromValue(s, out var value) ? value : default },
            { typeof(ProductionTrigger), s => ProductionTrigger.TryFromValue(s, out var value) ? value : default },
            { typeof(ProgramType), s => ProgramType.TryFromValue(s, out var value) ? value : default },
            { typeof(Radix), s => Radix.TryFromValue(s, out var value) ? value : default },
            { typeof(RoutineType), s => RoutineType.TryFromValue(s, out var value) ? value : default },
            { typeof(RungType), s => RungType.TryFromValue(s, out var value) ? value : default },
            { typeof(Scope), s => Scope.TryFromValue(s, out var value) ? value : default },
            { typeof(TagType), s => TagType.TryFromValue(s, out var value) ? value : default },
            { typeof(TagUsage), s => TagUsage.TryFromValue(s, out var value) ? value : default },
            { typeof(TaskEventTrigger), s => TaskEventTrigger.TryFromValue(s, out var value) ? value : default },
            { typeof(TaskType), s => TaskType.TryFromValue(s, out var value) ? value : default },
            { typeof(TransmissionType), s => TransmissionType.TryFromValue(s, out var value) ? value : default },
            { typeof(Use), s => Use.TryFromName(s, out var value) ? value : default },
            { typeof(Dimensions), s => Dimensions.TryParse(s, out var value) ? value : default },
            { typeof(Revision), Revision.Parse },
            { typeof(TagName), s => new TagName(s) },
            { typeof(NeutralText), s => new NeutralText(s) },
            { typeof(TaskPriority), s => TaskPriority.Parse(s) },
            { typeof(ScanRate), s => ScanRate.Parse(s) },
            { typeof(Watchdog), s => Watchdog.Parse(s) },
            { typeof(CatalogNumber), s => new CatalogNumber(s) },
            { typeof(Vendor), Vendor.Parse },
            { typeof(ProductType), ProductType.Parse }
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
        /// Parses the provided string input to the specified generic type base ont predefined Logix parsing functions or type converters.
        /// </summary>
        /// <param name="input">The string input to parse.</param>
        /// <typeparam name="T">The type of property to return.</typeparam>
        /// <returns>The resulting parsed value.</returns>
        /// <exception cref="InvalidOperationException">When a parser was not found to the specified type.</exception>
        public static T? TryParse<T>(this string? input)
        {
            if (input is null) return default;
            
            var parser = GetTryParser(typeof(T));

            return (T?)parser(input);
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
        public static Func<string, object> GetParser(Type type)
        {
            if (Parsers.ContainsKey(type))
                return Parsers[type];

            var converter = TypeDescriptor.GetConverter(type);

            if (converter.CanConvertFrom(typeof(string)))
                return s => converter.ConvertFrom(s);
            
            throw new InvalidOperationException($"No parse function has been defined for type '{type.Name}'");
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
        public static Func<string, object?> GetTryParser(Type type)
        {
            if (!TryParsers.TryGetValue(type, out var parser))
                throw new InvalidOperationException($"No parse function has been defined for type '{type.Name}'");

            return parser;
        }
    }
}