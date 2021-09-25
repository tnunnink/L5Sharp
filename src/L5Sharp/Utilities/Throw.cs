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
        
        public static void NotConfigurableException(string me) =>
            throw new RadixNotSupportedException($"The");
        
        public static void RadixNotSupportedException(Radix radix, IDataType dataType) =>
            throw new RadixNotSupportedException($"Radix {radix.Name} not supported by type {dataType.Name}");
    }
}