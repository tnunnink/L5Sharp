using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Utilities
{
    /// <summary>
    /// Static class containing mappings for converting string values (XML typed value) to the strongly type object.
    /// </summary>
    public static class LogixParsers
    {
        private static readonly Dictionary<Type, Func<string, object>> Parsers =
            new Dictionary<Type, Func<string, object>>
            {
                //Enums
                {typeof(ConnectionPriority), s => ConnectionPriority.FromName(s)},
                {typeof(ConnectionType), s => ConnectionType.FromName(s)},
                {typeof(DataTypeClass), s => DataTypeClass.FromName(s)},
                {typeof(DataTypeFamily), s => DataTypeFamily.FromName(s)},
                {typeof(ExternalAccess), s => ExternalAccess.FromName(s)},
                {typeof(KeyingState), s => KeyingState.FromName(s)},
                {typeof(ProcessorType), s => ProcessorType.FromName(s)},
                {typeof(ProductionTrigger), s => ProductionTrigger.FromName(s)},
                {typeof(ProgramType), s => ProgramType.FromName(s)},
                {typeof(Radix), s => Radix.FromName(s)},
                {typeof(RoutineType), s => RoutineType.FromName(s)},
                {typeof(RungType), s => RungType.FromName(s)},
                {typeof(Scope), s => Scope.FromName(s)},
                {typeof(TagType), s => TagType.FromName(s)},
                {typeof(TagUsage), s => TagUsage.FromName(s)},
                {typeof(TaskTrigger), s => TaskTrigger.FromName(s)},
                {typeof(TaskType), s => TaskType.FromName(s)},
                {typeof(TransmissionType), s => TransmissionType.FromName(s)},
                {typeof(Use), s => Use.FromName(s)},
                
                //Values
                {typeof(Dimensions), Dimensions.Parse},
                {typeof(TaskPriority), s => TaskPriority.Parse(s)},
                {typeof(ScanRate), s => ScanRate.Parse(s)},
                {typeof(Watchdog), s => Watchdog.Parse(s)},
            };

        public static Func<string, object> Get(Type type)
        {
            return Parsers.ContainsKey(type) ? Parsers[type] : null;
        }

        public static bool Contains(Type type)
        {
            return Parsers.ContainsKey(type);
        }
    }
}