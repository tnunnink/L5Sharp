using System;
using System.Collections.Generic;
using System.ComponentModel;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Common
{
    /// <summary>
    /// Static class containing mappings for converting string values (XML typed value) to the strongly type object.
    /// </summary>
    public static class LogixParser
    {
        private static readonly Dictionary<Type, Func<string, object?>> Parsers = new()
        {
            //Enums
            { typeof(ConnectionPriority), s => ConnectionPriority.TryFromValue(s, out var value) ? value : default },
            { typeof(ConnectionType), s => ConnectionType.TryFromValue(s, out var value) ? value : default },
            { typeof(DataTypeClass), s => DataTypeClass.TryFromValue(s, out var value) ? value : default },
            { typeof(DataTypeFamily), s => DataTypeFamily.TryFromValue(s, out var value) ? value : default },
            { typeof(ExternalAccess), s => ExternalAccess.TryFromName(s, out var value) ? value : default },
            { typeof(KeyingState), s => KeyingState.TryFromValue(s, out var value) ? value : default },
            { typeof(ProcessorType), s => ProcessorType.TryFromValue(s, out var value) ? value : default },
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
            { typeof(Use), s => Use.FromName(s) },

            //Value Types
            { typeof(Dimensions), Dimensions.Parse },
            { typeof(NeutralText), s => new NeutralText(s) },
            { typeof(TaskPriority), s => TaskPriority.Parse(s) },
            { typeof(ScanRate), s => ScanRate.Parse(s) },
            { typeof(Watchdog), s => Watchdog.Parse(s) }
        };

        
        /// <summary>
        /// Parses the provided string input to the specified generic type base ont predefined Logix parsing functions or type converters.
        /// </summary>
        /// <param name="input">The string input to parse.</param>
        /// <typeparam name="T">The type of property to return.</typeparam>
        /// <returns>The resulting parsed value.</returns>
        /// <exception cref="InvalidOperationException">When a parser was not found to the specified type.</exception>
        public static T? Parse<T>(string input)
        {
            var parser = Get(typeof(T));

            if (parser is null)
                throw new InvalidOperationException($"No parse function has been defined for type '{typeof(T)}'");

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
        public static Func<string, object?>? Get(Type type)
        {
            if (Parsers.ContainsKey(type))
                return Parsers[type];

            var converter = TypeDescriptor.GetConverter(type);

            if (converter.CanConvertFrom(typeof(string)))
                return s => converter.ConvertFrom(s);

            return null;
        }
    }
}