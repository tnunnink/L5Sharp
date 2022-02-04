using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Helpers;
using L5Sharp.Serialization;

namespace L5Sharp
{
    /// <summary>
    /// A helper class that contains all predefined serializer instances that can be used on a per context basis.
    /// </summary>
    public class LogixSerializer
    {
        private readonly Dictionary<string, IXSerializer> _serializers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        internal LogixSerializer(LogixContext context)
        {
            _serializers = new Dictionary<string, IXSerializer>
            {
                { LogixNames.Controller, new ControllerSerializer() },
                { LogixNames.DataType, new UserDefinedSerializer(context) },
                { LogixNames.Member, new MemberSerializer(context) },
                { LogixNames.Module, new ModuleSerializer(context) },
                { LogixNames.Tag, new TagSerializer(context) },
                { LogixNames.Rung, new RungSerializer() },
                { LogixNames.RllContent, new LadderLogicSerializer(context) },
                { LogixNames.Task, new TaskSerializer() }
            };
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="serializerName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public XElement Serialize<T>(T component, string? serializerName = null)
        {
            var name = serializerName ?? LogixNames.GetComponentName<T>();

            var serializer = GetSerializer<T>(name);

            return serializer.Serialize(component);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="serializerName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Deserialize<T>(XElement element, string? serializerName = null)
        {
            var name = serializerName ?? LogixNames.GetComponentName<T>();

            var serializer = GetSerializer<T>(name);

            return serializer.Deserialize(element);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private IXSerializer<T> GetSerializer<T>(string name) => 
            _serializers.TryGetValue(name, out var serializer) 
                ? (IXSerializer<T>)serializer 
                : throw new InvalidOperationException($"No serializer has been defined for'{name}'");
    }
}