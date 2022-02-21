using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Helpers
{
    /// <summary>
    /// Helper class that contains all Logix L5X component and attribute XName values as strongly typed members so to
    /// avoid using magic strings and allow us to update them in a central location as necessary.
    /// </summary>
    internal static class LogixNames
    {
        //Component Map Collection
        private static readonly Dictionary<Type, Tuple<string, string>> ComponentNameMap = new()
        {
            { typeof(IController), new Tuple<string, string>(L5XElement.Controller.ToString(), string.Empty) },
            {
                typeof(IUserDefined),
                new Tuple<string, string>(L5XElement.DataType.ToString(), L5XElement.DataTypes.ToString())
            },
            {
                typeof(IMember<IDataType>),
                new Tuple<string, string>(L5XElement.Member.ToString(), L5XElement.Members.ToString())
            },
            { typeof(Module), new Tuple<string, string>(L5XElement.Module.ToString(), L5XElement.Modules.ToString()) },
            {
                typeof(IAddOnInstruction),
                new Tuple<string, string>(L5XElement.AddOnInstructionDefinition.ToString(),
                    L5XElement.AddOnInstructionDefinitions.ToString())
            },
            { typeof(Port), new Tuple<string, string>(L5XElement.Port.ToString(), L5XElement.Ports.ToString()) },
            {
                typeof(Connection),
                new Tuple<string, string>(L5XElement.Connection.ToString(), L5XElement.Connections.ToString())
            },
            {
                typeof(IParameter<IDataType>),
                new Tuple<string, string>(L5XElement.Parameter.ToString(), L5XElement.Parameters.ToString())
            },
            {
                typeof(ITag<IDataType>),
                new Tuple<string, string>(L5XElement.Tag.ToString(), L5XElement.Tags.ToString())
            },
            {
                typeof(IProgram),
                new Tuple<string, string>(L5XElement.Program.ToString(), L5XElement.Programs.ToString())
            },
            {
                typeof(IRoutine<ILogixContent>),
                new Tuple<string, string>(L5XElement.Routine.ToString(), L5XElement.Routine.ToString())
            },
            { typeof(ILadderLogic), new Tuple<string, string>(L5XElement.RllContent.ToString(), string.Empty) },
            { typeof(IStructuredText), new Tuple<string, string>(L5XElement.StContent.ToString(), string.Empty) },
            { typeof(Rung), new Tuple<string, string>(L5XElement.Rung.ToString(), L5XElement.Rungs.ToString()) },
            { typeof(ITask), new Tuple<string, string>(L5XElement.Task.ToString(), L5XElement.Tasks.ToString()) }
        };


        /// <summary>
        /// A helper for dynamically getting a component <see cref="XName"/> based on the specified object type. 
        /// </summary>
        /// <typeparam name="TComponent">The component type to get an XName for.</typeparam>
        /// <returns>A <see cref="string"/> that represents the L5X element name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified component has no mapping defined.</exception>
        public static string GetComponentName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException(
                    $"No component type mapping defined for '{typeof(TComponent)}'");

            return ComponentNameMap[key].Item1;
        }

        /// <summary>
        /// A helper for dynamically getting a container <see cref="XName"/> based on the specified object type. 
        /// </summary>
        /// <typeparam name="TComponent">The component type to get an XName for.</typeparam>
        /// <returns>A <see cref="string"/> that represents the L5X container element name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified component has no mapping defined.</exception>
        public static string GetContainerName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException(
                    $"No component type mapping defined for '{typeof(TComponent)}'");

            return ComponentNameMap[key].Item2;
        }

        private static Type? FindKey<TComponent>() =>
            ComponentNameMap.Keys.FirstOrDefault(t => t.IsAssignableFrom(typeof(TComponent)));
    }
}