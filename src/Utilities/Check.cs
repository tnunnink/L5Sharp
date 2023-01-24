using System;
using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Utilities
{
    internal static class Check
    {
        public static void NotNull(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));
        }

        public static void NotNullOrEmpty(string str)
        {
            switch (str)
            {
                case null:
                    throw new ArgumentNullException(nameof(str));
                case "":
                    throw new ArgumentException("Value can not be empty", nameof(str));
            }
        }

        public static void SerializerAttribute(Attribute attribute, Type type)
        {
            const string suggestion = "Class must specify a LogixSerializerAttribute in order to be deserialized.";
            
            if (attribute is null)
                throw new InvalidOperationException(
                    $"No serializer defined for type {type}. {suggestion}");
        }
    }
}