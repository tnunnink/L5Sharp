using System;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;

namespace L5Sharp.Utilities
{
    internal static class Throw
    {
        public static void ArgumentNullOrEmptyException(string paramName)
            => throw new ArgumentException("Argument cannot be null or empty.", paramName);

        public static void InvalidNameException(string tagName) =>
            throw new InvalidNameException(
                $"Name {tagName} is not valid. " +
                $"Must contain alphanumeric, start with letter, and contains only '_' special characters");
        
        public static void PredefinedCollisionException(string dataType) =>
            throw new PredefinedCollisionException(
                $"Data type {dataType} already exists either as a predefined type on in the current controller context");

        public static void ComponentNameCollisionException(string name, Type type) =>
            throw new ComponentNameCollisionException(
                $"Name {name} already exists for {type}");
        
        public static void ComponentNotFoundException(string name, Type type) =>
            throw new ComponentNotFoundException(
                $"Component {name} does not exists for {type}");
        
        public static void ComponentReferencedException(string name, Type type) =>
            throw new ComponentReferencedException(
                $"Component {name} is of type {type} is currently referenced");

        public static void NotConfigurableException(string propertyName, string baseType, string reason) =>
            throw new NotConfigurableException(
                $"The property {propertyName} is not not configurable for this instance of {baseType}. {reason}");
        
        public static void RadixNotSupportedException(Radix radix, IDataType dataType) =>
            throw new RadixNotSupportedException($"Radix {radix.Name} not supported by type {dataType.Name}");
    }
}