using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.L5X
{
    /// <summary>
    /// Helper class that contains all Logix L5X component and attribute XName values as strongly typed members so to
    /// avoid using magic strings and allow us to update them in a central location as necessary.
    /// </summary>
    internal static class L5XNames
    {
        private static readonly Dictionary<Type, string> ComponentNameMap = new()
        {
            { typeof(IController), L5XElement.Controller.ToString() },
            { typeof(IUserDefined), L5XElement.DataType.ToString() },
            { typeof(IMember<IDataType>), L5XElement.Member.ToString() },
            { typeof(IModule), L5XElement.Module.ToString() },
            { typeof(ITag<IDataType>), L5XElement.Tag.ToString() },
            { typeof(IProgram), L5XElement.Program.ToString() },
            { typeof(IRoutine<ILogixContent>), L5XElement.Routine.ToString() },
            { typeof(ITask), L5XElement.Task.ToString() }
        };
        
        private static readonly Dictionary<Type, string> ContainerNameMap = new()
        {
            { typeof(IUserDefined), L5XElement.DataTypes.ToString() },
            { typeof(IMember<IDataType>), L5XElement.Members.ToString() },
            { typeof(IModule), L5XElement.Modules.ToString() },
            { typeof(ITag<IDataType>), L5XElement.Tags.ToString() },
            { typeof(IProgram), L5XElement.Programs.ToString() },
            { typeof(IRoutine<ILogixContent>), L5XElement.Routines.ToString() },
            { typeof(ITask), L5XElement.Tasks.ToString() }
        };


        /// <summary>
        /// A helper for dynamically getting a component <see cref="XName"/> based on the specified object type. 
        /// </summary>
        /// <typeparam name="TComponent">The component type to get an XName for.</typeparam>
        /// <returns>A <see cref="string"/> that represents the L5X element name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified component has no mapping defined.</exception>
        public static string GetComponentName<TComponent>() 
            where TComponent : ILogixComponent
        {
            var mapping = ComponentNameMap.FirstOrDefault(t => t.Key == typeof(TComponent)).Value;

            if (mapping is null)
                throw new InvalidOperationException($"No component name mapping defined for '{typeof(TComponent)}'");

            return mapping;
        }

        /// <summary>
        /// A helper for dynamically getting a container <see cref="XName"/> based on the specified object type. 
        /// </summary>
        /// <typeparam name="TComponent">The component type to get an XName for.</typeparam>
        /// <returns>A <see cref="string"/> that represents the L5X container element name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified component has no mapping defined.</exception>
        public static string GetContainerName<TComponent>()    
            where TComponent : ILogixComponent
        {
            var mapping = ContainerNameMap.FirstOrDefault(t => t.Key == typeof(TComponent)).Value;

            if (mapping is null)
                throw new InvalidOperationException($"No container name mapping defined for '{typeof(TComponent)}'");

            return mapping;
        }
    }
}