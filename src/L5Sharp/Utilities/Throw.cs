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

        public static void InvalidTagNameException(string tagName) =>
            throw new InvalidTagNameException(
                $"Name {tagName} is not valid. " +
                $"Must contain alphanumeric, start with letter, and contains only '_' special characters");
        
        public static void DataTypeAlreadyExistsException(string dataType) =>
            throw new DataTypeAlreadyExistsException(
                $"Data type {dataType} already exists either as a predefined type on in the current controller context");

        public static void NameCollisionException(string name) =>
            throw new NameCollisionException(
                $"Name {name} already exists for this type. Name must be unique");
        
        public static void ItemNotFoundException(string name) =>
            throw new ItemNotFoundException(
                $"Item {name} does not exists on this type");

        public static void NotConfigurableException(string propertyName, string baseType, string reason) =>
            throw new NotConfigurableException(
                $"The property {propertyName} is not not configurable for this instance of {baseType}. {reason}");
        
        public static void RadixNotSupportedException(Radix radix, IDataType dataType) =>
            throw new RadixNotSupportedException($"Radix {radix.Name} not supported by type {dataType.Name}");
    }
}