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
            { typeof(ConnectionPriority), s => ConnectionPriority.TryFromName(s, false, out var value) ? value : default },
            { typeof(ConnectionType), s => ConnectionType.FromName(s) },
            { typeof(DataTypeClass), s => DataTypeClass.FromName(s) },
            { typeof(DataTypeFamily), s => DataTypeFamily.FromName(s) },
            { typeof(ExternalAccess), s => ExternalAccess.FromName(s) },
            { typeof(KeyingState), s => KeyingState.FromName(s) },
            { typeof(ProcessorType), s => ProcessorType.FromName(s) },
            { typeof(ProductionTrigger), s => ProductionTrigger.FromName(s) },
            { typeof(ProgramType), s => ProgramType.FromName(s) },
            { typeof(Radix), s => Radix.FromName(s) },
            { typeof(RoutineType), s => RoutineType.FromName(s) },
            { typeof(RungType), s => RungType.FromName(s) },
            { typeof(Scope), s => Scope.FromName(s) },
            { typeof(TagType), s => TagType.FromName(s) },
            { typeof(TagUsage), s => TagUsage.FromName(s) },
            { typeof(TaskTrigger), s => TaskTrigger.FromName(s) },
            { typeof(TaskType), s => TaskType.FromName(s) },
            { typeof(TransmissionType), s => TransmissionType.FromName(s) },
            { typeof(Use), s => Use.FromName(s) },

            //Values
            { typeof(Dimensions), Dimensions.Parse },
            { typeof(TaskPriority), s => TaskPriority.Parse(s) },
            { typeof(ScanRate), s => ScanRate.Parse(s) },
            { typeof(Watchdog), s => Watchdog.Parse(s) }
        };

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T? Parse<T>(string input)
        {
            var type = typeof(T);

            var parser = Get(type);

            if (parser is null)
                throw new InvalidOperationException($"No parse function has been defined for type '{type.Name}'");

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